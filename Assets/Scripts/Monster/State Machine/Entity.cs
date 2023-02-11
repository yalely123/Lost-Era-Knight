using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicUpdate();
    }

    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
