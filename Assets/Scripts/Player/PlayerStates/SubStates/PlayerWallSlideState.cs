using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    private bool jumpInput;
    
    
    

    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        jumpInput = player.InputHandler.JumpInput;
        
        player.SetVelocityY(-playerData.wallSlideVelocity);

        if (jumpInput)
        {
            stateMachine.ChangeState(player.WallJumpState);
        }
    }

    

}
