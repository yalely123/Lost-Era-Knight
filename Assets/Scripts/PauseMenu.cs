using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void ResumeGame()
    {
        Debug.Log("Game is resume playing");
        // TODO: call ClosePauseMenu in GameManager
        GameManager.ClosePauseMenu();
    }

    public void GoToMainMenu()
    {
        Debug.Log("Go to main menu");
        GameManager.ClosePauseMenu();
        GameManager.isGamePaused = true;
        GameAi.isVictory = false;
        SceneManager.LoadScene("Main Menu");
    }
}
