using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellCastState : IState
{
    PlayerStateManager player;
    public PlayerSpellCastState(PlayerStateManager player)
    {
        this.player = player;
    }
    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(player.playerDatabase.SPELL);
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
        if (player.playerAnimation.CurrentClipNormalize() < 1f) return;

        player.SwitchState(player.idleState);
    }
}
