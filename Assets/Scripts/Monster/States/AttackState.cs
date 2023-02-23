using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected bool isTrigger;
    public AttackState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, Transform attackPosition) 
        : base(entity, finiteStateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();
        isAnimationFinished = false;
        isTrigger = false;
        entity.atsm.attackState = this;
        entity.SetVelocity(0f);
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
    }

    public virtual void TriggerAttack()
    {
        isTrigger = true;
    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
        isTrigger = false;
    }
}
