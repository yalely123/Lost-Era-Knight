using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameRunning { get; private set; }
    public static bool isPlayerReachFinishPortal;
    public static bool isGamePaused;

    [SerializeField]
    private bool isGameStopped;

    [SerializeField]
    private static GameObject pauseMenu;

    [SerializeField]
    private GameObject dataVisualizer;
    private bool isDataShown;

    [SerializeField]
    private LevelGenerator levelGen;
    [SerializeField]
    public static Transform playerTransform;
    [SerializeField]
    private GameObject playerPrefab;
    public static GameObject player;
    public static Vector2 playerSpawnPos;

    // tracking and collecting data from player
    public static Room curRoom; // room that player in now in
    [SerializeField]
    private Room forViewCurRoom;
    public static bool isDoorShouldBeClosed;



    private void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        if (playerTransform == null)
        {
            throw new System.ArgumentException("Cannot find player transform");
        }

        if (!GameAi.isFirstSetupWeight)
        {
            GameAi.FirstSetUpAllRoomWeight();
        }

    }

    private void Start()
    {
        //GameAi.CreateNewPlayer();
        //Instantiate(GameManager.player, Vector2.zero, Quaternion.identity);
        isGameStopped = false;
        isGameRunning = true;
        isPlayerReachFinishPortal = false;
        pauseMenu = GameObject.Find("PasuseMenu");
        isGamePaused = false;
        pauseMenu.SetActive(false);
        curRoom = forViewCurRoom;
        
        //Debug.Log("From Game Manager: ");
        //Debug.Log(player);
        isDataShown = false;
        dataVisualizer.SetActive(false);
        GameAi.ResetDataForNewRun();

        isDoorShouldBeClosed = true;
    }

    private void Update()
    {
        CheckKeyBoardInput();
        CheckIfPlayerReachFinishPortal();
        CheckIfDoorNeedToClose();
        //CheckWhatScene();

        forViewCurRoom = curRoom;


    }

    #region Check Function

    private void CheckKeyBoardInput ()
    {
        // for Debugging change scene to victory scene
        if (Input.GetKey(KeyCode.Alpha4)
            && Input.GetKey(KeyCode.Alpha5)
            && Input.GetKey(KeyCode.Alpha6))
        {
            if (!isGameStopped && isGameRunning)
            {
                ForcedEndGamePlay();
            }
        }

        // for open pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused) { 
                OpenPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isDataShown = !isDataShown;
            dataVisualizer.SetActive(isDataShown);
        }

        if (LevelGenerator.isFinishGenerating && curRoom != null)
        {
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
            {
                if (curRoom != null)
                {
                    curRoom.KillAllMonsterInRoom();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            string s = "";
            foreach (Room r in GameAi.playerRoute)
            {
                s += r.name;
                s += ", ";
            }
            Debug.Log(s);
        }
    }

    private void CheckIfDoorNeedToClose() // this will close all map tile door when player start play on that tile and open when that tile is cleared
    {
        if (curRoom.isRoomCleared)
        {
            isDoorShouldBeClosed = false;
        }

        if (!curRoom.isRoomCleared)
        {
            isDoorShouldBeClosed = true;
        }
    }

    private void CheckWhatScene()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (!GameAi.isFirstSetupWeight)
            {
                GameAi.FirstSetUpAllRoomWeight();
            }
        }
    }

    #endregion

    public static void SetCurrentRoom(Room r)
    {
        curRoom = r;
        //Debug.Log("In Set Current Room function isTimeStart = " + curRoom.isTimeStart);
        if (curRoom.isTimeStart && !curRoom.isAddedToRoute)
        {
            GameAi.playerRoute.Add(curRoom);
            curRoom.isAddedToRoute = true;
            Debug.Log("Trying to add room in player route, route member: " + GameAi.playerRoute.Count);
            curRoom.istraveled = true;
        }
    }

    #region About Loading Scene

    private void ForcedEndGamePlay() 
    {
        // Forced End the current game play this is for Debugging mode

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        Debug.Log("Game is forced to be END play through by Game Manager!");
        isGameStopped = true;
        isGameRunning = false;

        if (LevelGenerator.finishRoom.isTimeStart)
        {
            LevelGenerator.finishRoom.clearTime = Time.time;
            LevelGenerator.finishRoom.isTimeStart = false;
            LevelGenerator.finishRoom.playTime = LevelGenerator.finishRoom.clearTime - LevelGenerator.finishRoom.startTime;
        }

        LevelGenerator.UpdateGameAIGrid();
        isPlayerReachFinishPortal = false;
        GameAi.isVictory = true;

        //GameAi.AdjustWeight();
        GameAi.CalculateLevelScore();

        SceneManager.LoadScene("Victory");
    }

    public static void LoadGameOverScene()
    {
        isGameRunning = false;
        isGamePaused = true;
        GameAi.isVictory = false;
        Debug.Log("Go to Game Over Scene");
        LevelGenerator.UpdateGameAIGrid();

        //GameAi.AdjustWeight();
        GameAi.CalculateLevelScore();

        SceneManager.LoadScene("Game Over");
    }

    public static void SetGameBeforeStart()
    {
        // TODO: set all variable to false or true to make game ready to start
    }

    private void CheckIfPlayerReachFinishPortal()
    {
        if (isPlayerReachFinishPortal)
        {
            ForcedEndGamePlay();
        }
    }

    private void OpenPauseMenu()
    {
        // TODO: pause game then open pause menu and minimap
        // 1. pause game
        // 2. set active pause menu
        Debug.Log("Pause Menu is opened");
        if (!isGamePaused)
        {
            isGamePaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public static void ClosePauseMenu()
    {
        Debug.Log("Pause Menu is closed");
        isGamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    #endregion

}
