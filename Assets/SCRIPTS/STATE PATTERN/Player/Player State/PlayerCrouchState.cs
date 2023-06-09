using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : IState
{

    PlayerStateManager player;
    public PlayerCrouchState(PlayerStateManager player)
    {
        this.player = player;
    }

    float timer = 0;
    bool isMoving;

    public void EnterState()
    {
        player.playerController.EnterCrouch();
    }

    public void ExitState()
    {
        player.playerController.ExitCrouch();
    }

    public void FixedUpdateState()
    {
        CrouchMove();
    }

    public void UpdateState()
    {
        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        timer += Time.deltaTime;

        if (player.inputController.inputY >= 0)
        {
            if (player.inputController.inputX != 0) timer = 1f;

            if (timer < 0.5f) return;

            timer = 0;
            player.SwitchState(player.idleState);
        }

        if (player.inputController.inputY < 0 && player.inputController.inputX != 0)
            isMoving = true;
        else isMoving = false;
    }

    void CrouchMove()
    {
        if (!isMoving)
        {
            player.playerAnimation.PlayAnimatorClip(player.playerDatabase.CROUCH);
            return;
        }

        player.playerAnimation.PlayAnimatorClip(player.playerDatabase.CROUCH_MOVE);

        player.playerController.Movement();
    }
}
