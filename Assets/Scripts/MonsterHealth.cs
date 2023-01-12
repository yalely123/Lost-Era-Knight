using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField] private int maxHeath = 100;
    [SerializeField] private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHeath;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void ReceiveDamage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("cannot have negative damage");
        }else
        {
            health -= amount;
        }
    }

    void Die() 
    {
        Debug.Log("A monster is removed!");
        Destroy(gameObject);
    }

}
