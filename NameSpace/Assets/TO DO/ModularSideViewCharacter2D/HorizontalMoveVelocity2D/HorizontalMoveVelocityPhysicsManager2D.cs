using UnityEngine;

namespace TheAshBot.TwoDimensional.SideViewCharacterMovement
{
    public class HorizontalMoveVelocityPhysicsManager2D : MonoBehaviour, IHorizontalMoveVelocity2D
    {

        [SerializeField] private float movementSpeed = 5;


        private PhysicsManager2D physicsManager;



        private void Start()
        {
            if (!TryGetComponent(out physicsManager))
            {
                physicsManager = gameObject.AddComponent<PhysicsManager2D>();
            }
        }

        public float GetMovementSpeed()
        {
            throw new System.NotImplementedException();
        }

        public void SetHorizontalVelocity(float horizontalVelocity)
        {
            physicsManager.velocity = new Vector2(horizontalVelocity * movementSpeed, physicsManager.velocity.y);
        }
    }
}
