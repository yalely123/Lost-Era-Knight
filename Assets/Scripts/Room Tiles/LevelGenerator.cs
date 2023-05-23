using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class LevelGenerator : MonoBehaviour
{
    public static bool isFinishGenerating;
    public bool isRandomMaxAmountRoom;
    public static int gridSizeX = 7, gridSizeY = 5;
    public int UNITSCALE = 53; // mean that if we spawn room at (0,0) next right room will spawn (53, 0)
    public int maxAmountRoom, amountRoom;
    public int startPosX, startPosY;
    
    [SerializeField]
    private GameObject wholeMap; // Parent Game Object that contain all map tiles in hierarchie
    
    public static Room[,] rooms; // array that contain all room
    public static Room startRoom, finishRoom;
    private Room forInspecStartRoom, forInspecFinishRoom;

    public GameObject fakeRoom,
                      nextTile;

    private Queue<Room> roomConnectQ;
    private List<Room> randSetOfFinishRoom;

    [SerializeField]
    private Room currentRoom;

    public GameObject[] allRoomPrefab,
                        topDoorRoomPrefab,
                        rightDoorRoomPrefab,
                        bottomDoorRoomPrefab,
                        leftDoorRoomPrefab;
    public GameObject wallRoom;
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Minimap minimap;

    private void Awake()
    {
        rooms = new Room[gridSizeX, gridSizeY];
        player = GameObject.Find("Player").GetComponent<Transform>();
        GameAi.GridCol = gridSizeX;
        GameAi.GridRow = gridSizeY;
    }

    private void Start()
    {
        isFinishGenerating = false;
        if (isRandomMaxAmountRoom)
        {
            maxAmountRoom = Random.Range(3, 8);
        }
        amountRoom = maxAmountRoom;
        if (player == null)
        {
            throw new System.ArgumentException("Cannot find player gameobject");
        }
        //amountRoom = GameAi.maxRooms;
        roomConnectQ = new Queue<Room>();
        randSetOfFinishRoom = new List<Room>();
        GenerateNewLevel();
        isFinishGenerating = true;
        minimap.GenerateMiniMap();
        forInspecStartRoom = startRoom; 
        forInspecFinishRoom = finishRoom;
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
        int randSpawnX = Random.Range(0, gridSizeX); // create random x and y position for position of starting room
        int randSpawnY = Random.Range(0, gridSizeY);
        List<GameObject> randSet = GetRandomPrefabSet(randSpawnX, randSpawnY);
        int randIndex = Random.Range(0, randSet.Count-1);
        GameObject newRoomTile = Instantiate(randSet[randIndex], new Vector2(randSpawnX * UNITSCALE, randSpawnY * UNITSCALE), Quaternion.identity); // instantiate tile that's randomed to random position
        newRoomTile.transform.parent = wholeMap.transform; // bring gameObject of new room that we created to be a child of map gameObject in hierarchy
        Room newRoom = newRoomTile.GetComponent<Room>(); // set current room to be the room which has just created
        if (rooms[randSpawnX, randSpawnY] == null)
        { 
            rooms[randSpawnX, randSpawnY] = newRoom;
            rooms[randSpawnX, randSpawnY].SetPlayerTransform(player);
            rooms[randSpawnX, randSpawnY].BringPlayerToStartPosition();
            rooms[randSpawnX, randSpawnY].SetPositionInGrid(randSpawnX, randSpawnY); // tell the instance of class room to make it know position itself in grid context
            rooms[randSpawnX, randSpawnY].SetBoolDoor();
        }
        else
            Debug.Log(string.Format("Something went wrong: room array in this position({0}, {1}) already has room before adding in array", randSpawnX, randSpawnX));
        

        amountRoom--;

        startPosX = randSpawnX;
        startPosY = randSpawnY;
        startRoom = newRoom;
        //LogRoomsArray();
    }

    public void CreateNewRoom(List<GameObject> randSet, int gridPosX, int gridPosY)
    {
        // instantiate room that random from connectable tile set and then set up that room

        //Debug.Log("Log from CreateNewRoom function");

        int randIndex = Random.Range(0, randSet.Count - 1);
        GameObject newRoomTile = Instantiate(randSet[randIndex], 
                    new Vector2(gridPosX * UNITSCALE, gridPosY * UNITSCALE), Quaternion.identity);
        newRoomTile.transform.parent = wholeMap.transform;
        Room newRoom = newRoomTile.GetComponent<Room>();
        if (rooms[gridPosX, gridPosY] == null)
        {
            rooms[gridPosX, gridPosY] = newRoom;
            rooms[gridPosX, gridPosY].SetPlayerTransform(player);
            rooms[gridPosX, gridPosY].SetPositionInGrid(gridPosX, gridPosY);
            rooms[gridPosX, gridPosY].SetBoolDoor();
            rooms[gridPosX, gridPosY].LogAllRoomData();
            roomConnectQ.Enqueue(rooms[gridPosX, gridPosY]);
            amountRoom--;

            // for adding to set that we will random to be the finish room
            if (rooms[gridPosX, gridPosY].GetName().Length == 1 && rooms[gridPosX, gridPosY] != startRoom)
            {
                randSetOfFinishRoom.Add(rooms[gridPosX, gridPosY]);
            }
        }

        else 
        { 
            throw new System.ArgumentException((string.
                Format("Something went wrong: room array in this position({0}, {1}) already has room before adding in array", gridPosX, gridPosY))); 
        }
    }


    public List<GameObject> GetRandomPrefabSet(int posX, int posY)
    {
        // find that what tile can possible to connect consider by position in grid
        // return set of tiles that can connect and be a next room for generating level process

        List<GameObject> randomTileSet = allRoomPrefab.ToList(); // make that list is a all possible tiles

        // filter tiles from condition that will make that side can not connect

        // 1. out of grid bound: tile that has door connect to bound of grid will be filterd
        if (posX == 0)
        {
            for (int i = 0; i < leftDoorRoomPrefab.Length; i++)
            {
                if (randomTileSet.Contains(leftDoorRoomPrefab[i])) {
                    randomTileSet.Remove(leftDoorRoomPrefab[i]);
                }
            }
        }
        if (posX == gridSizeX-1)
        {
            for (int i = 0; i < rightDoorRoomPrefab.Length; i++)
            {
                if (randomTileSet.Contains(rightDoorRoomPrefab[i]))
                {
                    randomTileSet.Remove(rightDoorRoomPrefab[i]);
                }
            }

        }
        if (posY == 0)
        {
            for (int i = 0; i < bottomDoorRoomPrefab.Length; i++)
            {
                if (randomTileSet.Contains(bottomDoorRoomPrefab[i]))
                {
                    randomTileSet.Remove(bottomDoorRoomPrefab[i]);
                }
            }
        }
        if (posY == gridSizeY-1)
        {
            for (int i = 0; i < topDoorRoomPrefab.Length; i++)
            {
                if (randomTileSet.Contains(topDoorRoomPrefab[i]))
                {
                    randomTileSet.Remove(topDoorRoomPrefab[i]);
                }
            }
        }

        // 2. filter room that can connect if there are room before from other side of room that is connected

        if (posX - 1 >= 0 && rooms[posX-1, posY] != null) // checking left side of this room
        {
            Room sideRoom = rooms[posX - 1, posY];
            List<GameObject> temp = new List<GameObject>();
            if (sideRoom.hasRightDoor)
            {
                // this room must have left door
                foreach (GameObject tile in randomTileSet)
                {
                    if (leftDoorRoomPrefab.Contains(tile))
                    {
                        temp.Add(tile);
                    }
                }
                randomTileSet = temp;
            }
            else
            {
                // this room mustn't have left door (filter all possible left door)
                foreach (GameObject tile in randomTileSet)
                {
                    if (!leftDoorRoomPrefab.Contains(tile))
                    {
                        temp.Add(tile);
                    }
                }
                randomTileSet = temp;
            }
        }
        if (posY + 1 < gridSizeY && rooms[posX, posY + 1] != null) // checking top side of this room
        {
            Room sideRoom = rooms[posX, posY + 1];
            List<GameObject> temp = new List<GameObject>();
            if (sideRoom.hasBottomDoor)
            {
                // this room must have top door
                foreach (GameObject tile in randomTileSet)
                {
                    if (topDoorRoomPrefab.Contains(tile))
                    {
                        temp.Add(tile);
                    }
                }
                randomTileSet = temp;
            }
            else
            {
                // this room mustn't have top door (filter all possible top door)
                foreach (GameObject tile in randomTileSet)
                {
                    if (!topDoorRoomPrefab.Contains(tile))
                    {
                        temp.Add(tile);
                    }
                }
                randomTileSet = temp;
            }
        }
        if (posX + 1 < gridSizeX && rooms[posX + 1, posY] != null) // checking right side of this room
        {
            Room sideRoom = rooms[posX + 1, posY];
            List<GameObject> temp = new List<GameObject>();
            if (sideRoom.hasLeftDoor)
            {
                // this room must have right door
                foreach (GameObject tile in randomTileSet)
                {
                    if (rightDoorRoomPrefab.Contains(tile))
                    {
                        temp.Add(tile);
                    }
                }
                randomTileSet = temp;
            }
            else
            {
                // this room mustn't have right door (filter all possible right door)
                foreach (GameObject tile in randomTileSet)
                {
                    if (!rightDoorRoomPrefab.Contains(tile))
                    {
                        temp.Add(tile);
                    }
                }
                randomTileSet = temp;
            }
        }
        if (posY - 1 >= 0 && rooms[posX, posY - 1] != null) // checking bottom side of this room
        {
            Room sideRoom = rooms[posX, posY - 1];
            List<GameObject> temp = new List<GameObject>();
            if (sideRoom.hasTopDoor)
            {
                // this room must have bottom door
                foreach (GameObject tile in randomTileSet)
                {
                    if (bottomDoorRoomPrefab.Contains(tile))
                    {
                        temp.Add(tile);
                    }
                }
                randomTileSet = temp;
            }
            else 
            {
                // this room mustn't have bottom door (filter all possible bottom door)
                foreach (GameObject tile in randomTileSet)
                {
                    if (!bottomDoorRoomPrefab.Contains(tile))
                    {
                        temp.Add(tile);
                    }
                }
                randomTileSet = temp;
            }
        }

        string allPossiTile = "";
        foreach (GameObject tile in randomTileSet)
        {
            allPossiTile += tile.name + ", ";
        }
        Debug.Log(string.Format("All Possible Tile in this Position ({0}, {1})\n{2}",posX, posY,allPossiTile));

        return randomTileSet;
    }

    public void ChangeNotWorkTile() // change tile that has early deadend exist to be a tile that has door for continue generate
    {

    }

    public void CloseAllOpenDoorAfterGeneratingMap() // close all door of room that now in queue but we generate map reach score or amount of room already
    {
        while(roomConnectQ.Count > 0)
        {
            currentRoom = roomConnectQ.Dequeue();
            currentRoom.SetBoolDoor();
            bool isClosed =  false;
            GameObject wall;
            if (currentRoom.hasTopDoor)
            {
                wall = Instantiate(wallRoom, new Vector2(currentRoom.gridPosX * UNITSCALE, (currentRoom.gridPosY + 1) * UNITSCALE), Quaternion.identity);
                wall.transform.parent = wholeMap.transform;
                EmptyRoom empRoom = wall.GetComponent<EmptyRoom>();
                empRoom.gridPosX = currentRoom.gridPosX; empRoom.gridPosY = currentRoom.gridPosY + 1;
                rooms[currentRoom.gridPosX, currentRoom.gridPosY + 1] = empRoom;
                isClosed = true;

            }
            if (currentRoom.hasRightDoor)
            {
                wall = Instantiate(wallRoom, new Vector2((currentRoom.gridPosX + 1) * UNITSCALE, currentRoom.gridPosY * UNITSCALE), Quaternion.identity);
                wall.transform.parent = wholeMap.transform;
                EmptyRoom empRoom = wall.GetComponent<EmptyRoom>();
                empRoom.gridPosX = currentRoom.gridPosX + 1; empRoom.gridPosY = currentRoom.gridPosY;
                rooms[currentRoom.gridPosX + 1, currentRoom.gridPosY] = empRoom;
                isClosed = true;
            }
            if (currentRoom.hasLeftDoor)
            {
                wall = Instantiate(wallRoom, new Vector2((currentRoom.gridPosX - 1) * UNITSCALE, currentRoom.gridPosY * UNITSCALE), Quaternion.identity);
                wall.transform.parent = wholeMap.transform;
                EmptyRoom empRoom = wall.GetComponent<EmptyRoom>();
                empRoom.gridPosX = currentRoom.gridPosX - 1; empRoom.gridPosY = currentRoom.gridPosY;
                rooms[currentRoom.gridPosX - 1, currentRoom.gridPosY] = empRoom;
                isClosed = true;
            }
            if (currentRoom.hasBottomDoor)
            {
                wall = Instantiate(wallRoom, new Vector2(currentRoom.gridPosX * UNITSCALE, (currentRoom.gridPosY - 1) * UNITSCALE), Quaternion.identity);
                wall.transform.parent = wholeMap.transform;
                EmptyRoom empRoom = wall.GetComponent<EmptyRoom>();
                empRoom.gridPosX = currentRoom.gridPosX; empRoom.gridPosY = currentRoom.gridPosY - 1;
                rooms[currentRoom.gridPosX, currentRoom.gridPosY - 1] = empRoom; 
                isClosed = true;
            }

            if (isClosed) // mean that thi room is edge room so we can set this room to be a finish room which will be randomed next step
            {
                randSetOfFinishRoom.Add(currentRoom);
            }

        }
    }

    public void GenerateNewLevel()
    {
        if (amountRoom != maxAmountRoom)
        {
            throw new System.ArgumentException("At start: Max amount rooms != amount room or something went wrong before start generating level");
        } else
        {
            Debug.Log("Room number: 1");
            CreateStartRoom();
            //Debug.Log("going to log current room data");
            startRoom.LogAllRoomData();
            roomConnectQ.Enqueue(startRoom);
            currentRoom = startRoom;
            rooms[currentRoom.gridPosX, currentRoom.gridPosY].SetBoolDoor();

            string currentQ = "";
            foreach (Room r in roomConnectQ)
            {
                currentQ += r.name + ", ";
            }
            Debug.Log("Now, Queu contain: " + currentQ);


            int i = 0;
            //while (roomConnectQ.Count > 0 && i < 4)
            while (roomConnectQ.Count > 0 && amountRoom > 0)
            {
                Debug.Log("Room number: " + (i +2));
                currentRoom = roomConnectQ.Peek(); // peek method returns what queue will dequeue next
                rooms[currentRoom.gridPosX, currentRoom.gridPosY].SetBoolDoor();

                Debug.Log("Now current room name: " + currentRoom.name);
                //Debug.Log("Current Room name = " + currentRoom.name);
                // random that which door is open we will go for it
                List<char> allSide = currentRoom.getAllConnectableDoor();
                Debug.Log(string.Format("this room has top = {0}, right = {1}, bottom = {2}, left = {3}", currentRoom.hasTopDoor, currentRoom.hasRightDoor, currentRoom.hasBottomDoor, currentRoom.hasLeftDoor));
                //Debug.Log("allSide value = " + allSide.Count);

                
                if (allSide.Count > 0)
                {
                    int index = Random.Range(0, allSide.Count-1);
                    // choose/random room from side that we gonna connect
                    if (allSide[index] == 'T') {
                        Debug.Log(string.Format("adding to top side of current room ({0})", currentRoom.name));
                        CreateNewRoom(GetRandomPrefabSet(currentRoom.gridPosX, currentRoom.gridPosY + 1), currentRoom.gridPosX, currentRoom.gridPosY + 1);
                        
                    }
                    else if (allSide[index] == 'R') {
                        Debug.Log(string.Format("adding to right side of current room ({0})", currentRoom.name));
                        CreateNewRoom(GetRandomPrefabSet(currentRoom.gridPosX + 1, currentRoom.gridPosY), currentRoom.gridPosX + 1, currentRoom.gridPosY);
                        
                    }
                    else if (allSide[index] == 'B') {
                        Debug.Log(string.Format("adding to bottom side of current room ({0})", currentRoom.name));
                        CreateNewRoom(GetRandomPrefabSet(currentRoom.gridPosX, currentRoom.gridPosY - 1), currentRoom.gridPosX, currentRoom.gridPosY - 1);
                        
                    }
                    else{ // allside[index] == 'L'
                        Debug.Log(string.Format("adding to left side of current room ({0})", currentRoom.name));
                        CreateNewRoom(GetRandomPrefabSet(currentRoom.gridPosX - 1, currentRoom.gridPosY), currentRoom.gridPosX - 1, currentRoom.gridPosY );
                        
                    }
                    i++;
                }

                currentRoom.SetBoolDoor();
                allSide = currentRoom.getAllConnectableDoor();
                string curRoomDoorAvailable = "";
                foreach(char door in allSide)
                {
                    curRoomDoorAvailable += door + ", ";
                }
                Debug.Log(string.Format("Room ->{0}<-'s connectable door({1}): {2}", currentRoom.name, allSide.Count, curRoomDoorAvailable));

                // edit later zone
                //if (roomConnectQ.Count > 0) { roomConnectQ.Dequeue(); }
                // end zone

                /*
                if (!currentRoom.hasBottomDoor && !currentRoom.hasLeftDoor && !currentRoom.hasRightDoor && currentRoom.hasTopDoor)
                {
                    roomConnectQ.Dequeue();
                }
                */
                if (allSide.Count == 0)
                {
                    roomConnectQ.Dequeue();
                }

                currentQ = "";
                foreach (Room r in roomConnectQ)
                {
                    currentQ += r.name + ", ";
                }
                Debug.Log("Now, Queu contain: " + currentQ);
            
            
            }

            CloseAllOpenDoorAfterGeneratingMap();
            int temp = Random.Range(0, randSetOfFinishRoom.Count - 1);
            randSetOfFinishRoom[temp].SpawnFinishPortal();
            finishRoom = randSetOfFinishRoom[temp];

            UpdateGameAIGrid();

            LogRoomsArray();
            
        }

    }

    public static void UpdateGameAIGrid()
    {
        if (GameAi.roomGrid == null)
        {
            GameAi.roomGrid = new Room[gridSizeX, gridSizeY];
        }
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                GameAi.roomGrid[x, y] = rooms[x, y];
            }
        }
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

