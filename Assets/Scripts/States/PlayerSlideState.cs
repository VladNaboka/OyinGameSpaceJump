using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerBaseState
{
    public override IEnumerator UpdateState(PlayerMovement playerMovement)
    {
        if (SwipeController.swipeUp || Input.GetKeyDown(KeyCode.Space) && playerMovement.IsLose == false)
        {
            Debug.Log("Jump");
            if (playerMovement.IsGround && playerMovement.IsLose == false)
                playerMovement.SwitchState(playerMovement.jumpState);
        }
        playerMovement.Controller.height = 0;
        playerMovement.Controller.center = new Vector3(0, 0.49f, 0);

        playerMovement.Anim.SetTrigger("Slide");
        playerMovement.CameraMov.distance += new Vector3(0, -0.3f, 0);
        yield return new WaitForSeconds(1);

        playerMovement.Controller.height = 1.75f;
        playerMovement.Controller.center = new Vector3(0, 0.86f, 0);

        playerMovement.CameraMov.distance += new Vector3(0, 0.3f, 0);
    }
}
