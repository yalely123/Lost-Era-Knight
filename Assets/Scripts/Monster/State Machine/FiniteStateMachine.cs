using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    // this class simulate machine that read and change state for each instance of monster's behavior
    public State currentState { get; private set; }
    
    void initializeState(State startingState) // start the very first state(  start---->)
    {
        this.currentState = startingState;
        currentState.Enter();
    } 

    void changeState(State nextState)
    {
        currentState.Exit();
        currentState = nextState;
        currentState.Enter();
    }
}
