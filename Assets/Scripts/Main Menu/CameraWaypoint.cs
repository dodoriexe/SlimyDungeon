using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWaypoint : MonoBehaviour
{

    public bool activated = false;
    public Vector2 goTo;
    public float speed = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activated && goTo != null)
        {
            float interpolation = speed * Time.deltaTime;
            Vector3 position = transform.position;
            if (goTo.x == 0)
                position.x = 0;
            else
                position.x = Mathf.Lerp(transform.position.x, goTo.x, interpolation);
            position.y = Mathf.Lerp(transform.position.y, goTo.y, interpolation);
            transform.position = position;
        }
    }

    public void SetWaypoint(Vector2 point)
    {
        goTo = point;
    }
}
