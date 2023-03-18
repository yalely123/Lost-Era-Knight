using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_PlayerDetectedState : PlayerDetectedState
{
    private Monster1 monster;
    public M1_PlayerDetectedState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_PlayerDetectedState stateData, Monster1 monster) 
        : base(entity, finiteStateMachine, animBoolName, stateData)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // if player not in alert range change to idle state
        if (!isPlayerInAlertRange)
        {
            monster.idleState.SetFlipAfterIdle(false);
            finiteStateMachine.ChangeState(monster.idleState);
        }
        else if (isPlayerInAttackRange)
        {
            finiteStateMachine.ChangeState(monster.chooseAttackStyleState);
        }
        else if (!isPlayerInAttackRange)
        {
            finiteStateMachine.ChangeState(monster.chasePlayerState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
