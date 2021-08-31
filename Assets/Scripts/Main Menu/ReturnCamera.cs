using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnCamera : MonoBehaviour
{

    CameraFollow follow;
    CameraWaypoint waypoint;
    MoveCameraOnTouch move;
    // Start is called before the first frame update
    void Start()
    {
        follow = Camera.main.GetComponent<CameraFollow>();
        move = GameObject.FindGameObjectWithTag("MoveCamera").GetComponent<MoveCameraOnTouch>();
        waypoint = Camera.main.GetComponent<CameraWaypoint>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (move.activated && !other.CompareTag("Projectile"))
        {
            Camera.main.transform.position = move.oldCameraPos;
            follow.lockCamera = false;
            move.activated = false;
            waypoint.activated = false;
        }
    }
}
