using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : Entity
{
    public M2_IdleState idleState { get; private set; }
    public M2_MoveState moveState { get; private set; }
    public M2_PlayerDetectedState playerDetectedState { get; private set; } 
    public M2_ChasePlayerState chasePlayerState { get; private set; }
    public M2_ChooseAttackStyleState chooseAttackStyleState { get; private set; }
    public M2_ShootStingState shootStingState { get; private set; }
    public M2_CoolDownState coolDownState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField]
    private D_ChasePlayerState chasePlayerStateData;
    [SerializeField]
    private D_ChooseAttackStyleState chooseAttackStyleStateData;
    [SerializeField]
    private D_ShootProjectileState shootProjectileStateData;
    [SerializeField]
    private D_CoolDownState coolDownStateData;

    [SerializeField]
    private Transform attackPosition;

    public override void Start()
    {
        base.Start();

        idleState = new M2_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new M2_MoveState(this, stateMachine, "fly", moveStateData, this);
        playerDetectedState = new M2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chasePlayerState = new M2_ChasePlayerState(this, stateMachine, "chasePlayer", chasePlayerStateData, this);
        chooseAttackStyleState = new M2_ChooseAttackStyleState(this, stateMachine, "chooseAttackStyle", chooseAttackStyleStateData, this);
        shootStingState = new M2_ShootStingState(this, stateMachine, "shootSting", attackPosition, shootProjectileStateData, this);
        coolDownState = new M2_CoolDownState(this, stateMachine, "coolDown", coolDownStateData, this);
        stateMachine.InitializeState(idleState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(aliveGO.transform.position, entityData.alertRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(aliveGO.transform.position, entityData.attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aliveGO.transform.position, entityData.chasingRange);
        
    }
}
