using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1_ChasePlayerState : ChasePlayerState
{
    private Monster1 monster;
    public M1_ChasePlayerState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_ChasePlayerState stateData, Monster1 monster) 
        : base(entity, finiteStateMachine, animBoolName, stateData)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Now in M1 chase player state");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // TODO: 1. move toward player, while player in alert range and not in attack range
        //       2. change to attack state if player in attack range
        //       3. cahnge to idle state if player is out of alert range

        if (isPlayerInAlertRange && !isPlayerInAttackRange)
        {
            // Do move toward player.
            entity.SetVelocity(stateData.movementSpeed);
        }
        else if (isPlayerInAttackRange)
        {
            // Do change to attack state
            Debug.Log("Attack!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            entity.SetVelocity(0f);
        }
        else if (!isPlayerInAlertRange)
        {
            // Do change to Idle state
            monster.idleState.SetFlipAfterIdle(false);
            finiteStateMachine.ChangeState(monster.idleState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
