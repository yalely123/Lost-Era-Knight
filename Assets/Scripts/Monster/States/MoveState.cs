using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public MoveState(Entity entity, FiniteStateMachine finiteStateMachine) : base (entity, finiteStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
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
}
