using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject player;

    public void bringPlayerToStartPosition()
    {
        player.transform.Translate(transform.position);
    }
}
