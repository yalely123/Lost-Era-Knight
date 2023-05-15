using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownState : State
{
    protected D_CoolDownState stateData;
    protected bool isFinishCoolDown;

    public CoolDownState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_CoolDownState stateData) 
        : base(entity, finiteStateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Moving Stop!");
        entity.SetVelocity(0f);
        isFinishCoolDown = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time > startTime + stateData.duration)
        {
            isFinishCoolDown = true;
        }
        else
        {
            isFinishCoolDown = false;
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
