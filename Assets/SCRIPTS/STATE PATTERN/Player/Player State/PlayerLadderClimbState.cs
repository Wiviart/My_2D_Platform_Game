using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderClimbState : IState
{
    PlayerStateManager player;
    public PlayerLadderClimbState(PlayerStateManager player)
    {
        this.player = player;
    }


    public void EnterState()
    {
        // player.playerAnimation.PlayAnimatorClip("Ladder");
        player.soundEffect.PlayAudio(0);
    }

    public void ExitState()
    {

    }

    public void FixedUpdateState()
    {

    }

    public void UpdateState()
    {
        player.playerController.Movement();

        if (player.inputController.inputY != 0)
            player.playerAnimation.PlayAnimatorClip("Ladder Climb");
        else
            player.playerAnimation.PlayAnimatorClip("Ladder");
    }
}
