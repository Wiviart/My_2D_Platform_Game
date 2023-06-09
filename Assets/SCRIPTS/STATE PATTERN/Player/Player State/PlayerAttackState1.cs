using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState1 : IState
{
    PlayerStateManager player;
    PlayerAttackManager playerAttack;
    public PlayerAttackState1(PlayerStateManager player, PlayerAttackManager playerAttack)
    {
        this.player = player;
        this.playerAttack = playerAttack;
    }

    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip("Attack 2");
        player.soundEffect.PlayAudio(5);
        playerAttack.AttackCast(1);
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
        if (player.playerAnimation.currentState.normalizedTime <= 0.5f) return;

        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.inputController.isJumpPress)
            player.SwitchState(player.jumpState);

        // if (player.playerAnimation.currentState.normalizedTime <= 0.8f) return;

        if (player.inputController.isLeftMousePress)
            player.SwitchState(player.attackState2);

        if (player.playerAnimation.currentState.normalizedTime <= 1f) return;

        if (!player.inputController.isLeftMousePress)
            player.SwitchState(player.idleState);
    }
}
