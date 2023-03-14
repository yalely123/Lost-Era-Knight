using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplete : MonoBehaviour
{
    public int maxRoomAmount, roomCreatedAmount = 0;
    public int randNum;
    public Room currentRoom;
    public GameObject currentTile;
    public GameObject[] roomTiles;
    
    private void Start()
    {   
        GenerateStartRoom();
        
    }

    private void Update()
    {
        // this is for debugging
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            // mean that player pass top door
            GenerateNextRoom();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            // mean that player pass right door
            GenerateNextRoom();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // mean that player pass bottom door
            GenerateNextRoom();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // mean that player pass left door
            GenerateNextRoom();
        }
    }

    public void GenerateStartRoom()
    {
        // Randomly generate first room 
        roomCreatedAmount = 1;
        randNum = Random.Range(0, 15);
        currentTile = roomTiles[randNum];
        //currentTile = roomTiles[13];
        currentRoom = currentTile.GetComponent<Room>();
        //Instantiate(currentTile, Vector2.zero, Quaternion.identity);
        currentTile.SetActive(true);
    }

    public void GenerateNextRoom()
    {
        // TODO: create next room that available
        currentTile.SetActive(false);
        randNum = Random.Range(0, 15);
        currentTile = roomTiles[randNum];
        currentRoom = currentTile.GetComponent<Room>();
        currentTile.SetActive(true);
    }

    // TODO: choose what next room when player arrive next room
    //       Detect player reach door or something to trigger GenerateNextRoom()
}
