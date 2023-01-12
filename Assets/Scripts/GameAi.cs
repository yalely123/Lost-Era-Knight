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

    public static void logMonsterKillCount ()
    {
        Debug.Log(monsterKillCount);
    }
}
