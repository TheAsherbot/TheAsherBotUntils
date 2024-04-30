using UnityEngine;

namespace TheAshBot.TwoDimensional.SideViewCharacterMovement
{
    public class SideViewCharacterHorizontalMovementVelocity2D : MonoBehaviour, IHorizontalMoveVelocity2D
    {


        public enum MovementType
        {
            Transform,
            Rigidbody,
            PhysicsManager,
        }


        [SerializeField] private MovementType movementType;
        [SerializeField] private float movementSpeed = 5;


        private float horizontalVelocity;
        private new Rigidbody2D rigidbody;
        private PhysicsManager2D physicsManager;



        private void Update()
        {
            switch (movementType)
            {
                case MovementType.Transform:
                    transform.position += new Vector3(horizontalVelocity * movementSpeed * Time.deltaTime, 0);
                    break;
                case MovementType.PhysicsManager:
                    if (physicsManager == null) AddSetPhysicsManager();
                    physicsManager.velocity = new Vector2(horizontalVelocity * movementSpeed, physicsManager.velocity.y);
                    break;
                case MovementType.Rigidbody:
                    if (rigidbody == null) AddSetRigidBody();
                    rigidbody.velocity = new Vector2(horizontalVelocity * movementSpeed, rigidbody.velocity.y);
                    break;
            }
        }

        public void SetHorizontalVelocity(float horizontalVelocity)
        {
            this.horizontalVelocity = horizontalVelocity;
        }

        public float GetMovementSpeed()
        {
            return movementSpeed;
        }

        public void SetMovementSpeed(float movementSpeed)
        {
            this.movementSpeed = movementSpeed;
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
}
