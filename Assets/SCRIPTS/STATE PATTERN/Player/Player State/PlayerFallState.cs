using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : IState
{

    PlayerStateManager player;
    Rigidbody2D rigid;
    public PlayerFallState(PlayerStateManager player)
    {
        this.player = player;
        rigid = player.GetComponent<Rigidbody2D>();
    }


    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip("Fall");

        player.soundEffect.PlayAudio(0);
    }

    public void ExitState()
    {

    }

    public void FixedUpdateState()
    {
        player.playerController.MoveOnAir();

    }

    public void UpdateState()
    {
        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.playerCollision.isGrounded)
        {
            // if (rigid.velocity.y < -player.playerDatabase.maxFallVelocity * 0.5f)
            {
                player.SwitchState(player.crouchState);
                player.soundEffect.PlayAudio(3);
            }
            // else
            // {
            //     player.SwitchState(player.idleState);
            // }
        }

        if (player.playerCollision.isRightEdge || player.playerCollision.isLeftEdge)
            player.SwitchState(player.wallEdge);

        if (player.playerCollision.isLeftWall || player.playerCollision.isRightWall)
            player.SwitchState(player.wallSlideState);



        if (player.inputController.isLeftMousePress)
        {
            player.SwitchState(player.airAttackState);
        }

        if (player.inputController.isDashPress)
        {
            player.SwitchState(player.dashState);
        }

        // if (player.playerInteract.isClimbing) player.SwitchState(player.ladderState);
    }
}
