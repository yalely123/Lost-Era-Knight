using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    public int gridSizeX, gridSizeY;
    public int UNITSCALE = 53; // mean that if we spawn room at (0,0) next right room will spawn (53, 0)
    public int amountRoom;
    
    [SerializeField]
    private GameObject wholeMap; // Parent Object that contain all map tiles
    
    public static Room[,] rooms; // array that contain all room
    private Room nextRoom;
    public GameObject fakeRoom,
                      nextTile;
    [SerializeField]
    private Room currentRoom;

    public GameObject[] allRoomPrefab,
                        topDoorRoomPrefab,
                        rightDoorRoomPrefab,
                        bottomDoorRoomPrefab,
                        leftDoorRoomPrefab;
    [SerializeField]
    private Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        rooms = new Room[gridSizeX, gridSizeY];
        amountRoom = GameAi.maxRooms;
        CreateStartRoom();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            LogRoomsArray();
        }
    }

    public void CreateStartRoom()
    {
        int randSpawnX = Random.Range(0, gridSizeX);
        int randSpawnY = Random.Range(0, gridSizeY);
        List<GameObject> randSet = GetRandomPrefabSet(randSpawnX, randSpawnY);
        int randIndex = Random.Range(0, randSet.Count);
        GameObject newRoom = Instantiate(randSet[randIndex], new Vector2(randSpawnX * UNITSCALE, randSpawnY * UNITSCALE), Quaternion.identity);
        newRoom.transform.parent = wholeMap.transform;
        currentRoom = newRoom.GetComponent<Room>();
        currentRoom.SetPlayerTransform(player);
        currentRoom.BringPlayerToStartPosition();
        currentRoom.SetPositionInGrid(randSpawnX, randSpawnY);
        rooms[randSpawnX, randSpawnY] = currentRoom;
        amountRoom--;
        // TODO: add condition that not to create room that have door toward outside grid bound
    }

    public void RandomNextTile(Room fromRoom, string toSide = "all")
    {
        // fromRoom is mean room that consider side to random and toSide mean that we will connect room to that side
        // randome room to attrach if fromSide = left mean that should room from rightDoorSet
        // TODO: Connect room is from that side
        bool isCreatingComplete = false;
        List<GameObject> tilePrefabRandomDomain;
        if (toSide == "top")
        {
            if (fromRoom.gridPosY < gridSizeY-1)
            {
                // cal culate position of next room that we is generating
                int newGridPosX = fromRoom.gridPosX, newGridPosY = fromRoom.gridPosY + 1;
                if (rooms[newGridPosX, newGridPosY] == null)
                {
                    tilePrefabRandomDomain = GetRandomPrefabSet(newGridPosX, newGridPosY, "bottom");
                    InstantiateNewRoom(tilePrefabRandomDomain, newGridPosX, newGridPosY);

                    isCreatingComplete = true;
                }
            }
        }
        else if (toSide == "right")
        {
            if (fromRoom.gridPosX < gridSizeX - 1)
            {
                // cal culate position of next room that we is generating
                int newGridPosX = fromRoom.gridPosX +1 , newGridPosY = fromRoom.gridPosY;
                if (rooms[newGridPosX, newGridPosY] == null)
                {
                    tilePrefabRandomDomain = GetRandomPrefabSet(newGridPosX, newGridPosY, "left");
                    InstantiateNewRoom(tilePrefabRandomDomain, newGridPosX, newGridPosY);

                    isCreatingComplete = true;
                }
            }
        }
        else if (toSide == "bottom")
        {
            if (fromRoom.gridPosY > 0)
            {
                // cal culate position of next room that we is generating
                int newGridPosX = fromRoom.gridPosX, newGridPosY = fromRoom.gridPosY - 1;
                if (rooms[newGridPosX, newGridPosY] == null) { 
                    tilePrefabRandomDomain = GetRandomPrefabSet(newGridPosX, newGridPosY, "top");
                    InstantiateNewRoom(tilePrefabRandomDomain, newGridPosX, newGridPosY);

                    isCreatingComplete = true;
                }
            }
        }
        else if (toSide == "left")
        {
            if (fromRoom.gridPosX > 0)
            {
                // cal culate position of next room that we is generating
                int newGridPosX = fromRoom.gridPosX - 1 , newGridPosY = fromRoom.gridPosY;
                if (rooms[newGridPosX, newGridPosY] == null)
                {
                    tilePrefabRandomDomain = GetRandomPrefabSet(newGridPosX, newGridPosY, "right");
                    InstantiateNewRoom(tilePrefabRandomDomain, newGridPosX, newGridPosY);

                    isCreatingComplete = true;
                }
            }
        }
        if (isCreatingComplete)
        {
            amountRoom--;
        }
    }

    public void InstantiateNewRoom(List<GameObject> randSet, 
        int gridPosX, int gridPosY, bool bringPlayerToSpawnPoint = false)
    {
        int randIndex = Random.Range(0, randSet.Count);
        GameObject newRoom = Instantiate(randSet[randIndex], 
                    new Vector2(gridPosX * UNITSCALE, gridPosY * UNITSCALE), Quaternion.identity);
        newRoom.transform.parent = wholeMap.transform;
        currentRoom = newRoom.GetComponent<Room>();
        currentRoom.SetPlayerTransform(player);
        currentRoom.SetPositionInGrid(gridPosX, gridPosY);
        rooms[gridPosX, gridPosY] = currentRoom;
        if (bringPlayerToSpawnPoint)
        {
            currentRoom.BringPlayerToStartPosition();
        }
    }

    public List<GameObject> GetRandomPrefabSet(int posX, int posY, string fromSide = "")
    {
        List<GameObject> randomTileSet = new List<GameObject>();
        foreach (GameObject tile in allRoomPrefab)
        {
            randomTileSet.Add(tile);
        }
        if (posY == 0)
        {
            for (int i = 0; i < randomTileSet.Count; i++)
            {
                if (bottomDoorRoomPrefab.Contains(randomTileSet[i]))
                {
                    randomTileSet.Remove(randomTileSet[i]);
                    i--;
                }
            }
        }
        if (posY == gridSizeY - 1)
        {
            for (int i = 0; i < randomTileSet.Count; i++)
            {
                if (topDoorRoomPrefab.Contains(randomTileSet[i]))
                {
                    randomTileSet.Remove(randomTileSet[i]);
                    i--;
                }
            }
        }
        if (posX == 0)
        {
            for (int i = 0; i < randomTileSet.Count; i++)
            {
                if (leftDoorRoomPrefab.Contains(randomTileSet[i]))
                {
                    randomTileSet.Remove(randomTileSet[i]);
                    i--;
                }
            }
        }
        if (posX == gridSizeX - 1)
        {
            for (int i = 0; i < randomTileSet.Count; i++)
            {
                if (rightDoorRoomPrefab.Contains(randomTileSet[i]))
                {
                    randomTileSet.Remove(randomTileSet[i]);
                    i--;
                }
            }
        }

        string y = "";
        foreach (GameObject x in randomTileSet)
        {
            y += x.name + ", ";
        }  
        Debug.Log(y);
        return randomTileSet;
    }

    public void LogRoomsArray()
    {
        string roomGridLog = "";
        for (int i = 0; i < gridSizeX; i++)
        {
            string roomInCol = i.ToString() + ": ";
            for (int j = 0; j < gridSizeY; j++)
            {
                string atIndexRoom;
                if (rooms[i, j] == null)
                {
                    atIndexRoom = "Empty";
                }else
                {
                    atIndexRoom = rooms[i, j].ToString();
                }
                roomInCol += j.ToString() + "->" +atIndexRoom + " ";
            }
            // Debug.Log(i.ToString() + " _ " + roomInCol);
            roomGridLog += roomInCol + "\n";
        }
        Debug.Log(roomGridLog);
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + ((gridSizeX-1) * UNITSCALE/2) , transform.position.y + ((gridSizeY / 2) * UNITSCALE)),
            new Vector2(gridSizeX * UNITSCALE, gridSizeY * UNITSCALE));
    }
}

