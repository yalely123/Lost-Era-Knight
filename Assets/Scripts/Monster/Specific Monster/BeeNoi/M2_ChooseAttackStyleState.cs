using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_ChooseAttackStyleState : ChooseAttackStyleState
{
    private Monster2 monster;
    private int attackStyle;
    public M2_ChooseAttackStyleState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_ChooseAttackStyleState stateData, Monster2 monster) 
        : base(entity, finiteStateMachine, animBoolName, stateData)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        base.Enter();
        attackStyle = randomAttackStyle();
        Debug.Log("Bee: Enter ChooseAttackStyleState----------------------------");
        entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // TODO: make random Decision of attacking style
        if (attackStyle == 0)
        {
            // Change State to shoot sting state
            // To make game flow for now
            finiteStateMachine.ChangeState(monster.shootStingState);
        }
        else if (attackStyle == 1)
        {
            // Change State to special Attack state
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
