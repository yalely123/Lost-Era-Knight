using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameAi
{
    // this class control overall workflow of the game
    // to make that use public static to make this class global
    public static int monsterKillCount = 0;
    public static int deadCount = 0;
    public static int getHitCount = 0;
    public static Vector2 playerDeadPostion;
    public static float levelScore;
    public static GameObject[] monsterPrefab; // cantain all monster prefab of project for monster generating
                                              // future problem: referencing which is only can acces by index of array.
    public static GameObject[] monsterLeft; // contain all monster that still on current level in game hierachy and generating queue

    

    public static void Start()
    {
        Debug.Log("In start method of GameAi");
    }

    public static void LogMonsterKillCount()
    {
        Debug.Log(monsterKillCount);
    }

    public static void KillAllMonsters()
    {
        Debug.Log("All monsters are deleted!");
        monsterLeft = GameObject.FindGameObjectsWithTag("Monster");
        int index = monsterLeft.Length - 1;
        for (; index >= 0; index--) { 
            Object.Destroy(monsterLeft[index]);
        }
    }

    public static void CreateNewPlayer()
    {
        // Create new player object when player object is deleted
        // find how to update status/info of player in GameAI to new player object
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
}
