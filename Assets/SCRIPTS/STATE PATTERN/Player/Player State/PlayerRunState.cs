using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IState
{

    PlayerStateManager player;
    Rigidbody2D rigid;
    public PlayerRunState(PlayerStateManager player)
    {
        this.player = player;
        rigid = player.GetComponent<Rigidbody2D>();
    }



    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip("Run");
        player.soundEffect.PlayAudio(1);
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
        player.playerController.Movement();
    }

    public void UpdateState()
    {
        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.playerCollision.isGrounded)
        {
            if (player.inputController.inputX == 0)
            {
                if (Mathf.Abs(rigid.velocity.x) < 5)
                    player.SwitchState(player.walkState);
            }

            if (player.inputController.isJumpPress)
                player.SwitchState(player.jumpState);

            if (player.inputController.isLeftMousePress)
                player.SwitchState(player.attackState);

            if (player.inputController.inputY < 0)
                player.SwitchState(player.crouchState);


            if (player.inputController.isDashPress)
                player.SwitchState(player.dashState);
        }

        if (!player.playerCollision.isGrounded)
        {
            player.SwitchState(player.fallState);

            if (player.playerCollision.isLeftWall || player.playerCollision.isRightWall)
                player.SwitchState(player.wallSlideState);
        }

        // if (player.playerInteract.isClimbing) player.SwitchState(player.ladderState);

    }

}
