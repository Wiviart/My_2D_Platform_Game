using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IState
{
    PlayerStateManager player;
    PlayerJump playerJump;
    public PlayerJumpState(PlayerStateManager player)
    {
        this.player = player;

        playerCollision = player.GetComponent<PlayerCollisionDetector>();
        playerController = player.GetComponent<PlayerController>();
        playerDatabase = player.GetComponent<PlayerDatabase>();
        rigid = player.GetComponent<Rigidbody2D>();
        coli = player.GetComponent<Collider2D>();
    }

    PlayerDatabase playerDatabase;
    PlayerController playerController;
    PlayerCollisionDetector playerCollision;
    Rigidbody2D rigid;
    Collider2D coli;
    int jumpCount;
    float timer;
    float lastGroundTime;

    public void EnterState()
    {
        jumpCount = player.playerDatabase.jumpNumber;
        Jump();

        player.playerAnimation.PlayAnimatorClip(player.playerDatabase.JUMP);
        player.soundEffect.PlayAudio(2);

    }


    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
        player.playerController.MoveOnAir();
    }

    public void UpdateState()
    {
        CheckJumpCondition();

        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (Mathf.Abs(rigid.velocity.y) < 2)
            if (!player.playerCollision.isLeftWall && !player.playerCollision.isRightWall)
                player.SwitchState(player.onAirState);

        if (player.playerCollision.isLeftWall || player.playerCollision.isRightWall)
            player.SwitchState(player.wallSlideState);

        if (player.playerCollision.isLeftEdge || player.playerCollision.isRightEdge)
            player.SwitchState(player.wallEdge);

        if (player.inputController.isLeftMousePress)
            player.SwitchState(player.airAttackState);


        if (player.inputController.isDashPress)
            player.SwitchState(player.dashState);
    }

    void CheckJumpCondition()
    {
        CoyoteJumpCheck();
        JumpCut();
        JumpReset();
    }
  

    void Jump()
    {
        if (jumpCount <= 0) return;
        timer = 0;

        rigid.velocity = new Vector2(rigid.velocity.x, playerDatabase.jumpForce);

        jumpCount--;
    }

    void JumpReset()
    {
        if (Input.GetKey(KeyCode.Space)) return;

        if (playerCollision.isGrounded) jumpCount = playerDatabase.jumpNumber;
    }
    public void StopJump(float time)
    {
        player.StartCoroutine(StopJumpCoroutine(time));
    }

    void JumpCut()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f) return;

        if (Input.GetKeyUp(KeyCode.Space))
            player.StartCoroutine(StopJumpCoroutine(0));
    }

    void CoyoteJumpCheck()
    {
        if (playerCollision.isGrounded)
            lastGroundTime = 0f;
        else lastGroundTime += Time.deltaTime;

        if (lastGroundTime <= playerDatabase.maxCoyoteTime)
            playerCollision.isGrounded = true;
    }

    IEnumerator StopJumpCoroutine(float time)
    {
        yield return new WaitForSeconds(time);

        rigid.velocity = new Vector2(rigid.velocity.x, 0);
    }

    
}
