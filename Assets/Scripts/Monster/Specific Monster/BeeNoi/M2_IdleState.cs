using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_IdleState : IdleState
{
    private Monster2 monster;
    private bool isPlayerInAlartRange;
    public M2_IdleState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_IdleState stateData, Monster2 monster) : base(entity, finiteStateMachine, animBoolName, stateData)
    {
        this.monster = monster;
    } 

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Bee: Enter Idle State");
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
        if (isPlayerInAlartRange)
        {
            // TODO: change to player Detected State
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        isPlayerInAlartRange = entity.CheckPlayerInAlertCircleRange();
    }
}
