using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttackState stateData;
    protected AttackDetail attackDetails;
    protected bool isPlayerInAlertRange;
    protected Collider2D playerCollision;
    protected bool isAttackHitPlayer;
    public MeleeAttackState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData) 
        : base(entity, finiteStateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = entity.aliveGO.transform.position;
        isPlayerInAlertRange = entity.CheckPlayeInAlertRange();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
        isAttackHitPlayer = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        isPlayerInAlertRange = entity.CheckPlayeInAlertRange();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, 
                                                                 stateData.attackRadius, 
                                                                 stateData.whatIsPlayer);

        foreach (Collider2D coll in detectedObjects) // for each colliders that overlap the circle
        {
            //coll.transform.SendMessage("Damage", attackDetails);
            if (coll.CompareTag("Player")) {
                // TODO: send damage to player help and knock player back
                //Debug.Log("Melee hit player");
                coll.GetComponent<PlayerHealth>().ReceiveDamage(stateData.attackDamage, entity.aliveGO.transform.position.x);
            }
        }
    }
}
