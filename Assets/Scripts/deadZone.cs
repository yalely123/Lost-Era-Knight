using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Player touches dead zone!!!");

        if (collider.GetComponent<PlayerHealth>() != null) // call die when player in this zone
        {
            PlayerHealth playerhealth = collider.GetComponent<PlayerHealth>();
            playerhealth.Die();
        }
    }
}
