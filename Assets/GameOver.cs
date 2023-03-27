using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private float timeEnterScene;
    private float inputCoolDown = 3f;

    private void Start()
    {
        timeEnterScene = Time.time;
    }
    private void Update()
    {
        if (Time.time > timeEnterScene + inputCoolDown)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ContinueTheGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                LoadMainMenu();
            }
        }
    }
    public void ContinueTheGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
