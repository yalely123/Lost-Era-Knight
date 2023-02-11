using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameAi
{
    // this class control overall workflow of the game
    // to make that use public static to make this class global
    public static int monsterKillCount = 0,
                      deadCount = 0,
                      getHitCount = 0;

    public static float levelScore;

    public static Vector2 playerDeadPostion;
    
    public static GameObject[] monsterPrefab; // contain all monster prefab of project for monster generating
                                              // future problem: referencing which is only can acces by index of array. not something specific like name of monter
    public static GameObject[] monsterLeft = { }; // contain all monster that still on current level in game hierachy and generating queue

    public static void LogMonsterKillCount()
    {
        Debug.Log(monsterKillCount);
    }

    public static void KillAllMonsters()
    {
        Debug.Log("All monsters are deleted!");
        monsterLeft = GameObject.FindGameObjectsWithTag("Monster");
        int index = monsterLeft.Length - 1;
        Debug.Log(index);
        for (; index >= 0; index--) { 
            Object.Destroy(monsterLeft[index]);
        }
    }

    public static void CreateNewPlayer()
    {
        // Create new player object when player object is deleted by dying
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

    public static void spawnMonster(float posX, float posY, int monNum = -1)
    {
        // monNum = -1 mean that spawn a random monster
        // otherwise mean that spawn monster by monster prefab's index
        int monsterPrefabIndex;
        Vector2 spawnPos = new Vector2(posX, posY);
        if (monNum == -1)
        {
            monsterPrefabIndex = Random.Range(0, monsterPrefab.Length);
        } else
        {
            monsterPrefabIndex = monNum;
        }
        Object.Instantiate(monsterPrefab[monsterPrefabIndex], spawnPos, Quaternion.identity);
        
    }

    public static int getNumberOfMonsterInMap()
    {
        return monsterLeft.Length;
    }
}
