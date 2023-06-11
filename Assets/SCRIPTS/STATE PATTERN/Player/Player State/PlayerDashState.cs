using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : IState
{

    PlayerStateManager player;
    InputControllerNew inputController;
    PlayerDatabase playerDatabase;
    Rigidbody2D rigid;
    float timer;

    public PlayerDashState(PlayerStateManager player)
    {
        this.player = player;
        inputController = player.inputController;
        playerDatabase = player.playerDatabase;
        rigid = player.rigid;
    }

    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(player.playerDatabase.DASH);
        Dash();
    }

    public void ExitState()
    {
        rigid.velocity = Vector2.zero;
    }

    public void FixedUpdateState()
    {

    }

    public void UpdateState()
    {
        LastDashTime();

        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (timer > player.playerDatabase.dashingTime)
            player.SwitchState(player.idleState);
    }

    void Dash()
    {
        if (player.playerController.dashTimer < player.playerDatabase.dashingCooldown) return;
       
        player.playerController.dashTimer = 0;
        timer = 0;

        float dashX;

        if (inputController.inputXRaw != 0)
            dashX = inputController.inputXRaw;
        else
            dashX = player.transform.localScale.x;

        rigid.AddForce(new Vector2(dashX, inputController.inputYRaw) * playerDatabase.dashingPower, ForceMode2D.Impulse);

        playerDatabase.afterImage.DisplaySprite();
    }
    void LastDashTime()
    {
        timer += Time.deltaTime;
    }
}
