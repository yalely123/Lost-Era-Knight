using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSwarm : MonoBehaviour
{
    private GameObject player;
    private float speed = 3f;
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
        // lookpostion from monster toward player direction in form of Vector2(x, y axis)
        //lookPosition = new Vector2(player.transform.position.x - 
        //    transform.position.x, player.transform.position.y - transform.position.y).normalized; // normalized for scale this vector2 to be less than 1

        lookPosition = new Vector2(player.transform.position.x -
            transform.position.x, 0).normalized; // find direction in x axis only

        monsterRb.AddForce(lookPosition * speed);
    }
}
