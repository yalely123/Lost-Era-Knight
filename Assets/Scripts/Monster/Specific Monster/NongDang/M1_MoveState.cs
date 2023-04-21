using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_MoveState : MoveState
{
    private Monster1 monster;

    public M1_MoveState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_MoveState stateData, Monster1 monster) 
        : base(entity, finiteStateMachine, animBoolName, stateData)
    {
        this.monster = monster;

    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);
        monster.isAlert = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isDetectingLedge || isDetectingWall)
        {
            // go to idle State
            monster.idleState.SetFlipAfterIdle(true);
            finiteStateMachine.ChangeState(monster.idleState);
        } 
        else if (isPlayerInAlertRange)
        {
            // go to PlayerDetectedState
            finiteStateMachine.ChangeState(monster.playerDetectedState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
