using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // temporary max health for now
    [SerializeField] private int health;
    private bool deadShown = false;

    private float armour;
    // TODO: add armour in the future in decreasing attacked damage
    
    void Start()
    {
        health = maxHealth;
    }

    public void Die()
    {
        if (!deadShown) // for Debugging, Die() funciton.
        {
            Debug.Log("You are dead!");
            deadShown = true;
        }
        GameAi.deadCount += 1;
        Destroy(gameObject); // Destroy player object (player is dead, shouldn't show on sceen)
        
    }

    public void ReceiveDamage(int amount) // this funciton is for receive damage and calculate how much to decrease health
    {

        if (amount < 0) // Ensure that damage which receive is not negative value.
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }else
        {
            health -= amount;
        }
        
        //Debug.Log("Receive damage: " + amount);
        
        // TODO:
        //  - (In future) cal damage according to damage weigth with armour
    }

    void Update()
    {
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        if (Input.GetKeyDown(KeyCode.H)) // press h to take damage 15 point to test system
        {
            ReceiveDamage(15);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            health = maxHealth;
            deadShown = false;
        }
    }
}
