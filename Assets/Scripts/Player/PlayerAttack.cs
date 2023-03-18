using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private bool attacking = false;
    public float timeToAttack = 0.3f;
    private float timer = 0.0f;
    private string side = "default";
    public float attackRadius = 1.35f;

    private Animator anim;
    public Transform attackArea;
    // Start is called before the first frame update
    void Start()
    {
        // attackArea = GameObject.Find("Attack Area").transform;
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            side = "top";
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
                anim.SetBool("isAttacking", attacking);
            }
        }
    }

    private void Attack(string side = "default")
    {
        attacking = true;
        if (side == "default")
        {
            // attackArea.SetActive(attacking); // this line is for anable child object
            anim.SetBool("isAttacking", attacking);
            Collider2D[] attackCollision = Physics2D.OverlapCircleAll(attackArea.transform.position, attackRadius);
            foreach (Collider2D coll in attackCollision)
            {
                if (coll.tag == "Monster")
                {
                    Debug.Log("Player hit monster");
                }
            }
        }
    }

    private void setOffIsAttacking()
    {
        attacking = false;
        anim.SetBool("isAttacking", false);
    }

    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackArea.transform.position, attackRadius);
    }
    
}
