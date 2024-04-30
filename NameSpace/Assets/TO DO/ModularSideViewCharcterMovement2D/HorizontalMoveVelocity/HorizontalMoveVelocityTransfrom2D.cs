using UnityEngine;

namespace TheAshBot.TwoDimentional.SideViewCharcterMovement
{
    public class HorizontalMoveVelocityTransfrom2D : MonoBehaviour, IHorizontalMoveVelocity2D
    {


        [SerializeField] private float movementSpeed = 5;


        private float horizontalVelocity;


        private void Update()
        {
            transform.position += new Vector3(horizontalVelocity * movementSpeed * Time.deltaTime, 0);
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
