using UnityEngine;

public class ModularSideViewCharacterMovement2D : MonoBehaviour
{


    public enum MovementType
    {
        PhysicsManager2D,
        Rigidbody2D,
    }


    [Header("General")]
    [SerializeField] private MovementType movementType;

    private Rigidbody2D rigidbody;
    private PhysicsManager2D physicsManager;



    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        physicsManager = GetComponent<PhysicsManager2D>();
    }

    private void Update()
    {
        float movement = 0;
        bool isJumping = false;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement++;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement--;
        }
        if (physicsManager.IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                isJumping = true;
            }
        }

        switch (movementType)
        {
            case MovementType.PhysicsManager2D:
                physicsManager.velocity = new Vector2(movement * 3, physicsManager.velocity.y + (isJumping ? 5 : 0));
                break;
            case MovementType.Rigidbody2D:

                break;
        }
    }



}
