using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newShootStingStateData", menuName = "Data/State Data/Shoot Sting Data")]
public class D_ShootProjectileState : ScriptableObject
{
    public float performDuration = 0.3f;
    public float waitBeforeDuration = 0.5f;
    public float attackDamage = 10;
    public GameObject bullet;
    public LayerMask whatIsPlayer;
    public float bulletSpeed = 10f;
    public float bulletLifeTime = 10f;
}
