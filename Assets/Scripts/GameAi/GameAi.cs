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

    public static int maxAmountRoom;
    
    public static float levelStartTime, levelFinishTime;

    public static bool isVictory = false;

    public static int GridRow, GridCol;
    public static Room[,] roomGrid { get; set; }
    public static List<Room> playerRoute = new List<Room>();
    public static float routeAverageTimeLast, 
                        routeAverageTimeCur,
                        wholeMapAverageTime;

    public static Dictionary<string, float> allRoomWeight = new Dictionary<string, float>();
    public static bool isFirstSetupWeight = false;

    public static float levelScore = 0;
    public static bool isFirstLevelSetUp = false;
    public static bool isFirstRun = true;

    public static int runNum = 0;


    public static void ResetDataForNewRun()
    {
        monsterKillCount = 0;
        playerRoute.Clear();
    }

    public static void FirstSetUpAllRoomWeight()
    {
        allRoomWeight.Add("B", 42f);
        allRoomWeight.Add("BL", 30f);
        allRoomWeight.Add("L", 29f);
        allRoomWeight.Add("R", 41f);
        allRoomWeight.Add("RB", 60f);
        allRoomWeight.Add("RBL", 56f);
        allRoomWeight.Add("RL", 27f);
        allRoomWeight.Add("T", 61f);
        allRoomWeight.Add("TB", 36f);
        allRoomWeight.Add("TBL", 29f);
        allRoomWeight.Add("TL", 45f);
        allRoomWeight.Add("TR", 41f);
        allRoomWeight.Add("TRB", 34f);
        allRoomWeight.Add("TRBL", 30f);
        allRoomWeight.Add("TRL", 52f);
        isFirstSetupWeight = true;

        FirstLevelSetUp();
    }

    public static void FirstLevelSetUp()
    {
        maxAmountRoom = Random.Range(6, 9);
        float averageAllRoomWeight = FindAverageAllWeight();

        levelScore = maxAmountRoom * averageAllRoomWeight;

        isFirstLevelSetUp = true;
        Debug.Log("Level Score: " + levelScore);
    }

    public static float CalculateLevelScore()
    {
        // TODO: Calculate Score for map generation in next run through
        wholeMapAverageTime = FindAverageWholeMap();
        routeAverageTimeLast = routeAverageTimeCur;
        routeAverageTimeCur = FindAverageRouteTime();
        int routeRoomNumber = playerRoute.Count;
        float sumRouteTime = GetSumWeightInRoute();
        Debug.Log("level Score - sum route time = " + (levelScore - sumRouteTime));
        levelScore = levelScore - sumRouteTime;
        AdjustWeight();
        sumRouteTime = GetSumWeightInRoute();
        levelScore += sumRouteTime;

        Debug.Log("Call Function cal culate level Score reuturn: " + levelScore);

        return levelScore;
    
    }

    public static void AdjustWeight()
    {
        // TODO: adjust weight each room in route that player pass in each run
        foreach (Room r in playerRoute)
        {
            string name = r.GetName();
            allRoomWeight[name] = (allRoomWeight[name] + r.playTime) / 2;
        }
    }

    public static float FindAverageRouteTime()
    {
        float average = 0;
        int roomAmount = 0;
        foreach(Room r in playerRoute)
        {
            if (r.isRoomCleared)
            {
                average += r.playTime;
                roomAmount += 1;
            }
        }
        if (roomAmount != 0 )
        {
            average = average / roomAmount;
        }
        else
        {
            average = -1; // No count this round try to play again
        }
        return average;
    }

    private static float FindAverageWholeMap()
    {
        float average = 0;
        float roomNum = 0;
        for (int y = 0; y < GridRow; y++)
        {
            for (int x = 0; x < GridCol; x++)
            {
                Room r = roomGrid[x, y];
                if (IsRegularRoom(r))
                {
                    average += allRoomWeight[r.GetName()];
                    roomNum += 1;
                }
            }
        }
        if (roomNum > 0)
        {
            average /= roomNum;
        }

        return average;
    }

    private static float FindAverageAllWeight()
    {
        float average = 0;
        int weightAmount = 15;
        foreach(KeyValuePair<string, float> rw in allRoomWeight)
        {
            //Debug.Log(string.Format("Key = {0}, Value = {1}", rw.Key, rw.Value));
            average += rw.Value;
        }
        average = average / weightAmount;
        //Debug.Log("Average All weight map = " + average);
        return average;
    }

    public static float GetSumPlayTimeInGrid()
    {
        float time = 0;
        for (int y = 0; y < GridRow; y++)
        {
            for (int x = 0; x < GridCol; x++)
            {
                if ( IsRegularRoom(roomGrid[x,y]))
                {
                    time += allRoomWeight[roomGrid[x, y].GetName()];
                }
            }
        }
        return time;
    }

    public static float GetSumPlayTimeInRoute()
    {
        float time = 0;
        foreach (Room r in playerRoute)
        {
            time += r.playTime;
        }
        return time;
    }

    public static float GetSumWeightInRoute()
    {
        float weight = 0;
        foreach (Room r in playerRoute)
        {
            weight += allRoomWeight[r.GetName()];
        }
        return weight;
    }

    public static int GetNumberOfRoomInGrid()
    {
        int amount = 0;
        for (int y = 0; y < GridRow; y++)
        {
            for (int x = 0; x < GridCol; x++)
            {
                if (IsRegularRoom(roomGrid[x, y]))
                {
                    amount += 1;
                }
            }
        }
        return amount;
    }

    private static bool IsRegularRoom(Room r)
    {
        return (r != null && r.GetName() != "wall");
    }

    #region Log Function

    public static void LogAllRouteName()
    {
        string s = "";
        foreach(Room r in playerRoute)
        {
            s += r.GetName();
        }
        Debug.Log(s);
    }

    public static void LogMonsterKillCount()
    {
        Debug.Log(monsterKillCount);
    }

    #endregion

}
