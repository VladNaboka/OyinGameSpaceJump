using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public override IEnumerator UpdateState(PlayerMovement playerMovement)
    {
        if (SwipeController.swipeDown && playerMovement.IsLose == false)
        {
            playerMovement.SwitchState(playerMovement.slideState);
        }
        playerMovement.playerVelocity.y = Mathf.Sqrt(playerMovement.JumpPower * -3.0f * playerMovement.GravityValue);
        playerMovement.Anim.SetBool("isRunning", false);
        playerMovement.Anim.SetTrigger("Jump");
        yield return null;
    }
}
