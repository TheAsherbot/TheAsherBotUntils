using UnityEngine;

namespace TheAshBot.TwoDimensional.TopDownCharcterMovement
{
    public interface IMoveVelocity2D
    {

        public void SetVelocity(Vector3 velocityVector);

        public float GetMovementSpeed();

    }
}
