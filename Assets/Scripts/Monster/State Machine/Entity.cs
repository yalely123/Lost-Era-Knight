using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;
    public int facingDirection { get; private set; } // 1 as facing to right side and -1 mean left side

    private Vector2 velocityWorkSpace; // use when new vector 2 to set velocity
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO;

    public AnimationToStatemachine atsm;

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    public Transform playerCheck;

    public bool isAlert;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private Material flashMaterial;
    [SerializeField]
    private Material defaultMaterial;
    private float flashDuration = 0.15f;
    private Coroutine flashRoutine;

    public virtual void Start()
    {
        facingDirection = 1;
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStatemachine>();

        stateMachine = new FiniteStateMachine();
        isAlert = false;

        currentHealth = entityData.maxHealth;
    }

    public virtual void Update()
    {
        SendDamageIfPlayerTouchMonsterHitBox();
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        CheckIfMonsterDead();
        
        stateMachine.currentState.PhysicUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkSpace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckPlayeInAlertRange()
    {
        if (playerCheck != null)
            return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.alertRange, entityData.whatIsPlayer);
        else
            return false;
    }

    public virtual bool CheckPlayerInAttackRange()
    {
        if (playerCheck != null)
            return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.attackRange, entityData.whatIsPlayer);
        else return false;
    }

    public virtual bool CheckPlayerInAlertCircleRange()
    {
        Collider2D[] alertCollisionInCircleArea = Physics2D.OverlapCircleAll(aliveGO.transform.position, entityData.alertRange);
        foreach(Collider2D coll in alertCollisionInCircleArea)
        {
            if (coll.tag == "Player") { return true; }
        }
        return false;
    }

    public virtual bool CheckPlayerInAttackCircleRange()
    {
        Collider2D[] attackCollisionInCircleArea = Physics2D.OverlapCircleAll(aliveGO.transform.position, entityData.attackRange);
        foreach (Collider2D coll in attackCollisionInCircleArea)
        {
            if (coll.tag == "Player") { return true; }
        }
        return false;
    }

    public virtual bool CheckPlayerInChasingCircleRange()
    {
        Collider2D[] chasingCollisionInCircleArea = Physics2D.OverlapCircleAll(aliveGO.transform.position, entityData.chasingRange);
        foreach (Collider2D coll in chasingCollisionInCircleArea)
        {
            if (coll.tag == "Player") { return true; }
        }
        return false;
    }

    public virtual void SendDamageIfPlayerTouchMonsterHitBox()
    {
        // check if player touch monster call player to receive damage
        Vector2 monsterHitBox = new Vector2(entityData.colliderWidth, entityData.colliderHeight);
        Collider2D[] bodyHitBox = Physics2D.OverlapBoxAll(aliveGO.transform.position, monsterHitBox, 0);
        foreach (Collider2D coll in bodyHitBox)
        {
            if (coll.tag == "Player")
            {
                coll.gameObject.GetComponent<PlayerHealth>()
                    .ReceiveDamage(entityData.bodyTouchDamage, 
                                   aliveGO.transform.position.x);
            }
        }
    }

    public virtual bool CheckIfNeedToFlip()
    {
        if (playerCheck != null)
        {
            if (playerCheck.position.x > aliveGO.transform.position.x && facingDirection != 1 && isAlert)
            {
                return true;
            }
            else if (playerCheck.position.x < aliveGO.transform.position.x && facingDirection != -1 && isAlert)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else return false;
    }

    
    public virtual Vector2 CheckPlayerAngle()
    {
        if (playerCheck != null)
        {
            Vector2 angle = new Vector2(playerCheck.position.x - aliveGO.transform.position.x,
                playerCheck.position.y - aliveGO.transform.position.y);
            return angle;
        }
        else return Vector2.zero;
    }

    public virtual bool CheckIfMonsterDead()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void ReceiveDamage(float amount)
    {
        currentHealth -= amount;
        DamageFlash();
        if (currentHealth <= 0 )
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("Monster Die");
        Destroy(gameObject);
        GameAi.monsterKillCount += 1;
    }

    public void DamageFlash()
    {
        // != null mean that this coroutine is running.
        // if coroutine still running we have to stop it first
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(DamageFlashRoutine());
    }

    public IEnumerator DamageFlashRoutine()
    {
        aliveGO.GetComponent<SpriteRenderer>().material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        aliveGO.GetComponent<SpriteRenderer>().material = defaultMaterial;
        flashRoutine = null;
    }

    

    public virtual void OnDrawGizmos() // draw line to debugging
    {
        Gizmos.DrawLine(wallCheck.position, 
                        wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, 
                        ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
        Gizmos.DrawWireCube(new Vector2 (aliveGO.transform.position.x + entityData.colliderXOffset,
                                         aliveGO.transform.position.y + entityData.colliderYOffset), 
                           new Vector2(entityData.colliderWidth, entityData.colliderHeight));
    }

}
