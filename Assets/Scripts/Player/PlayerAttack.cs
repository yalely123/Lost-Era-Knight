using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private bool isAttacking = false, 
                 isAttackInputed = false; // mean that player press attack button(x)

    public float attackRadius = 1.35f,
                 attackDamage = 15f;

    private string side = "default";

    private bool canMeleeHit;
    private bool isAttackHit;

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
        CheckAttackDirectionInput();
        CheckAttackInput();
        if (isAttackInputed && !isAttacking) // check that player can attack only if press attack and that time player is not attacking
        {
            TriggerAttack();
        }

    }

    private void CheckAttackDirectionInput()
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
    }

    private void CheckAttackInput()
    {
        isAttackInputed = Input.GetKeyDown(KeyCode.X);
    }

    private void Attack(string side = "default")
    {
        isAttacking = true;
        if (side == "default")
        {
            anim.SetBool("isAttacking", isAttacking);
            if (!isAttackHit)
            { 
                CheckAttackHit();
            }
        }
    }

    private void CheckAttackHit ()
    {
        Collider2D[] attackCollision = Physics2D.OverlapCircleAll(attackArea.transform.position, attackRadius);
        foreach (Collider2D coll in attackCollision)
        {
            if (coll.tag == "Monster") // this mean that attack hit monster
            {
                isAttackHit = true;
                if (canMeleeHit)
                { SendDamage(coll, attackDamage); } // send damage to that monster
            }
        }
    }

    private void SendDamage(Collider2D coll, float damage)
    {
        coll.GetComponentInParent<Entity>().SendMessage("ReceiveDamage", damage);
    }

    public void TriggerAttack()
    {
        isAttacking = true;
        isAttackHit = false;
        canMeleeHit = true;
        anim.SetBool("isAttacking", isAttacking);
        Attack(side);
    }

    public void FinishAttack()
    {
        isAttacking = false;
        canMeleeHit = true;
        isAttackHit = false;
        anim.SetBool("isAttacking", isAttacking);
    }

    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackArea.transform.position, attackRadius);
    }
    
}
