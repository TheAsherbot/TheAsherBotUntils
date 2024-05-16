using UnityEngine;

namespace TheAshBot.TwoDimensional.TopDownCharacterMovement
{
    public class TopDownCharacterMovement2D : MonoBehaviour
    {


        public void AddMovementVelocity()
        {
            gameObject.AddComponent<TopDownCharacterMovementVelocity2D>();
        }
        public void AddMovementInput()
        {
            gameObject.AddComponent<TopDownCharacterMovementInput2D>();
        }
        public void AddMovementPosition()
        {
            gameObject.AddComponent<TopDownCharacterMovementPosition2D>();
        }


    }
}
