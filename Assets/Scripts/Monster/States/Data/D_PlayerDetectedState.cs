using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Player Detected State Data")]
public class D_PlayerDetectedState : ScriptableObject
{
    public float actionTime = 1.5f; // Time that will hold on this state before change to next state.
}
