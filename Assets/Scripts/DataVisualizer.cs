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

    public TMP_Text dataText;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (GameManager.isGameRunning)
        {
            InGameUpdate();
        }
        dataText.text = content;
    }

    private void InGameUpdate()
    {
        levelScore = GameAi.levelScore;
        time = Time.time - startTime;
        monsterKill = GameAi.monsterKillCount;
        healthRemain = GameAi.healthRemain;
        content = string.Format("Level Score: {3:0}\nTime(sec): {0:0}\nMonster Kill Count: {1}\nHealth Remain: {2}", time, monsterKill, healthRemain, levelScore);
    }

}
