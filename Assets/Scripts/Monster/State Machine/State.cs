using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine finiteStateMachine;
    protected Entity entity;

    protected float startTime; // time when this state is entered

    protected string animBoolName; 

    public State(Entity entity, FiniteStateMachine finiteStateMachine, string animBoolName)
    {
        this.entity = entity;
        this.finiteStateMachine = finiteStateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() 
    {
        startTime = Time.time;
        if (animBoolName != "")
            entity.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        if (animBoolName != "")
            entity.anim.SetBool(animBoolName, false); // set current animation to false
    }

    public virtual void LogicUpdate() 
    {
        // for update in logical way like condition or proper logic
    }

    public virtual void PhysicUpdate()
    {
        // update about physic such as animator, force, etc.
    }
}


