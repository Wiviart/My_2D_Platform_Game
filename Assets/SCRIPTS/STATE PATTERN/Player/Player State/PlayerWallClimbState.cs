using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : IState
{
    Collider2D playerColi;
    Rigidbody2D playerRigid;
    PlayerStateManager player;
    PlayerCollisionDetector playerCollision;
    string WALLCLB = "Wall Climb";
    int ledgeSide;
    public Vector2 wallEdgePoint;

    public PlayerWallClimbState(PlayerStateManager player, PlayerCollisionDetector playerCollision)
    {
        this.player = player;
        this.playerCollision = playerCollision;
        playerRigid = player.GetComponent<Rigidbody2D>();
        playerColi = player.GetComponent<Collider2D>();
    }



    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(WALLCLB);

        WallClimb();
    }

    public void ExitState()
    {

    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.playerAnimation.currentState.IsName(WALLCLB)
         && player.playerAnimation.currentState.normalizedTime >= 0.9f)
        {
            player.SwitchState(player.idleState);
        }
    }

    public void WallClimb()
    {
        player.StartCoroutine(WallClimbDelay());
    }

    /**************************************************************************************************************************************************/

    IEnumerator WallClimbDelay()
    {
        GetEdgePoint(ledgeSide);

        yield return new WaitForSeconds(0.5f);

        Vector3 position = new Vector3(
            wallEdgePoint.x + playerColi.bounds.size.x / 2 * player.transform.localScale.x,
            wallEdgePoint.y + playerColi.bounds.size.y / 2 + 0.46f,
            0);

        player.transform.position = position;
    }

    /**************************************************************************************************************************************************/
    void GetEdgePoint(int ledgeSide)
    {
        

        switch (ledgeSide)
        {
            case -1:
                {
                    Collider2D leftHit = playerCollision.ledgePoint;

                    wallEdgePoint = leftHit.bounds.center + new Vector3(leftHit.bounds.size.x / 2, leftHit.bounds.size.y / 2, 0);
                    break;
                }
            case 1:
                {
                    Collider2D rightHit = playerCollision.ledgePoint;

                    wallEdgePoint = rightHit.bounds.center + new Vector3(-rightHit.bounds.size.x / 2, rightHit.bounds.size.y / 2, 0);
                    break;
                }
        }
    }
}
