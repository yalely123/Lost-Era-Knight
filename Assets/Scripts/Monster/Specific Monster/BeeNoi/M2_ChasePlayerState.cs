using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_ChasePlayerState : ChasePlayerState
{
    private Monster2 monster;
    private bool isPlayerInChasingCircleRange;
    private bool isPlayerInAttackCircleRange;
    private bool isNeedToFlip;
    private float attackPositionDistance;
    private float distanceOffset = 0.2f;

    public M2_ChasePlayerState(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName, D_ChasePlayerState stateData, Monster2 monster) 
        : base(entity, finiteStateMachine, animBoolName, stateData)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        base.Enter();
        isNeedToFlip = entity.CheckIfNeedToFlip();
        //Debug.Log("Bee: Enter ChasePlayerState");
        attackPositionDistance = Vector2.Distance(monster.aliveGO.transform.position, entity.playerCheck.position);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Debug.Log("is Player in Attack range: " + isPlayerInAttackCircleRange);
        if (isNeedToFlip)
        {
            entity.Flip();
        }
        if (isPlayerInChasingCircleRange && (attackPositionDistance > distanceOffset))
        {
            // TODO: Do chasing player
            //Debug.Log(entity.facingDirection);
            flyTowardPlayer(stateData.chaseSpeed);
        }
        else if (isPlayerInAttackCircleRange && (attackPositionDistance > distanceOffset))
        {
            flyTowardPlayer(stateData.secondChaseSpeed);
        }

        else if (isPlayerInAttackCircleRange && (attackPositionDistance <= distanceOffset))
        {
            // TODO: change state to choose attack style state
            finiteStateMachine.ChangeState(monster.chooseAttackStyleState);
        }
        else if (!isPlayerInChasingCircleRange)
        {
            // TODO: change state to backToSpawnPointState
            // to make game can flow
            finiteStateMachine.ChangeState(monster.playerDetectedState);
        }
        
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        isPlayerInChasingCircleRange = entity.CheckPlayerInChasingCircleRange();
        isNeedToFlip = entity.CheckIfNeedToFlip();
        isPlayerInAttackCircleRange = entity.CheckPlayerInAttackCircleRange();
        attackPositionDistance = Vector2.Distance(monster.aliveGO.transform.position,
            new Vector2(entity.playerCheck.position.x + (-5 * entity.facingDirection), entity.playerCheck.position.y + 5));
    }

    private void flyTowardPlayer(float speed)
    {
        entity.aliveGO.transform.position = Vector2.MoveTowards(entity.aliveGO.transform.position,
                new Vector2(entity.playerCheck.position.x + (-5 * entity.facingDirection), entity.playerCheck.position.y + 5),
                speed * Time.deltaTime);
    }
}
