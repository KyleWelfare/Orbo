using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        DetermineWallJumpDirection();
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        player.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseRemainingJumps();
        player.InAirState.SetIsJumping();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
        
    }

    public void DetermineWallJumpDirection()
    {
        if (player.wallPos.point.x < player.transform.position.x)
        {
            wallJumpDirection = 1;
        }
        else if (player.wallPos.point.x > player.transform.position.x)
        {
            wallJumpDirection = -1;
        }
    }
}
