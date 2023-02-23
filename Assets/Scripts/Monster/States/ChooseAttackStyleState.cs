using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAttackStyleState : State
{
    protected D_ChooseAttackStyleState stateData;
    protected float weightSA;
    protected float weightNA;
    public ChooseAttackStyleState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_ChooseAttackStyleState stateData) 
        : base(entity, finiteStateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        // define base weight when not including difficulty
        weightSA = stateData.weightSA; 
        weightNA = stateData.weightNA;
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

    protected int randomAttackStyle()
    {
        // return 0 means normal attack and 1 means special attack
        int ranNum = Random.Range(0, 2);
        return 0;
    }
}
