using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_idleState stateData;
    protected bool isPlayerInAttRange;
    protected float flipTime;

    public IdleState(Entity entity, FiniteStateMachine finiteStateMachine, D_idleState stateData) : base(entity, finiteStateMachine)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        setRandomFlipTime();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time > startTime + flipTime)
        {
            entity.Flip();
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    private void setRandomFlipTime()
    {
        flipTime = Random.Range(stateData.minFlipTime, stateData.maxFlipTime);
    }
}
