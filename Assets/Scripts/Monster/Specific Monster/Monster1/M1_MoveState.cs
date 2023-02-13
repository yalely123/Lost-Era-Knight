using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_MoveState : MoveState
{
    Monster1 monster;

    public M1_MoveState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_MoveState stateData, Monster1 monster) 
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
        if (!isDetectingLedge || isDetectingWall)
        {
            //TODO: go to idle State;
            monster.idleState.SetFlipAfterIdle(true);
            finiteStateMachine.ChangeState(monster.idleState);
        } 
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
