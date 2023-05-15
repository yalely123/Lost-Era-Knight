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
    [SerializeField]
    private bool isHitable;
    //private bool isInvincEnd;

    private Rigidbody2D rb;
    private PlayerController controller;
    private SpriteRenderer playerSprite;
    private Coroutine flashRoutine;

    [SerializeField]
    private HealthBar healthBar;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = maxHealth;
        controller = gameObject.GetComponent<PlayerController>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        isHitable = true;
        flashRoutine = null;
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
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
            ReceiveDamage(50, transform.position.x);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            health = maxHealth;
            deadShown = true;
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
        Destroy(gameObject); // Destroy player object (player is dead, shouldn't show on sceen)
        GameAi.LoadGameOverScene();
    }

    public void ReceiveDamage(float amount, float xPos) // this funciton is for receive damage and calculate how much to decrease health
    {
        if (!isHitable || controller.godModeOn)
        {
            return;
        }

        if (amount < 0) // Ensure that damage which receive is not negative value.
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }else
        {
            health -= amount;
            controller.isHit = true;
            controller.attackXPos = xPos;

            healthBar.SetHealth(health);
        }
        
        if (health > 0)
        {
            // call funciton in playercontroller to make player knockback
            FlashDamage();
        }
    }

    private void FlashDamage()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(VisualIndicator(Color.grey, 1f));
    }

    private IEnumerator VisualIndicator(Color color, float duration = 0.6f)
    {
        isHitable = false;
        for (int i = 0; i < 5; i++)
        {
            playerSprite.color = color;
            yield return new WaitForSeconds(duration / 5);
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(duration / 5);
        }
        isHitable = true;
    }

    private IEnumerator Invincible(float duration)
    {
        isHitable = false;
        yield return new WaitForSeconds(duration);
        isHitable = true;

    }
}
