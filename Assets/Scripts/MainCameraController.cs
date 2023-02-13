using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public float dampTime = 0.05f;
    public Vector3 velocity = Vector2.zero; // short hand of Vector2(0, 0)
    public Transform target;
    Camera followCamera;
    void Start()
    {
        followCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (target) // checking that target is already set
        {
            Vector3 point = followCamera.WorldToViewportPoint(target.position); // point is current position of player in form of Vector3
            Vector3 delta = target.position - followCamera.ViewportToWorldPoint(
                                                new Vector3(0.5f, 0.25f, point.z)); // 0.5 means that middle of 
            Vector3 destination = transform.position + delta;
            transform.position = destination;
        }
       
    }
}
