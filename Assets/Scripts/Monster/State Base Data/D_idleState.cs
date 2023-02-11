using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle State")]
public class D_idleState : ScriptableObject
{
    public float idleTime = 10f; // for changing to other state
    public float alertRange = 10f;
    public float minFlipTime = 1f;
    public float maxFlipTime = 3f;
}
