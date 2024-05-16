using UnityEngine;

namespace TheAshBot.TwoDimensional.TopDownCharacterMovement
{
    public class MoveVelocityTransform2D : MonoBehaviour, IMoveVelocity2D
    {


        [SerializeField] private int movementSpeed;


        private Vector3 velocityVector;


        private void Update()
        {
            transform.position += movementSpeed * Time.deltaTime * velocityVector;
        }


        public void SetVelocity(Vector3 velocityVector)
        {
            this.velocityVector = velocityVector;
        }

        public float GetMovementSpeed()
        {
            return movementSpeed;
        }


    }
}
