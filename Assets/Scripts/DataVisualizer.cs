using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DataVisualizer : MonoBehaviour
{
    public static string content1;
    public static string content2;
    public int monsterKill, healthRemain;
    public float startTime, time;
    public float levelScore;
    public string routeString = "";
    public string playerGodMode;

    public TMP_Text dataText;
    public TMP_Text weightText;

    private void Start()
    {
        startTime = Time.time;

        //GameAi.LogAllRouteName();
    }

    private void Update()
    {
        if (GameManager.isGameRunning)
        {
            InGameUpdate();
        }
        levelScore = GameAi.levelScore;
        dataText.text = content1;

        if (GameAi.isFirstSetupWeight)
        {
            WeightDataUpdate();
        }
        weightText.text = content2;
    }

    private string UpdateRoute()
    {
        //Debug.Log("In function Update Route startfunction");
        string s = "";
        int i = 0, m = GameAi.playerRoute.Count;
        foreach(Room r in GameAi.playerRoute)
        {
            s += r.GetName();
            if (i < m-1)
            {
                s += " -> ";
            }
            i++;
        }
        //Debug.Log("Log from updateRoute function s =" + s);
        return s;
    }

    private void InGameUpdate()
    {
        
        time = Time.time - startTime;
        monsterKill = GameAi.monsterKillCount;
        healthRemain = GameAi.healthRemain;
        routeString = UpdateRoute();
        if (PlayerController.godModeOn)
        {
            playerGodMode = "On";
        }else
        {
            playerGodMode = "Off";
        }

        if (SceneManager.GetActiveScene().name != "Game")
        {
            time = 0f;
        }
        content1 = string.Format("Level Score: {3:.00}\n{0:0}\nMonster Kill Count: {1}\nHealth Remain: {2}\nCurrentRoom: {5}\nRoute({6}): {4}\n God Mode: {7}",
            time, monsterKill, healthRemain, "", routeString, GameManager.curRoom.GetName(), GameAi.playerRoute.Count, playerGodMode);
    }

    private void WeightDataUpdate()
    {
        content2 = "level Score = " + GameAi.levelScore + "\n";
        foreach (KeyValuePair<string, float> rf in GameAi.allRoomWeight)
        {
            content2 += string.Format("{0} : {1}\n", rf.Key, rf.Value);
        }
    }

}
