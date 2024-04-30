using UnityEngine;

public class JumpVelocityRigidbody2D : MonoBehaviour, IJumpVelocity2D
{

    [SerializeField] private float jumpHeight;

    private new Rigidbody2D rigidbody;



    private void Start()
    {
        if (!TryGetComponent(out rigidbody))
        {
            rigidbody = gameObject.AddComponent<Rigidbody2D>();
        }
    }


    public float GetJumpHeight()
    {
        return jumpHeight;
    }

    public void Jump()
    {
        rigidbody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
    }

}
