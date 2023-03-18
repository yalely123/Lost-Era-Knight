using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_ChooseAttackStyleState : ChooseAttackStyleState
{
    private Monster1 monster;
    private int attackStyle; // 0 is normal attack and 1 is special attack
    public M1_ChooseAttackStyleState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_ChooseAttackStyleState stateData, Monster1 monster) 
        : base(entity, finiteStateMachine, animBoolName, stateData)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        base.Enter();
        // TODO: call calculated score of difficulty from GameAI to weight weightNA & weightSA

        attackStyle = randomAttackStyle();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Debug.Log(attackStyle); // use for see output of randomAttackStyle()
        if (attackStyle == 0)
        {
            // TODO: change state to normal attack of this monster
            //Debug.Log("do normal attack!");
            finiteStateMachine.ChangeState(monster.meleeAttackState);
        }
        else if (attackStyle == 1)
        {
            // TODO: change state to special attack of this monster
            Debug.Log("release special attack!!!");
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
