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
        forViewCurRoom = curRoom;
        if (LevelGenerator.isFinishGenerating && curRoom != null) 
        { 
            if (Input.GetKey(KeyCode.M))
            {
                if (curRoom != null)
                {
                    curRoom.KillAllMonsterInRoom();
                }
            }
        }
    }

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


    private void ForcedEndGamePlay() 
    {
        // Forced End the current game play this is for Debugging mode
        // TODO: change scene to try again scene

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        Debug.Log("Game is forced to be END play through by Game Manager!");
        isGameStopped = true;
        isGameRunning = false;

        if (levelGen.finishRoom.isTimeStart)
        {
            levelGen.finishRoom.clearTime = Time.time;
            levelGen.finishRoom.isTimeStart = false;
            levelGen.finishRoom.playTime = levelGen.finishRoom.startTime - levelGen.finishRoom.clearTime;
        }
        
        SceneManager.LoadScene("Victory");
    }

    public static void LoadGameOverScene()
    {
        isGameRunning = false;
        isGamePaused = true;
        Debug.Log("Go to Game Over Scene");
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



}
