using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_IdleState stateData;
    protected bool isPlayerInAttRange;
    protected float flipTime;
    protected bool flipAfterIdle;
    protected float idleTime;
    protected bool isIdleTimeOver;
    protected bool isPlayerInAlertRange;

    public IdleState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_IdleState stateData)
        : base(entity, finiteStateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
        isPlayerInAlertRange = entity.CheckPlayeInAlertRange();

    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time > startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        isPlayerInAlertRange = entity.CheckPlayeInAlertRange();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }

}
