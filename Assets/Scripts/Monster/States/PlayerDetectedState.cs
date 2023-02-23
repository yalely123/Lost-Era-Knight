using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;
    protected bool isPlayerInAlertRange;
    protected bool isPlayerInAttackRange;
    protected bool canPerformAction;

    public PlayerDetectedState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_PlayerDetectedState stateData) 
        : base(entity, finiteStateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Now, monster1 is in PlayerDetected State");
        entity.SetVelocity(0f); // to make monster stop moving when enter playerDetected state
        isPlayerInAlertRange = entity.CheckPlayeInAlertRange();
        isPlayerInAttackRange = entity.CheckPlayerInAttackRange();
        canPerformAction = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time > startTime + stateData.actionTime)
        {
            canPerformAction = true;
        }
       
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        isPlayerInAlertRange = entity.CheckPlayeInAlertRange();
        isPlayerInAttackRange = entity.CheckPlayerInAttackRange();
    }
}
