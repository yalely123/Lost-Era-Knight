using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : Entity
{
    public IdleState idleState { get; private set; }

    private D_idleState idleStateData;

    public override void Start()
    {
        base.Start();

        idleState = new IdleState(this, stateMachine, idleStateData);
    }

}
