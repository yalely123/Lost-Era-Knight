using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : Entity
{
    public M1_IdleState idleState { get; private set; }
    public M1_MoveState moveState { get; private set; }
    public M1_PlayerDetectedState playerDetectedState { get; private set;}
    public M1_ChasePlayerState chasePlayerState { get; private set; }
    public M1_ChooseAttackStyleState chooseAttackStyleState { get; private set; }
    public M1_MeleeAttackState meleeAttackState { get; private set; }

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
    private D_MeleeAttackState meleeAttackStateData;

    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform playerCheckPosition;


    public override void Start()
    {
        base.Start();

        moveState = new M1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new M1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new M1_PlayerDetectedState(this, stateMachine, "", playerDetectedStateData, this);
        chasePlayerState = new M1_ChasePlayerState(this, stateMachine, "", chasePlayerStateData, this);
        chooseAttackStyleState = new M1_ChooseAttackStyleState(this, stateMachine, "", chooseAttackStyleStateData, this);
        meleeAttackState = new M1_MeleeAttackState(this, stateMachine, "", meleeAttackPosition, meleeAttackStateData, this);

        stateMachine.InitializeState(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);

        Gizmos.DrawWireSphere(playerCheckPosition.position + (Vector3)(aliveGO.transform.right * entityData.attackRange), 0.2f);
    }
}
