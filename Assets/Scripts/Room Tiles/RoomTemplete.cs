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
    public GameObject[] topDoorRoomTiles;
    public GameObject[] rightDoorRoomTiles;
    public GameObject[] bottomDoorRoomTiles;
    public GameObject[] leftDoorRoomTiles;
    public GameObject player;
    
    private void Start()
    {
        player = GameObject.Find("Player");
        ChooseStartRoomTile();
    }

    public void ChooseStartRoomTile()
    {
        // Randomly generate first room tile.
        roomCreatedAmount = 1;
        randNum = Random.Range(0, 15);
        currentTile = roomTiles[randNum];
        //currentTile = roomTiles[13];
        currentRoom = currentTile.GetComponent<Room>();
        //Instantiate(currentTile, Vector2.zero, Quaternion.identity);
        currentTile.SetActive(true);
        currentRoom.bringPlayerToStartPosition();
    }

    public void ChooseStartRoomTile(int tilesIndex)
    {

        currentTile = roomTiles[tilesIndex];
        //currentTile = roomTiles[13];
        currentRoom = currentTile.GetComponent<Room>();
        //Instantiate(currentTile, Vector2.zero, Quaternion.identity);
        currentTile.SetActive(true);
        currentRoom.bringPlayerToStartPosition();
    }

    public void ChangeRoomTile(string from = "default") // from take side that player enter door
    {
        // create next room that correct side

        currentTile.SetActive(false);

        // choose door that have opposite side of from variable
        if (from == "Top Door")
        {
            
            randNum = Random.Range(0, bottomDoorRoomTiles.Length);
            currentTile = bottomDoorRoomTiles[randNum];
            player.transform.position = new Vector3(0.5f,
                -23f, player.transform.position.z);
        }
        else if (from == "Right Door")
        {
            randNum = Random.Range(0, leftDoorRoomTiles.Length);
            currentTile = leftDoorRoomTiles[randNum];
            player.transform.position = new Vector3(-24f,
                -4f, player.transform.position.z);
        }
        else if (from == "Buttom Door")
        {
            randNum = Random.Range(0, topDoorRoomTiles.Length);
            currentTile = topDoorRoomTiles[randNum];
            player.transform.position = new Vector3(0.5f,
                22f, player.transform.position.z);
        }
        else if (from == "Left Door")
        {
            randNum = Random.Range(0, rightDoorRoomTiles.Length);
            currentTile = rightDoorRoomTiles[randNum];
            player.transform.position = new Vector3(25f, 
                -4f, player.transform.position.z);
        }
        else
        {
            randNum = Random.Range(0, roomTiles.Length);
            currentTile = roomTiles[randNum];
        }
        currentRoom = currentTile.GetComponent<Room>();
        //Debug.Log(currentTile.name);
        currentTile.SetActive(true);
        
    }

    // TODO: choose what next room when player arrive next room
    
}
