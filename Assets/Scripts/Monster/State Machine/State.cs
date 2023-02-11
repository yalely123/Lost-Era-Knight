using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine finiteStateMachine;
    protected Entity entity;

    protected float startTime; // time when this state is entered

    public State(Entity entity, FiniteStateMachine finiteStateMachine)
    {
        this.entity = entity;
        this.finiteStateMachine = finiteStateMachine;
    }

    public virtual void Enter() 
    {
        startTime = Time.time;
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicUpdate()
    {
        // update about physic such as animator, force, etc.
    }
}


