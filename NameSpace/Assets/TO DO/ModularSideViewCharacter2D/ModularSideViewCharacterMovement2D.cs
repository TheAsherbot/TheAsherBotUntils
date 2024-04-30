using UnityEngine;

public class ModularSideViewCharacterMovement2D : MonoBehaviour
{


    public enum MovementType
    {
        PhysicsManager2D,
        Rigidbody2D,
        Transform,
    }


    [Header("General")]
    [SerializeField] private MovementType movementType;

    

}
