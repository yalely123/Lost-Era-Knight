using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoomData
{
    public string RoomName { get; private set; }
    public bool IsTravel { get; private set; }
    public int GridPosX { get; private set; }
    public int GridPosY { get; private set; }
    public float PlayTime { get; private set; }

    public RoomData(string name, int posX, int posY, float playTime, bool isTravel)
    {
        RoomName = name;
        GridPosX = posX;
        GridPosY = posY;
        PlayTime = playTime;
        IsTravel = isTravel;
        
    }
    public override string ToString()
    {
        return string.Format("This call from struct room name " + RoomName);
    }

}
