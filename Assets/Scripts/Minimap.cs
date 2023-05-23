using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public GameObject minimap;
    public bool isMinimapShown;

    private int col, row;
    private Room[,] allRooms;

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

    public Image[] minimapSlot;

    

    private void Start()
    {
        // generate minimap from rooms variable from levelgenerator

        Debug.Log("Start generating Minimap");

        if (LevelGenerator.rooms != null && GameManager.isGameRunning)
        {
            Debug.Log("Found rooms in LevelGenerator class");
            allRooms = LevelGenerator.rooms;
            col = LevelGenerator.gridSizeX;
            row = LevelGenerator.gridSizeY;
        }

        isMinimapShown = false;
    }

    private void Update()
    {
        CheckKeyboardInput();
        ShowMinimap();
    }

    private void CheckKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMinimapShown = !isMinimapShown;
        }
    }

    private void ShowMinimap()
    {

        minimap.SetActive(isMinimapShown);
    }

    private List<Sprite> GetSetOfDirectionSprite(Room r)
    {
        if (LevelGenerator.startRoom != null && r == LevelGenerator.startRoom)
        {
            if (GameManager.curRoom != null && r == GameManager.curRoom)
            {
                if (r.istraveled)
                {
                    return tNsNcSprites;
                }
                return startNcurSprites;
            }
            if (r.istraveled)
            {
                return travNStartSprites;
            }
            return startSprites;
        }
        if (LevelGenerator.finishRoom && r == LevelGenerator.finishRoom)
        {
            if (r == GameManager.curRoom)
            {
                return finNcurSprites;
            }
            return finSprites;
        }
        if (GameManager.curRoom != null && r == GameManager.curRoom)
        {
            if (r.istraveled)
            {
                return travNcurSprites;
            }
            return curSprites;
        }
        if (r.istraveled)
        {   
            return travSprites;
        }
        return regSprites;
    }

    private Sprite GetDirectionSprite(Room r)
    {
        List<Sprite> directionSpriteSet = GetSetOfDirectionSprite(r);
        string n = r.GetName();
        
        if (n == "T")
        {
            return directionSpriteSet[0];
        }
        else if (n == "R")
        {
            return directionSpriteSet[1];
        }
        else if (n == "B")
        {
            return directionSpriteSet[2];
        }
        else if (n == "L")
        {
            return directionSpriteSet[3];
        }
        else if (n == "RL" )
        {
            return directionSpriteSet[4];
        }
        else if (n == "RB")
        {
            return directionSpriteSet[5];
        }
        else if (n == "BL")
        {
            return directionSpriteSet[6];
        }
        else if (n == "TR")
        {
            return directionSpriteSet[7];
        }
        else if (n == "TB")
        {
            return directionSpriteSet[8];
        }
        else if (n == "TL")
        {
            return directionSpriteSet[9];
        }
        else if (n == "RBL")
        {
            return directionSpriteSet[10];
        }
        else if (n == "TBL")
        {
            return directionSpriteSet[11];
        }
        else if (n == "TRB")
        {
            return directionSpriteSet[12];
        }
        else if (n == "TRL")
        {
            return directionSpriteSet[13];
        }
        else if (n == "TRBL")
        {
            return directionSpriteSet[14];
        }
        else
        {
            return emptySprite;
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
                Room r = allRooms[x, y];
                int index = (y * 7) + x;
                if (r == null) // case that this position is not contain any room
                {
                    minimapSlot[index].sprite = emptySprite;
                }
                else if (r.GetName() == "wall") // case for r is no door room/ wall room
                {
                    minimapSlot[index].sprite = wallSprite;
                }
                else // mean that is position is contain a room and need more consideration
                {
                    // TODO: things that i want to consider: traveled?, door direction
                    // minimapSlot[index].sprite = regSprites[14];
                    minimapSlot[index].sprite = GetDirectionSprite(r);
                }
            }
        }
        
       
    }

    public void UpdateMinimapAtPosition()
    {
        // update minimap specific at that position in case that player reach that room while playing the run
        foreach (Room r in GameAi.playerRoute)
        {
            int index = (r.gridPosY * 7) + r.gridPosX;
            minimapSlot[index].sprite = GetDirectionSprite(r);
        }
    }
}
