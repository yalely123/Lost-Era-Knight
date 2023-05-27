using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataVisualizer : MonoBehaviour
{
    public static string content;
    public int monsterKill, healthRemain;
    public float startTime, time;
    public float levelScore;
    public string routeString = "";

    public TMP_Text dataText;

    private void Start()
    {
        startTime = Time.time;

        GameAi.LogAllRouteName();
    }

    private void Update()
    {
        if (GameManager.isGameRunning)
        {
            InGameUpdate();
        }
        dataText.text = content;
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
        levelScore = GameAi.levelScore;
        time = Time.time - startTime;
        monsterKill = GameAi.monsterKillCount;
        healthRemain = GameAi.healthRemain;
        routeString = UpdateRoute();
        content = string.Format("Level Score: {3:0}\nTime(sec): {0:0}\nMonster Kill Count: {1}\nHealth Remain: {2}\nCurrentRoom: {5}\nRoute({6}): {4}",
            time, monsterKill, healthRemain, levelScore, routeString, GameManager.curRoom.GetName(), GameAi.playerRoute.Count);
    }

}
