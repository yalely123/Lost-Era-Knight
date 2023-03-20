using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInAlertRange;

    public MoveState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_MoveState stateData) 
        : base (entity, finiteStateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void Enter()
    {
        base.Enter();
        //entity.SetVelocity(stateData.movementSpeed);

        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
        isPlayerInAlertRange = entity.CheckPlayeInAlertRange();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
        isPlayerInAlertRange = entity.CheckPlayeInAlertRange();
        
    }
}
