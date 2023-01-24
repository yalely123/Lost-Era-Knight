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
    private static GameObject[] monsterLeft;

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
}
