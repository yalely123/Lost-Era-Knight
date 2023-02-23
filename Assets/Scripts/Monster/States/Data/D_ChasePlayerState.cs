using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChasePlayerStateData", menuName = "Data/State Data/Chase Player Data")]
public class D_ChasePlayerState : ScriptableObject
{
    public float chaseSpeed = 10f;
    public float chaseTime = 5f;
}
