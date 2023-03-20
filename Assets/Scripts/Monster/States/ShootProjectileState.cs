using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileState : AttackState
{
    protected D_ShootProjectileState stateData;
    protected AttackDetail attackDetails;
    protected bool isPlayerInAlertCircleRange;
    protected Vector2 playerAngle;
    protected float triggerTime;
    protected GameObject sting;
    protected Sting stingScript;
    
    public ShootProjectileState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, Transform attackPosition, D_ShootProjectileState stateData) 
        : base(entity, finiteStateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = entity.aliveGO.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        triggerTime = Time.time;
        // TODO: intantiate projectile Object
        sting = GameObject.Instantiate(stateData.bullet, attackPosition.position, attackPosition.rotation);
        stingScript = sting.GetComponent<Sting>();
        stingScript.ShootSting(stateData.bulletSpeed, stateData.bulletLifeTime, stateData.attackDamage);
    }

}
