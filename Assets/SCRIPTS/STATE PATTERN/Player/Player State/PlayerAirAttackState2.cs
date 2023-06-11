using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState2 : IState
{
    PlayerStateManager player;
    PlayerAttackManager playerAttack;
    public PlayerAirAttackState2(PlayerStateManager player, PlayerAttackManager playerAttack)
    {
        this.player = player;
        this.playerAttack = playerAttack;
    }
    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(player.playerDatabase.AIR_ATK_2);
        player.soundEffect.PlayAudio(5);
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
        playerAttack.AttackCast(5);

        if (player.playerAnimation.currentState.normalizedTime <= 0.5f) return;

        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.playerAnimation.currentState.normalizedTime <= 1) return;

        player.playerAnimation.PlayAnimatorClip(player.playerDatabase.AIR_ATK_2_LOOP);

        if (player.playerCollision.isGrounded)
        {
            player.playerAnimation.PlayAnimatorClip(player.playerDatabase.AIR_ATK_2_END);
            playerAttack.AttackCast(6);
        }

        if (player.playerAnimation.CheckCurrentClip(player.playerDatabase.AIR_ATK_2_END) && player.playerAnimation.currentState.normalizedTime > 1)
            player.SwitchState(player.idleState);
    }

}
