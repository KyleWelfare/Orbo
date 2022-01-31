using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandRunningState : PlayerGroundedState
{
    public PlayerLandRunningState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlip(xInput);
        

        if (!isExitingState)
        {
            if (isAnimationFinished && xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if (isAnimationFinished && xInput == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocityX(playerData.movementVelocity * xInput);
    }
}