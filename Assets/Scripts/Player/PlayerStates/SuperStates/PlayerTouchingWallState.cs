using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected float xInput;
    private bool dashInput;


    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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

        player.InputHandler.UseDashInput();
        player.JumpState.ResetRemainingJumps();
        player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.MovementInput;
        dashInput = player.InputHandler.DashInput;

        if (isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || (xInput != player.FacingDirection && xInput != 0))
        {
            stateMachine.ChangeState(player.InAirState);
            player.JumpState.DecreaseRemainingJumps();
        }
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
        //else if (PlayerHealthController.instance.tookDamage == true)
        //{
        //    stateMachine.ChangeState(player.KnockbackState);
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
