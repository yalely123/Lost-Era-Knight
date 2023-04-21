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
    private bool needToOpenPauseMenu;

    [SerializeField]
    private static GameObject pauseMenu;

    [SerializeField]
    public static Transform player;

    private void Start()
    {
        isGameStopped = false;
        isGameRunning = true;
        isPlayerReachFinishPortal = false;
        pauseMenu = GameObject.Find("PasuseMenu");
        isGamePaused = false;
        pauseMenu.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Transform>();
        //Debug.Log("From Game Manager: ");
        //Debug.Log(player);
    }

    private void Update()
    {
        CheckKeyBoardInput();
        CheckIfPlayerReachFinishPortal();
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
        SceneManager.LoadScene("Victory");
        isGameStopped = true;
        isGameRunning = false;
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
