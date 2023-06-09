using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : IState
{
    PlayerStateManager player;
    string WALLSLIDE = "Wall Slide";



    public PlayerWallSlideState(PlayerStateManager player)
    {
        this.player = player;
    }



    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(WALLSLIDE);

        player.soundEffect.PlayAudio(0);
        player.playerController.UnGravity(0.05f);
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
        player.playerController.MoveOnAir();
        // playerWall.WallSlide();
    }

    public void UpdateState()
    {
        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.inputController.isJumpPress)
            player.SwitchState(player.wallJumpState);

        if (player.playerCollision.isGrounded)
            player.SwitchState(player.idleState);

        if (player.playerCollision.isLeftEdge || player.playerCollision.isRightEdge)
            player.SwitchState(player.wallEdge);

        if (!player.playerCollision.isLeftWall && !player.playerCollision.isRightWall)
            player.SwitchState(player.fallState);
    }
}
