using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : Entity
{
    public M2_IdleState idleState { get; private set; }
    public M2_MoveState moveState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;

    public override void Start()
    {
        base.Start();
        idleState = new M2_IdleState(this, stateMachine, "", idleStateData, this);
        moveState = new M2_MoveState(this, stateMachine, "", moveStateData, this);
        stateMachine.InitializeState(idleState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, entityData.alertRange);
    }
}
