using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState1 : IState
{
    PlayerStateManager player;
    PlayerAttackManager playerAttack;
    public PlayerAirAttackState1(PlayerStateManager player, PlayerAttackManager playerAttack)
    {
        this.player = player;
        this.playerAttack = playerAttack;
    }
    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(player.playerDatabase.AIR_ATK_1);
        player.soundEffect.PlayAudio(5);
        playerAttack.AttackCast(4);
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
        if (player.playerAnimation.currentState.normalizedTime <= 0.5f) return;

        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.inputController.isLeftMousePress)
            player.SwitchState(player.airAttackState2);

        if (player.playerAnimation.currentState.normalizedTime <= 1) return;

        player.SwitchState(player.fallState);
    }

}
