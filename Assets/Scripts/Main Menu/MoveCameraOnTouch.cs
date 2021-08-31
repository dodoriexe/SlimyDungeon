using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraOnTouch : MonoBehaviour
{
    public bool activated = false;
    public Vector3 oldCameraPos;
    CameraWaypoint wp;
    // Start is called before the first frame update
    void Start()
    {
        wp = Camera.main.GetComponent<CameraWaypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && !other.CompareTag("Projectile"))
        {
            activated = true;
            oldCameraPos = Camera.main.transform.position;
            Camera.main.GetComponent<CameraFollow>().lockCamera = true;
            Camera.main.GetComponent<CameraWaypoint>().activated = true;
            wp.speed = 1.5f;
            wp.SetWaypoint(new Vector2(-24.0f, -9.5f));
        }
    }
}
