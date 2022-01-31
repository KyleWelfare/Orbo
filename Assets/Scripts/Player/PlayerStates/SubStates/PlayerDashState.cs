using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }
    public float lastDashTime;
    private float dashDirection;

    public float oldXPos;

    

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.InputHandler.UseDashInput();

        dashDirection = player.FacingDirection;
        if (player.CheckIfTouchingWall())
        {
            dashDirection *= -1;
        }

        startTime = Time.time;

    }

    public override void Exit()
    {
        base.Exit();     
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!isExitingState)
        {
            player.RB.gravityScale = 0;
            player.SetVelocityX(playerData.dashVelocity * dashDirection);
            player.SetVelocityY(0f);
            player.CheckIfShouldFlip(dashDirection);

            if (Time.time >= startTime + playerData.dashDuration || player.CheckIfTouchingWall())
            {
                player.RB.gravityScale = 4.5f;
                player.SetVelocityX(0);
                isAbilityDone = true;
                lastDashTime = Time.time;
            }
        } 
    }
    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }

    public void ResetCanDash() => CanDash = true;


}
