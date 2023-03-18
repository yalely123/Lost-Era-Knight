using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_MoveState : MoveState
{
    private Monster2 monster;

    public M2_MoveState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_MoveState stateData, Monster2 monster) 
        : base(entity, finiteStateMachine, animBoolName, stateData)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        base.Enter();
        isPlayerInAlertRange = entity.CheckPlayerInAlertCircleRange();
        Debug.Log("Bee: Enter MoveState");
        entity.SetVelocity(stateData.movementSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time > startTime + stateData.maxMoveTime)
        {
            // move time is over then set flip after Idle and change to Idle State
            monster.idleState.SetFlipAfterIdle(true);
            finiteStateMachine.ChangeState(monster.idleState);

        }
        if (isPlayerInAlertRange)
        {
            // TODO: change to player Detected State
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        isPlayerInAlertRange =  entity.CheckPlayerInAlertCircleRange();
    }
}
