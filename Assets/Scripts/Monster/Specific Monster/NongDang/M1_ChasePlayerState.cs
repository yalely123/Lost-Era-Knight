using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_ChasePlayerState : ChasePlayerState
{
    private Monster1 monster;
    private bool isPlayerInChasingCircleRange;
    private bool isNeedToFlip;
    public M1_ChasePlayerState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_ChasePlayerState stateData, Monster1 monster) 
        : base(entity, finiteStateMachine, animBoolName, stateData)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        base.Enter();
        isPlayerInChasingCircleRange = monster.CheckPlayerInAlertCircleRange();
        isNeedToFlip = monster.CheckIfNeedToFlip();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isNeedToFlip = monster.CheckIfNeedToFlip();

        if ((isPlayerInChasingCircleRange || isPlayerInAlertRange) && !isPlayerInAttackRange)
        {
            // Do move toward player.
            if (isNeedToFlip)
            {
                
                entity.Flip();
                //finiteStateMachine.ChangeState(monster.coolDownState);
            }
            entity.SetVelocity(stateData.chaseSpeed);
        }
        else if (isPlayerInAttackRange)
        {
            // Do change to attack state
            entity.SetVelocity(0f);
            finiteStateMachine.ChangeState(monster.chooseAttackStyleState);
        }
        else if (!isPlayerInAlertRange)
        {
            // Do change to Idle state
            monster.idleState.SetFlipAfterIdle(false);
            finiteStateMachine.ChangeState(monster.idleState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        isPlayerInChasingCircleRange = monster.CheckPlayerInAlertCircleRange();
        
    }
}
