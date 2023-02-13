using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_IdleState : IdleState
{
    private Monster1 monster;

    public M1_IdleState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_IdleState stateData, Monster1 monster) 
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

        if (isIdleTimeOver)
        {
            finiteStateMachine.ChangeState(monster.moveState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
