using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public PlayerInput swipeController;

    public abstract IEnumerator UpdateState(PlayerMovement playerMovement);

    //protected abstract void ExitState(PlayerMovement playerMovement);
}
