using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : Entity
{
    public M1_IdleState idleState { get; private set; }

    public M1_MoveState moveState { get; private set; }
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;


    public override void Start()
    {
        base.Start();

        moveState = new M1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new M1_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.InitializeState(moveState);
    }


}
