using UnityEngine;

public class SideViewCharacterJumpVelocity2D : MonoBehaviour, IJumpVelocity2D
{


    public enum MovementType
    {
        Transform,
        Rigidbody,
        PhysicsManager,
    }


    [SerializeField] private MovementType movementType;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private AnimationCurve jumpPath;
    [SerializeField] private float jumpTime;

    private float startJumpHeight = 0;
    private float elapsedJumpTime;
    private bool isJumping;


    private new Rigidbody2D rigidbody;
    private PhysicsManager2D physicsManager;


    private void Update()
    {
        if (movementType == MovementType.Transform)
        {
            if (isJumping)
            {
                elapsedJumpTime += Time.deltaTime;
                if (elapsedJumpTime > jumpTime)
                {
                    isJumping = false;
                    return;
                }

                float percentageComplete = elapsedJumpTime / jumpTime;

                transform.position = new Vector3(0, startJumpHeight + (jumpPath.Evaluate(percentageComplete) * jumpHeight));
            }
        }
    }

    public float GetJumpHeight()
    {
        return jumpHeight;
    }

    public void Jump()
    {
        switch (movementType)
        {
            case MovementType.Transform:
                startJumpHeight = transform.position.y;
                elapsedJumpTime = 0;
                isJumping = true;
                break;
            case MovementType.PhysicsManager:
                if (physicsManager == null)
                {
                    AddSetPhysicsManager();
                }

                physicsManager.AddForce(new Vector2(0, jumpHeight));
                break;
            case MovementType.Rigidbody:
                if (rigidbody == null)
                {
                    AddSetRigidBody();
                }

                rigidbody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                break;
        }
    }


    private void AddSetRigidBody()
    {
        if (!TryGetComponent(out rigidbody))
        {
            if (physicsManager)
            {
                physicsManager.enabled = false;
            }

            rigidbody = gameObject.AddComponent<Rigidbody2D>();
            rigidbody.freezeRotation = true;
        }
    }

    private void AddSetPhysicsManager()
    {
        if (!TryGetComponent(out physicsManager))
        {
            if (rigidbody)
            {
                Destroy(rigidbody);
            }

            physicsManager = gameObject.AddComponent<PhysicsManager2D>();
        }
        physicsManager.enabled = true;
    }
}
