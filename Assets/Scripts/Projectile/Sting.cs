using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sting : MonoBehaviour
{
    private float speed;
    [SerializeField]
    private float stingLifeTime;
    private float xStartPos;
    private float startTime;
    private float damage;

    [SerializeField]
    private float damageRadius;
    private Rigidbody2D rb;

    public Transform damagePosition;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Transform playerPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
        xStartPos = transform.position.x;
        startTime = Time.time;
        Vector2 playerDirection = new Vector2(playerPosition.position.x - transform.position.x,
            playerPosition.position.y - transform.position.y + 1).normalized;
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb.velocity = playerDirection * speed;
    }

    private void FixedUpdate()
    {
        Collider2D groundHit = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsGround);
        Collider2D playerHit = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsPlayer);

        if (playerHit)
        {
            // TODO: call playerHealth ReceiveDamage()
            playerPosition.gameObject.SendMessage("ReceiveDamage", 15);
            Destroy(gameObject);

        }
        if (groundHit || Time.time > startTime + stingLifeTime) // projectile hit ground or expire
        {
            Destroy(gameObject);
        }
    }

    public void ShootSting(float speed, float lifetime, float damage) 
    {
        this.speed = speed;
        stingLifeTime = lifetime;
        this.damage = damage;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
