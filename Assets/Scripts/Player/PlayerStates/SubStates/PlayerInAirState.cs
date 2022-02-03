using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //Input
    private float xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool dashInput;

    //Checks
    private bool isTouchingWall;
    private bool isJumping;
    private bool isGrounded;

    private float minJumpTimer;
    private bool coyoteTime;

    //protected bool wallJumpCoyoteTime;
    //private float startWallJumpCoyoteTime;
    //private bool oldIsTouchingWall;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();

        if (player.FacingDirection > 0)
        {
            player.lastJumpLocation = new Vector2(player.transform.position.x-0.75f, player.transform.position.y);
        }
        else if (player.FacingDirection < 0)
        {
            player.lastJumpLocation = new Vector2(player.transform.position.x+0.75f, player.transform.position.y);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        xInput = player.InputHandler.MovementInput;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        dashInput = player.InputHandler.DashInput;

        minJumpTimer -= Time.deltaTime;
        CheckJumpMultiplier();

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingWall && (xInput == player.FacingDirection || xInput == 0) && player.CurrentVelocity.y <= 0) 
        {
           stateMachine.ChangeState(player.WallSlideState);
        }
        else if (jumpInput && player.JumpState.CanJump() && !isTouchingWall)  
        {
            player.InputHandler.UseJumpInput();
            player.Anim.SetTrigger("doubleJump");
            stateMachine.ChangeState(player.JumpState);
            coyoteTime = false;
        }
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
        //else if (PlayerHealthController.instance.tookDamage == true)
        //{
        //    stateMachine.ChangeState(player.KnockbackState);
        //}
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);
            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        }

    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping && minJumpTimer <= 0)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseRemainingJumps();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void SetIsJumping()
    {
        isJumping = true;
        minJumpTimer = playerData.minJumpTime;
    }

    //    private void CheckWallJumpCoyoteTime()
    //    {
    //        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
    //        {
    //            wallJumpCoyoteTime = false;
    //        }
    //    }
    //    public void StartWallJumpCoyoteTime()
    //    {
    //        wallJumpCoyoteTime = true;
    //        startWallJumpCoyoteTime = Time.time;
    //    }
    //    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;
    //}
}