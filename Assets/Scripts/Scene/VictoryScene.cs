using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    public float inputCoolDownDuration;
    private float timeEnterScene;

    private void Start()
    {
        timeEnterScene = Time.time;
    }

    private void Update()
    {
        if (Time.time > timeEnterScene + inputCoolDownDuration)
        {
            if (Input.anyKey && !Input.GetKey(KeyCode.M) && !Input.GetKey(KeyCode.Backspace))
            {
                LoadMainMenu();
            }
            
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
