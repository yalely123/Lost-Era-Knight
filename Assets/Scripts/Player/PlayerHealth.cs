using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public float maxHealth = 100; // temporary max health for now
    [SerializeField] 
    private float health;
    public float stunTime = 0;
    [SerializeField]
    private bool deadShown = false;

    private Rigidbody2D rb;
    private PlayerController controller;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = maxHealth;
        controller = gameObject.GetComponent<PlayerController>();
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
    public void Die()
    {
        if (!deadShown) // for Debugging, Die() funciton.
        {
            Debug.Log("You are dead!");
            deadShown = true;
        }
        GameAi.deadCount += 1;
        /*
        GameAi.KillAllMonsters();
        */
        Destroy(gameObject); // Destroy player object (player is dead, shouldn't show on sceen)
        
    }

    public void ReceiveDamage(float amount) // this funciton is for receive damage and calculate how much to decrease health
    {

        if (amount < 0) // Ensure that damage which receive is not negative value.
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }else
        {
            health -= amount;
        }
        
        if (health > 0)
        {
            // call funciton in playercontroller to make player knockback

        }
    }

}
