using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private GameObject attackAreaTop = default;
    private GameObject attackAreaDown = default;
    private bool attacking = false;
    private float timeToAttack = 0.15f;
    private float timer = 0.0f;
    private string side = "default";

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        attackAreaTop = transform.GetChild(1).gameObject;
        attackAreaDown = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            side = "top";
            Debug.Log("attack top side");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            side = "down";
        }
        else
        {
            side = "default";
        }


        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack(side);
        }
        if (attacking)
        {
            timer += Time.deltaTime;
            
            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
                attackAreaTop.SetActive(attacking);
                attackAreaDown.SetActive(attacking);
            }
        }
    }

    private void Attack(string side = "default")
    {
        attacking = true;
        if (side == "default")
        {
            attackArea.SetActive(attacking); // this line is for anable child object
        }else if (side == "top")
        {
            attackAreaTop.SetActive(attacking);
        }else if (side == "down")
        {
            attackAreaDown.SetActive(attacking);
        }
        
    }
}
