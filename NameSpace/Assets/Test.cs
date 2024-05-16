using TheAshBot.TwoDimensional.SideViewCharacterMovement;

using UnityEngine;

public class Test : MonoBehaviour
{
    IHorizontalMoveVelocity2D horizontalMoveVelocity2D;

    private void Start()
    {

        horizontalMoveVelocity2D = GetComponent<IHorizontalMoveVelocity2D>();
    }



    private void Update()
    {
        Application.targetFrameRate = 60;
        float horizontalVelocity = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalVelocity--;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalVelocity++;
        }

        horizontalMoveVelocity2D.SetHorizontalVelocity(horizontalVelocity);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<IJumpVelocity2D>().Jump();
        }

    }

}
