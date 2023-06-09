using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallEdgeState : IState
{
    PlayerStateManager player;
    PlayerCollisionDetector playerCollision;
    Rigidbody2D playerRigid;
    string GRAB = "Wall Grab";
    int ledgeSide;

    public PlayerWallEdgeState(PlayerStateManager player, PlayerCollisionDetector playerCollision)
    {
        this.player = player;
        playerRigid = player.GetComponent<Rigidbody2D>();
        this.playerCollision = playerCollision;
    }
    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(GRAB);

    }

    public void ExitState()
    {

    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
        WallEdgeGrab();

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.inputController.inputY > 0)
            player.SwitchState(player.wallClimb);

        if (player.inputController.inputY < 0)
            player.SwitchState(player.wallSlideState);

        if (player.inputController.isJumpPress)
            player.SwitchState(player.wallJumpState);
    }

    void WallEdgeGrab()
    {
        ledgeSide = playerCollision.GetLedgeSide();

        if (ledgeSide == 0) return;
        
        playerRigid.velocity = Vector2.zero;
        player.transform.localScale = new Vector3(ledgeSide, 1, 1);
    }
}
