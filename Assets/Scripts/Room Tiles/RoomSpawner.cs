using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Room thisRoom;

    public GameObject player;
    public GameObject finishPortal;

    private void Start()
    {
        thisRoom = GetComponentInParent<Room>();
        player = GameObject.Find("Player");
        CreatePortalIfIsEndRoom();
    }

    private void CreatePortalIfIsEndRoom()
    {
        if (thisRoom.roomType == 2)
        {
            GameObject door = Instantiate(finishPortal, 
                new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
            door.transform.parent = thisRoom.gameObject.transform;
        }
    }


}
