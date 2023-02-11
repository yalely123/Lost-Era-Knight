using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRandomSpawner : MonoBehaviour
{
    public GameObject[] monsterSet;
    private GameObject player;
    [SerializeField]
    private bool isSpawn = false;
    public bool spawnWhenOnPos = false;
    
    //public Transform spawnPoint;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && spawnWhenOnPos)
        {
            if (player.transform.position.x > -1f && player.transform.position.x < 2.4 
                && player.transform.position.y < 1.2f && player.transform.position.y > -2.3) // this is condition for spawn monster when player in box of area
            {
                if (!isSpawn) // to prevent repeatedly spawning
                {
                    GameAi.spawnMonster(transform.position.x, transform.position.y);
                    isSpawn = true;
                }
            }
            else
            {
                isSpawn = false;
                //Destroy(newMonster); // this line is temperary one. Destroy by tag or something that can loop with no bonding with local file variable
            }
        }
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            //isSpawn = false;
            GameAi.spawnMonster(transform.position.x, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameAi.spawnMonster(transform.position.x, transform.position.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameAi.spawnMonster(transform.position.x, transform.position.y, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameAi.spawnMonster(transform.position.x, transform.position.y, 2);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GameAi.KillAllMonsters();
        }
    }
}
//x> - 1 y <-.5