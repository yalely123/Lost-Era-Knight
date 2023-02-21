using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : Entity
{
    public M1_IdleState idleState { get; private set; }
    public M1_MoveState moveState { get; private set; }
    public M1_PlayerDetectedState playerDetectedState { get; private set;}
    public M1_ChasePlayerState chasePlayerState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField]
    private D_ChasePlayerState chasePlayerStateData;


    public override void Start()
    {
        base.Start();

        moveState = new M1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new M1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new M1_PlayerDetectedState(this, stateMachine, "", playerDetectedStateData, this);
        chasePlayerState = new M1_ChasePlayerState(this, stateMachine, "", chasePlayerStateData, this);

        stateMachine.InitializeState(moveState);
    }


}
