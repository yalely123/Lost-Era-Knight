using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    // private bool istraveled = false;
    public GameObject[] topPossibleAttachRoom,
                        rightPossibleAttachRoom,
                        buttomPossibleAttachRoom,
                        leftPossibleAttachRoom;
    public Room topAttrachRoom;
    public Room rightAttrachRoom;
    public Room buttomAttrachRoom;
    public Room leftAttrachRoom;

    public Transform door;
    public Transform player;

    private void Start()
    {
        //door = transform.Find("RoomDoor");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform == door)
            {
                Debug.Log("Tung Tong");
            }
            
        }
    }

    // TODO: collect all room that can traverse or traversed
}
