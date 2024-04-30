using System.Collections.Generic;
using System.Linq;

using TheAshBot;

using UnityEngine;

public class PhysicsManager2D : MonoBehaviour
{



    private const float MIN_VELOCITY = 0.01f;
    private const float START_GRAVITY = -0.1f;


    public delegate void OnCollision(Collider2D otherCollider);

    public event OnCollision OnCollisionEnter;
    public event OnCollision OnCollisionStay;
    public event OnCollision OnCollisionExit;



#if UNITY_EDITOR
    [Space(8), SerializeField] private bool showGismos = true;
#endif

    [Header("Movement")]
    private Vector3 movement;


    [Header("Gravity")]
    [SerializeField] private bool useGravity = true;
    [SerializeField] private float gravitySclae = -20;

    private float gravity;


    [Header("Velocity")]
    [SerializeField] public Vector2 velocity;


    [Header("Drag")]
    [SerializeField] private bool useDrag = true;
    [SerializeField] private float linerDrag = 0.1f;


    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float yOffset = 0;
    [SerializeField] private float groundCheckWidth= 0.05f;

    private bool hadCheckedIsGroundedThisFrame;

    private bool isGrounded;
    private bool isGroundedLast;


    [Header("Collision")]
    [SerializeField] private LayerMask collidableLayers;
    [Tooltip("SLOPES ARE NOT SUPPORTED!!!")]
    [SerializeField] private float maxSlopeAngle = 55;

    private float skinWidth = 0.015f;
    private int maxBounces = 5;
    private Bounds bounds;



    private new Collider2D collider;
    private List<Collider2D> collisionList;


    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        collisionList = new List<Collider2D>();

