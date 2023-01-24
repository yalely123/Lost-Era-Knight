using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Object touch dead zone");

        if (collider.GetComponent<PlayerHealth>() != null) // call die when player in this zone
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            playerHealth.Die();
        }

        if (collider.GetComponent<MonsterHealth>() != null) 
        {
            MonsterHealth monsterHealth = collider.GetComponent<MonsterHealth>();
            monsterHealth.Die();
        }
    }
}
