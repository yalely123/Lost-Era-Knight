using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Minimap : MonoBehaviour
{
    private int col, row;

    public Sprite wallSprite, emptySprite;
    public List<Sprite>
        regSprites,
        finSprites,
        startSprites,
        curSprites,
        finNcurSprites,
        startNcurSprites,
        travSprites,
        travNStartSprites,
        tNsNcSprites,
        travNcurSprites;

    public GameObject[] minimapSlot;

    private void Start()
    {
        // generate minimap from rooms variable from levelgenerator

        if (LevelGenerator.rooms != null)
        {
            col = LevelGenerator.gridSizeX;
            row = LevelGenerator.gridSizeY;
            // GenerateMiniMap();
        }else
        {
            throw new System.ArgumentException("Something wrong with rooms variable in Levelgenerator");
        }
        
    }


    public void GenerateMiniMap()
    {
        // for frist time generate minimap

        
        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < col; x++)
            {
                // TODO: read every position in rooms and select sprite that match for each room in each position
                Room r = LevelGenerator.rooms[x, y];
                if (r == null) // case that this position is not contain any room
                {

                }
                else if (r.getName() == "wall") // case for r is no door room/ wall room
                {

                }
                else // mean that is position is contain a room and need more consideration
                {
                    // TODO: things that i want to consider: traveled?, door direction
                }
            }
        }
        
       
    }

    public void UpdateMinimapAtPosition(int x, int y)
    {
        // update minimap specific at that position in case that player reach that room while playing the run

    }
}
