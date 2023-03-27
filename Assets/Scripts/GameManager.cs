using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameRunning { get; private set; }

    [SerializeField]
    private bool isGameStopped;

    [SerializeField]
    public static Transform player;

    private void Start()
    {
        isGameStopped = false;
        isGameRunning = true;
        player = GameObject.Find("Player").GetComponent<Transform>();
        Debug.Log("From Game Manager: ");
        Debug.Log(player);
    }

    private void Update()
    {
        CheckKeyBoardInput();
        
    }

    private void CheckKeyBoardInput ()
    {
        if (Input.GetKey(KeyCode.Alpha4)
            && Input.GetKey(KeyCode.Alpha5)
            && Input.GetKey(KeyCode.Alpha6))
        {
            if (!isGameStopped && isGameRunning)
            {
                ForcedEndGamePlay();
            }
        }
    }

    private void ForcedEndGamePlay() 
    {
        // Forced End the current game play this is for Debugging mode
        // TODO: change scene to try again scene

        Debug.Log("Game is forced to be END play through by Game Manager!");
        SceneManager.LoadScene("Victory");
        isGameStopped = true;
        isGameRunning = false;
    }
}
