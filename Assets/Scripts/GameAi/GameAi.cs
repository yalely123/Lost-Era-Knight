using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameAi
{
    // this class control overall workflow of the game
    // to make that use public static to make this class global
    public static int monsterKillCount = 0,
                      healthRemain,
                      deadCount,
                      victoryCount = 0;
    
    public static float levelStartTime, levelFinishTime;
    
    public static bool isVictory;

    public static float levelScore = 9999;

    public static List<Room> playerRoute = new List<Room>();
    public static int GridRow, GridCol;
    public static Room[,] roomGrid { get; set; }
    
    

    public static void LogMonsterKillCount()
    {
        Debug.Log(monsterKillCount);
    }


    public static void CreateNewPlayer()
    {
        // Create new player object when player object is deleted by dying
        // find how to update status/info of player in GameAI to new player object
        GameObject.Instantiate(GameManager.player, Vector2.zero, Quaternion.identity);
    }

    public static GameObject[] GenerateMonsterByLevel(string level)
    {
        GameObject[] monsterInLevel = { new GameObject() };
        return monsterInLevel;
        // generate monster by using level score to game hierachy and game scene
        // return: array of all monster that will be on this level
    }

    public static float CalculateLevelScore()
    {
        float level = 1;
        levelScore = level;
        return level;

        // Calculate level score from player behavior statistic
        // then contain level to class property
        // and return/ call GenerateMonsterByLevel
        // for generating monster in next play round of player
    }

    public static void ResetDataForNewRun()
    {
        monsterKillCount = 0;
        playerRoute.Clear();
    }
}
