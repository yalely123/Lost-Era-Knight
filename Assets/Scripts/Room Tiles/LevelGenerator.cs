using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int gridSizeX, gridSizeY;
    public int UNITSCALE = 53; // mean that if we spawn room at (0,0) next right room will spawn (53, 0)
    public Room[,] rooms;
    public GameObject fakeRoom;
    [SerializeField]
    private Room currentRoom;

    private void Start()
    {

        rooms = new Room[gridSizeX, gridSizeY];
        int randSpawnX = Random.Range(0, gridSizeX);
        int randSpawnY = Random.Range(0, gridSizeY);
        //currentRoom = fakeRoom.GetComponent<Room>();
        Instantiate(fakeRoom, new Vector2(randSpawnX * UNITSCALE, randSpawnY * UNITSCALE), Quaternion.identity);
            
        
    }
}

