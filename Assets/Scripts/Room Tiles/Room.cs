using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Room : MonoBehaviour
{
    public bool istraveled = false;
    public bool isPlayerInThisRoom = false,
                isDoorSpawned = false,
                isMessageSent = false,
                isRoomCleared = false;
    //public static bool isDoorAlreadyClose = true, isDoorAlreadyOpen = false;

    private int UNITESCALE = (53/2);
    public bool hasTopDoor, hasRightDoor, hasBottomDoor, hasLeftDoor; // side that room have door and didn't connect with next room yet will be true
    public int gridPosX, gridPosY;
    public float startTime, clearTime, playTime;
    public bool isTimeStart = false;

    //public Transform door;
    [SerializeField]
    private Transform playerTransform;
    public Transform playerSpawnPoint;
    public LevelGenerator levelGen;
    public GameObject finishPortal;
    public GameObject doors;
    public Transform monsterCollection;



    private void Start()
    {
        if (GameObject.Find("Room Templete").GetComponent<LevelGenerator>() != null)
            levelGen = GameObject.Find("Room Templete").GetComponent<LevelGenerator>();
        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("Player").transform;
        }
        playerSpawnPoint.Find("Player");
        SetBoolDoor();


        // for testing
        //SpawnDoor();
        isDoorSpawned = true;
        //Debug.Log(string.Format("Horizontal Bound ({0}, {1}) _ player X({2})", transform.position.x - UNITESCALE, transform.position.x + UNITESCALE, playerTransform.position.x));
    }

    private void Update()
    {
        // TODO: check if that player is in this room area
        CheckIfPlayerInThisRoom();
        CheckIfThisRoomIsClear();
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (isDoorSpawned)
            {
                OpenDoor();
            }
            else {
                CloseDoor();
            }
        }
        if (GameManager.isDoorShouldBeClosed)
        {
            CloseDoor();
        }else if (!GameManager.isDoorShouldBeClosed)
        {
            OpenDoor();
        }
        
    }

    public void CheckIfPlayerInThisRoom()
    {
        // Debug.Log("Check Player in Room Function");
        if (playerTransform.position.x < transform.position.x + (UNITESCALE - 3) 
            && playerTransform.position.x > transform.position.x - (UNITESCALE - 3)
            && playerTransform.position.y < transform.position.y + (UNITESCALE - 3)
            && playerTransform.position.y > transform.position.y - (UNITESCALE - 3))
        {
            //Debug.Log("Player is now in this room");
            isPlayerInThisRoom = true;
            if (!isMessageSent)
            {
                GameManager.curRoom = this;
                isMessageSent = true;
                if (!isRoomCleared && !isTimeStart)
                {
                    startTime = Time.time;
                    isTimeStart = true;
                }
            }

        }else
        {
            isPlayerInThisRoom = false;
            isMessageSent = false;
        }
    }

    public void CheckIfThisRoomIsClear()
    {
        if (monsterCollection.childCount == 0)
        {
            isRoomCleared = true;
            if (isTimeStart)
            {
                clearTime = Time.time;
                playTime = clearTime - startTime;
                isTimeStart = false;
            }
        }
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

    public void CloseDoor()
    {
        isDoorSpawned = true;
        doors.SetActive(true);
        //isDoorAlreadyClose = true;
        //isDoorAlreadyOpen = false;
    }

    public void OpenDoor()
    {
        isDoorSpawned = false;
        doors.SetActive(false);
        //isDoorAlreadyOpen = true;
        //isDoorAlreadyClose = false;
    }

    public void KillAllMonsterInRoom()
    {
        foreach (Transform monster in monsterCollection)
        {
            monster.SendMessage("ReceiveDamage", 999);
        }
    }

    public void LogAllRoomData()
    {
        string s = string.Format("Game Object Name = {9}\nisTraveled = {0}\nhasDoor: top = {1}, right = {2}, bottom = {3}, left = {4}" +
            "\nPositin in Grid: x = {5}, Y = {6}\n player transform position = {7}\n player spawn point position = {8}",
            istraveled, hasTopDoor, hasRightDoor, hasBottomDoor, hasLeftDoor, gridPosX, gridPosY, playerTransform.position, playerSpawnPoint.position, name);
        Debug.Log(s);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector2(2*UNITESCALE-6, 2*UNITESCALE-6));
    }

}
