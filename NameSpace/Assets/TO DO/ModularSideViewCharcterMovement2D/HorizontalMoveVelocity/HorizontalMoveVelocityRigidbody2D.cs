using UnityEngine;

namespace TheAshBot.TwoDimentional.SideViewCharcterMovement
{
    public class HorizontalMoveVelocityRigidbody2D : MonoBehaviour, IHorizontalMoveVelocity2D
    {


        [SerializeField] private float movementSpeed = 5;


        private float horizontalVelocity;
        private new Rigidbody2D rigidbody;


        private void Start()
        {
            if (!TryGetComponent(out rigidbody))
            {
                rigidbody = gameObject.AddComponent<Rigidbody2D>();

                rigidbody.freezeRotation = true;
            }
        }

        private void FixedUpdate()
        {
            rigidbody.velocity = new Vector2(horizontalVelocity * movementSpeed, rigidbody.velocity.y);
        }



        public float GetMovementSpeed()
        {
            return movementSpeed;
        }

        public void SetHorizontalVelocity(float horizontalVelocity)
        {
            this.horizontalVelocity = horizontalVelocity;
        }
    }
}
