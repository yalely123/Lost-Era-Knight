using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_MeleeAttackState : MeleeAttackState
{
    private Monster1 monster;
   
    public M1_MeleeAttackState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Monster1 monster) 
        : base(entity, finiteStateMachine, animBoolName, attackPosition, stateData)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
        finiteStateMachine.ChangeState(monster.playerDetectedState);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // current logic will rework when combine with animation
        if (Time.time > startTime + stateData.waitBeforeperformDuration && !isTrigger)
        {
            TriggerAttack();
        }
        else if (Time.time > startTime + stateData.performDuration && isTrigger)
        {
            FinishAttack();
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        // isTrigger = true;
    }
}
