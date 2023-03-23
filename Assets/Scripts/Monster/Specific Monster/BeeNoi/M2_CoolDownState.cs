using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_CoolDownState : CoolDownState
{
    private Monster2 monster;
    public M2_CoolDownState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_CoolDownState stateData, Monster2 monster) 
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
        if (isFinishCoolDown)
        {
            finiteStateMachine.ChangeState(monster.idleState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
