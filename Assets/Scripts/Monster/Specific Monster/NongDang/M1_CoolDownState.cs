using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_CoolDownState : CoolDownState
{
    private Monster1 monster;
    public M1_CoolDownState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_CoolDownState stateData, Monster1 monster) 
        : base(entity, finiteStateMachine, animBoolName, stateData)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        base.Enter();
        // Debug.Log("Enter cool down state");
        entity.SetVelocity(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("isFinishCoolDown?: " + isFinishCoolDown);
        if (isFinishCoolDown)
        {
            finiteStateMachine.ChangeState(monster.playerDetectedState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
