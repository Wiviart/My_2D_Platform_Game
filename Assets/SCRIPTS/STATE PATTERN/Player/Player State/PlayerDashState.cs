using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : IState
{

    PlayerStateManager player;
    PlayerDash playerDash;
    InputControllerNew inputController;
    PlayerDatabase playerDatabase;
    Rigidbody2D rigid;
    float dashTimer;
    public PlayerDashState(PlayerStateManager player, PlayerDash playerDash)
    {
        this.player = player;
        this.playerDash = playerDash;
        inputController = player.GetComponent<InputControllerNew>();
        playerDatabase = player.GetComponent<PlayerDatabase>();
        rigid = player.GetComponent<Rigidbody2D>();
    }


    string DASH = "Wall Jump";


    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(DASH);
        Dash();
    }

    public void ExitState()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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

        if (player.playerAnimation.CheckCurrentClip(DASH)
        && player.playerAnimation.CurrentClipNormalize() > player.playerDatabase.dashingTime)
        {
            player.SwitchState(player.idleState);
        }
    }

    void Dash()
    {
        if (dashTimer < playerDatabase.dashingCooldown) return;

        float dashX;

        if (inputController.inputXRaw != 0)
            dashX = inputController.inputXRaw;
        else
            dashX = player.transform.localScale.x;

        rigid.AddForce(new Vector2(dashX, inputController.inputYRaw) * playerDatabase.dashingPower, ForceMode2D.Impulse);

        playerDatabase.afterImage.DisplaySprite();

        dashTimer = 0;
    }
    void LastDashTime()
    {
        dashTimer += Time.deltaTime;
    }
}
