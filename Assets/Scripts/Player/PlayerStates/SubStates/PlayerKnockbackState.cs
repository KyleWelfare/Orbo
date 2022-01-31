using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackState : PlayerAbilityState
{
    public PlayerKnockbackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        if (player.JumpState.amountOfJumpsLeft > 1)
        {
            player.JumpState.DecreaseRemainingJumps();
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
        if (!isExitingState)
        {
            player.SetVelocityX(playerData.knockbackVelocityX * -player.FacingDirection);
            player.SetVelocityY(playerData.knockbackVelocityY);
            

            if (Time.time >= startTime + playerData.knockbackDuration)
            {
                isAbilityDone = true;
                PlayerHealthController.instance.tookDamage = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
