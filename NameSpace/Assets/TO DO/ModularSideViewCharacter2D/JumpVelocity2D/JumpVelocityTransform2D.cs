using UnityEngine;

public class JumpVelocityTransform2D : MonoBehaviour, IJumpVelocity2D
{

    [SerializeField] private float jumpHeight;
    [SerializeField] private AnimationCurve jumpPath;
    [SerializeField] private float jumpTime;

    private float startJumpHeight = 0;
    private float elapsedJumpTime;
    private bool isJumping;
    

    private void Update()
    {
        if (isJumping)
        {
            elapsedJumpTime += Time.deltaTime;
            if (elapsedJumpTime > jumpTime)
            {
                isJumping = false;
                return;
            }

            float percentageComplete = elapsedJumpTime / jumpTime;

            transform.position = new Vector3(0, startJumpHeight + (jumpPath.Evaluate(percentageComplete) * jumpHeight));
        }
    }

    public float GetJumpHeight()
    {
        return jumpHeight;
    }

    public void Jump()
    {
        startJumpHeight = transform.position.y;
        elapsedJumpTime = 0;
        isJumping = true;
    }

}
