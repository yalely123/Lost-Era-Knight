using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Room : MonoBehaviour
{
    public bool istraveled = false;
    public bool hasTopDoor, hasRightDoor, hasBottomDoor, hasLeftDoor; // side that room have door and didn't connect with next room yet will be true
    public int gridPosX, gridPosY;
    public int roomType; // 0 is normal room, 1 is start room and 2 is ending room
    //public Transform door;
    [SerializeField]
    private Transform playerTransform;
    public Transform playerSpawnPoint;
    public LevelGenerator levelGen;
    public GameObject finishPortal;


    private void Start()
    {
        if (GameObject.Find("Room Templete").GetComponent<LevelGenerator>() != null)
            levelGen = GameObject.Find("Room Templete").GetComponent<LevelGenerator>();
        playerSpawnPoint.Find("Player");
        SetBoolDoor();
        
    }

    public void BringPlayerToStartPosition() // bring player to player start point of this tile
    {
        if (playerTransform != null)
        {
            playerTransform.position = playerSpawnPoint.position;
            GameManager.playerSpawnPos = playerSpawnPoint.position;
        }
        else
        {
            Debug.Log("Player Transform is not found");
        }
    }

    public void SpawnFinishPortal()
    {
        // spawn finish portal in finishroom
        if (finishPortal != null)
        {
            GameObject portal = Instantiate(finishPortal, playerSpawnPoint.position, Quaternion.identity);
            portal.transform.parent = transform;
        }else
        {
            throw new Exception("Portal GameObject is not Found!");
        }
    }

    public void SetBoolDoor() // read prefab name then set that which door is available
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
            

            if (gridPosY + 1 < LevelGenerator.gridSizeY && LevelGenerator.rooms[gridPosX, gridPosY + 1] != null) // check top side
            {
                hasTopDoor = false;
            }
           
            if (gridPosX + 1 < LevelGenerator.gridSizeX && LevelGenerator.rooms[gridPosX + 1, gridPosY] != null) // check right side
            {
                hasRightDoor = false;
            }
            
            if (gridPosY - 1 >= 0 && LevelGenerator.rooms[gridPosX, gridPosY - 1] != null) // check bottom side
            {
                hasBottomDoor = false;
            }
            
            if (gridPosX - 1 >= 0 && LevelGenerator.rooms[gridPosX - 1, gridPosY] != null) // check left side
            {
                hasLeftDoor = false;
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

    public List<char> getAllConnectableDoor() // return list of shorted character of side that door can connect with new room
    {
        List<char> temp = new List<char>();
        if (hasTopDoor)
        {
            temp.Add('T');
            //Debug.Log("has top door");
        }
        if (hasRightDoor)
        {
            temp.Add('R');
            //Debug.Log("has right door");
        }
        if (hasBottomDoor)
        {
            temp.Add('B');
            //Debug.Log("has bottom door");
        }
        if (hasLeftDoor)
        {
            temp.Add('L');
            //Debug.Log("has left door");
        }
        return temp;
    }

    public string getName()
    {
        string temp = "";

        foreach(char s in name)
        {
            if (s == 'T' || s == 'R' || s == 'B' || s == 'L')
            {
                temp += s;
            }
        }
        return temp;
    }

    public void LogAllRoomData()
    {
        string s = string.Format("Game Object Name = {9}\nisTraveled = {0}\nhasDoor: top = {1}, right = {2}, bottom = {3}, left = {4}" +
            "\nPositin in Grid: x = {5}, Y = {6}\n player transform position = {7}\n player spawn point position = {8}",
            istraveled, hasTopDoor, hasRightDoor, hasBottomDoor, hasLeftDoor, gridPosX, gridPosY, playerTransform.position, playerSpawnPoint.position, name);
        Debug.Log(s);
    }

}
