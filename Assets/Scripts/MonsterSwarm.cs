using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSwarm : MonoBehaviour
{
    private GameObject player;
    private float speed = 5;
    private Vector3 lookPosition;
    private Rigidbody2D monsterRb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        monsterRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lookPosition = new Vector2(player.transform.position.x - 
            transform.position.x, player.transform.position.y - transform.position.y).normalized;
        monsterRb.AddForce(lookPosition * speed);
    }
}
