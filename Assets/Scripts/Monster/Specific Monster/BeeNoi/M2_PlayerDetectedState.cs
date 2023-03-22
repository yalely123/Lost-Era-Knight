using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_PlayerDetectedState : PlayerDetectedState
{
    private Monster2 monster;
    private bool isPlayerInAlertCircleRange;
    private bool isPlayerInAttackCircleRange;
    private bool isNeedToFlip;
    public M2_PlayerDetectedState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_PlayerDetectedState stateData, Monster2 monster) 
        : base(entity, finiteStateMachine, animBoolName, stateData)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        base.Enter();
        isPlayerInAlertCircleRange = entity.CheckPlayerInAlertCircleRange();
        isPlayerInAttackCircleRange = entity.CheckPlayerInAttackCircleRange();
        monster.isAlert = true;
        isNeedToFlip = entity.CheckIfNeedToFlip();
        //Debug.Log("Bee: Enter PlayerDetectedState");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Debug.Log(canPerformAction);
        if (isNeedToFlip)
        {
            entity.Flip();
        }
        if (canPerformAction)
        {
            
            if (isPlayerInAlertCircleRange)
            {
                finiteStateMachine.ChangeState(monster.chasePlayerState);
            }
            if (!isPlayerInAlertCircleRange)
            {
            finiteStateMachine.ChangeState(monster.idleState);
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        isPlayerInAlertCircleRange = entity.CheckPlayerInAlertCircleRange();
        isPlayerInAttackCircleRange = entity.CheckPlayerInAttackCircleRange();
        isNeedToFlip = entity.CheckIfNeedToFlip();
    }
}
