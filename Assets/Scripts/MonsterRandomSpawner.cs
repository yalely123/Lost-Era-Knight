using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRandomSpawner : MonoBehaviour
{
    private GameObject player;
    private GameObject newMonster;
    public GameObject[] monsterSet;
    [SerializeField]
    private bool isSpawn = false;
    
    //public Transform spawnPoint;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.transform.position.x > -1f && player.transform.position.y < 0f) // this is condition for spawn monster
        {
            if (!isSpawn)
            {
                int randMonster = Random.Range(0, monsterSet.Length);
                newMonster = Instantiate(monsterSet[randMonster], transform.position, Quaternion.identity);
                isSpawn = true;
            }    
        }else
        {
            isSpawn = false;
            Destroy(newMonster); // this line is temperary one i will des by tag or something that can loop with no bonding with local file variable
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            isSpawn = false;
        }
    }
}
//x> - 1 y <-.5