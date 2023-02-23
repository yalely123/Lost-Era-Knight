using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack Data")]
public class D_MeleeAttackState : ScriptableObject
{
    public float attackRadius = 0.5f;

    public LayerMask whatIsPlayer;

    public float attackDamage = 10f;

    public float performDuration = 0.3f;
    public float waitBeforeperformDuration = 0.4f;
}
