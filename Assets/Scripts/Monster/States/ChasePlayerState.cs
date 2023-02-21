using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerState : State
{
    protected D_ChasePlayerState stateData;
    protected bool isPlayerInAlertRange;
    protected bool isPlayerInAttackRange;
    public ChasePlayerState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_ChasePlayerState stateData) : base(entity, finiteStateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        isPlayerInAlertRange = entity.CheckPlayeInAlertRange();
        isPlayerInAttackRange = entity.CheckPlayerInAttackRange();
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
        isPlayerInAlertRange = entity.CheckPlayeInAlertRange();
        isPlayerInAttackRange = entity.CheckPlayerInAttackRange();
    }
}
