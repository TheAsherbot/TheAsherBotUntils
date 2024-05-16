using UnityEngine;

public class JumpVelocityPhysicsManager2D : MonoBehaviour, IJumpVelocity2D
{

    [SerializeField] private float jumpHeight;

    private PhysicsManager2D physicsManager;



    private void Start()
    {
        if (!TryGetComponent(out physicsManager))
        {
            physicsManager = gameObject.AddComponent<PhysicsManager2D>();
        }
    }


    public float GetJumpHeight()
    {
        return jumpHeight;
    }

    public void Jump()
    {
        physicsManager.AddForce(new Vector2(0, jumpHeight));
    }

}
