using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]

public class D_Entity : ScriptableObject
{
    public float maxHealth = 100;
    public float wallCheckDistance = 0.3f;
    public float ledgeCheckDistance = 0.4f;

    public float alertRange = 4f;

    public float attackRange = 1f;
    public float chasingRange = 7f;

    // public float knockForce = 30f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
