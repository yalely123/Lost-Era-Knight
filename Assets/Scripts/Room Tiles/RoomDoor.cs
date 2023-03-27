using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    public RoomTemplete roomTemplete;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            // TODO: call function in roomTemplete to change room
            //roomTemplete.ChangeRoomTile(name);
            // Debug.Log(name);
        }
    }
}
