using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    // this class simulate machine that read and change state for each instance of monster's behavior
    public State currentState { get; private set; }
    
    public void InitializeState(State startingState) // start the very first state(  start----> someState ----> ... )
    {
        this.currentState = startingState;
        currentState.Enter();
    } 

    public void ChangeState(State nextState)
    {
        currentState.Exit(); // exit current state
        currentState = nextState;
        currentState.Enter(); // enter next state
    }
}
