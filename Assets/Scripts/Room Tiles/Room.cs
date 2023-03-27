using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool istraveled = false;
    public bool hasTopDoor, hasRightDoor, hasBottomDoor, hasLeftDoor;
    public int gridPosX, gridPosY;
    public Dictionary<string, Room> nextRoom;
    //public Transform door;
    [SerializeField]
    private Transform playerTransform;
    public Transform playerSpawnPoint;
    public LevelGenerator levelGen;


    private void Start()
    {
        levelGen = GameObject.Find("Room Templete").GetComponent<LevelGenerator>();
        SetBoolDoor();
        RandomlyConnectRoom();
    }

    public void BringPlayerToStartPosition()
    {
        if (playerTransform != null)
        {
            playerTransform.position = playerSpawnPoint.position;
        }else
        {
            Debug.Log("Player Transform is not found");
        }
    }

    private void SetBoolDoor()
    {
        foreach (char c in gameObject.name)
        {
            if (c == '(') { break; } // prefab generate to hierachy there will be (clone) follow by the name
            if (char.ToUpper(c) == 'T')
            {
                hasTopDoor = true;
            }
            if (char.ToUpper(c) == 'R')
            {
                hasRightDoor = true;
            }
            if (char.ToUpper(c) == 'B')
            {
                hasBottomDoor = true;
            }
            if (char.ToUpper(c) == 'L')
            {
                hasLeftDoor = true;
            }
        }
    }

    public void SetPositionInGrid(int x, int y)
    {
        gridPosX = x;
        gridPosY = y;
    }

    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;  
    }

    public void RandomlyConnectRoom()
    {
        // TODO: choose room that can connect relate to grid
        string sideToConnect;
        if (levelGen.amountRoom > 0)
        {
            if (hasTopDoor)
            {
                sideToConnect = "top";
                levelGen.RandomNextTile(this, sideToConnect);
            }
            if (hasRightDoor)
            {
                sideToConnect = "right";
                levelGen.RandomNextTile(this, sideToConnect);
            }
            if (hasBottomDoor)
            {
                sideToConnect = "bottom";
                levelGen.RandomNextTile(this, sideToConnect);
            }
            if (hasLeftDoor)
            {
                sideToConnect = "left";
                levelGen.RandomNextTile(this, sideToConnect);
            }
        }
    }

}
