using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_ShootStingState : ShootProjectileState
{
    private Monster2 monster;
    public M2_ShootStingState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, Transform attackPosition, D_ShootProjectileState stateData, Monster2 monster) 
        : base(entity, finiteStateMachine, animBoolName, attackPosition, stateData)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        base.Enter();
        // do Shoot sting
       // Debug.Log("Bee: Enter Shoot sting state");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
        finiteStateMachine.ChangeState(monster.idleState);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time > startTime + stateData.waitBeforeDuration && !isTrigger)
        {
            TriggerAttack();
        }
        if ((Time.time > triggerTime+ stateData.performDuration && isTrigger) || isAnimationFinished)
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
        //Debug.Log("Shoot Sting Action!!");
    }
}