        // OnCollisionEnter += PhysicsManager2D_OnCollisionEnter;
    }

    private void Update()
    {
        hadCheckedIsGroundedThisFrame = false;

        bounds = collider.bounds;
        bounds.Expand(-2 * skinWidth);

        ApplyMovement();

        GroundCheck();

        if (useDrag)
        {
            // Drag
            velocity -= (Vector2)movement * linerDrag;
        }

        if (useGravity)
        {
            Gravity();
        }

        // minVelocity
        if (Mathf.Abs(velocity.x) < MIN_VELOCITY) velocity = new Vector2(0, velocity.y);
        if (Mathf.Abs(velocity.y) < MIN_VELOCITY) velocity = new Vector2(velocity.x, 0);

        DetectCollision();

    }


    public void ResetGravity()
    {
        gravity = 0;
    }


    #region Movement

    private void ApplyMovement()
    {
        Vector2 horizontialVelocity = new Vector2(velocity.x, 0);
        Vector2 verticalVelocity = new Vector2(0, velocity.y);
        movement = CollideAndSlide(horizontialVelocity * Time.deltaTime, transform.position, 0, false, horizontialVelocity * Time.deltaTime);
        movement += CollideAndSlide(verticalVelocity * Time.deltaTime, transform.position + movement, 0, true, verticalVelocity * Time.deltaTime);
        
        transform.position += movement;
    }

    #endregion


    #region Collision Detection
    
    private void DetectCollision()
    {
        List<Collider2D> collisionList = GetCollision();

        foreach (Collider2D collision in collisionList)
        {
            if (this.collisionList.Contains(collision))
            {
                // Collision stayed
                OnCollisionStay?.Invoke(collision);
            }
            else
            {
                // Collision Entered
                OnCollisionEnter?.Invoke(collision);
            }
        }

        foreach (Collider2D collision in this.collisionList)
        {
            if (!collisionList.Contains(collision))
            {
                // Collision Exited
                OnCollisionExit?.Invoke(collision);
            }
        }

        this.collisionList = collisionList;
    }

    private List<Collider2D> GetCollision()
    {
        List<Collider2D> collisionList = new List<Collider2D>();

        if (collider is BoxCollider2D)
        {
            collisionList = GetCollisionFromBoxCollider();
        }
        else if (collider is CircleCollider2D)
        {
            collisionList = GetCollisionFromCircleCollider();
        }
        else if (collider is CapsuleCollider2D)
        {
            collisionList = GetCollisionFromCapsuleCollider();
        }
        else
        {
            this.Log("your collider is not a suported collider type.");
        }

        collisionList.Remove(collider);
        return collisionList;
    }

    private List<Collider2D> GetCollisionFromBoxCollider()
    {
        BoxCollider2D boxCollider = collider as BoxCollider2D;
        return Physics2D.OverlapBoxAll((Vector2)transform.position + boxCollider.offset, boxCollider.size, 0, collidableLayers).ToList();
    }
    private List<Collider2D> GetCollisionFromCircleCollider()
    {
        CircleCollider2D circleCollider = collider as CircleCollider2D;
        return Physics2D.OverlapCircleAll((Vector2)transform.position + circleCollider.offset, circleCollider.radius, collidableLayers).ToList();
    }
    private List<Collider2D> GetCollisionFromCapsuleCollider()
    {
        CapsuleCollider2D capsuleCollider = collider as CapsuleCollider2D;
        return Physics2D.OverlapCapsuleAll((Vector2)transform.position + capsuleCollider.offset, capsuleCollider.size, CapsuleDirection2D.Vertical, 0, collidableLayers).ToList();
    }

    #endregion


    #region Collide And Slide!

    private Vector3 CollideAndSlide(Vector3 velocity, Vector3 position, int depth, bool gravityPass, Vector3 initialVelocity)
    {
        if (depth >= maxBounces)
        {
            return Vector3.zero;
        }

        float distance = velocity.magnitude + skinWidth;

        RaycastHit2D raycastHit = Physics2D.CircleCast(position, bounds.extents.x, velocity.normalized, distance, collidableLayers);
        if (raycastHit.transform != null)
        {
            Vector3 snapToSurface = velocity.normalized * (raycastHit.distance - skinWidth);
            Vector3 leftover = velocity - snapToSurface;
            float angle = Vector3.Angle(Vector3.up, raycastHit.normal);

            if (snapToSurface.magnitude <= skinWidth)
            {
                snapToSurface = Vector3.zero;
            }

            // normal Ground / Slope
            if (angle <= maxSlopeAngle)
            {
                if (gravityPass)
                {
                    return snapToSurface;
                }
                leftover = ProjectAndScale(leftover, raycastHit.normal);
            }
            // Wall or Steep slope
            else
            {
                // 3D
                float scale = 1 - Vector3.Dot(new Vector3(raycastHit.normal.x, 0/*, raycastHit.normal.z*/).normalized, -new Vector3(initialVelocity.x, 0 , initialVelocity.z).normalized);

                if (isGrounded && !gravityPass)
                {
                    leftover = ProjectAndScale(new Vector3(leftover.x, 0, leftover.z), new Vector3(raycastHit.normal.x, 0/*, raycastHit.normal.z*/));
                }
                else
                {
                    leftover = ProjectAndScale(leftover, raycastHit.normal) * scale;
                }
            }

            return snapToSurface + CollideAndSlide(leftover, position + snapToSurface, depth + 1, gravityPass, initialVelocity);
        }

        return velocity;
    }

    private Vector3 ProjectAndScale(Vector3 vector, Vector3 normal)
    {
        float magnitude = vector.magnitude;
        vector = Vector3.ProjectOnPlane(vector, normal).normalized;
        vector *= magnitude;
        return vector;
    }

    #endregion



    private void Gravity()
    {
        if (!IsGrounded())
        {
            gravity += gravitySclae * Time.deltaTime * Time.deltaTime;
            velocity += Vector2.up * gravity;
        }

        if (velocity.y < 0 && START_GRAVITY < 0)
        {
            // is falling
            if (!isGroundedLast && IsGrounded())
            {
                // hit the ground
                velocity = new Vector2(velocity.x, 0);
                gravity = START_GRAVITY;
            }
        }
    }

    #region GroundCheck

    public bool IsGrounded()
    {
        if (!hadCheckedIsGroundedThisFrame)
        {
            GroundCheck();
        }
        return isGrounded;
    }

    private void GroundCheck()
    {
        float boxCastDistance = 0.01f;

        isGroundedLast = isGrounded;

        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position - new Vector3(0, yOffset - 0.0f), new Vector2(groundCheckWidth * 2, 0.001f), 0, Vector2.down, boxCastDistance, groundLayer);

        isGrounded = raycastHit.transform != null;
        hadCheckedIsGroundedThisFrame = true;

#if UNITY_EDITOR
        if (showGismos)
        {
            Color rayColor = isGrounded ? Color.green : Color.red;

            Debug.DrawRay(transform.position + new Vector3(groundCheckWidth, 0), Vector2.down * (yOffset / 2 + boxCastDistance), rayColor);
            Debug.DrawRay(transform.position - new Vector3(groundCheckWidth, 0), Vector2.down * (yOffset / 2 + boxCastDistance), rayColor);
            Debug.DrawRay(transform.position - new Vector3(groundCheckWidth, yOffset + boxCastDistance), Vector2.right * groundCheckWidth * 2, rayColor);
        }
#endif
    }

    #endregion

}
