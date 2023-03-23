using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackArea : MonoBehaviour 
{
    private int damage = 10;

    private void OnTriggerEnter2D(Collider2D collider) // check collision from player's attack
    {
        if (collider.GetComponent<PlayerHealth>() != null)
        {
            PlayerHealth playerhealth = collider.GetComponent<PlayerHealth>();
            //playerhealth.ReceiveDamage(damage);

            Debug.Log("attack hit player!");
        }

        if (collider.GetComponent<MonsterHealth>() != null)
        {
            MonsterHealth monsterhealth = collider.GetComponent<MonsterHealth>();
            monsterhealth.ReceiveDamage(damage);

            Debug.Log("attack hit monster!");
        }
    }
}
