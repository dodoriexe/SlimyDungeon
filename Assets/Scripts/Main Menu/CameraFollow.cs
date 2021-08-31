using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject toFollow;
    public float speed = 0.0f;
    public float boundsPercent = 0.0f;
    public float acceleration = 0.0f;
    public float maxSpeed = 0.0f;
    public float resetTime = 0.0f;
    public float lockLeft = 0.0f;
    public float lockRight = 0.0f;
    public float lockUp = 0.0f;
    public float lockDown = 0.0f;

    private float currentSpeed = 0.0f;
    private float resetCooldown = 0.0f;

    public bool lockCamera = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lockCamera)
            return;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(toFollow.transform.position);

        float sPWidth = (Camera.main.pixelWidth / 100.0f) * boundsPercent;
        float sPHeight = (Camera.main.pixelHeight / 100.0f) * boundsPercent;

        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = (vertExtent * Screen.width / Screen.height);

        if (screenPos.x < sPWidth || screenPos.y < sPHeight || screenPos.x > Camera.main.pixelWidth - sPWidth || screenPos.y > Camera.main.pixelHeight - sPHeight)
        {
            resetCooldown = resetTime;
        }
        else
        {
            if (resetCooldown <= 0)
            {
                currentSpeed = speed;
            }
            resetCooldown -= Time.deltaTime;
        }
        if(resetCooldown > 0)
        {
            if (currentSpeed >= maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
            float interpolation = currentSpeed * Time.deltaTime;

            Vector3 position = this.transform.position;
            position.y = Mathf.Lerp(this.transform.position.y, toFollow.transform.position.y, interpolation);
            position.x = Mathf.Lerp(this.transform.position.x, toFollow.transform.position.x, interpolation);

            if (position.x + horzExtent > lockRight)
            {
                //transform.position.Set(lockRight - horzExtent, transform.position.y, transform.position.z);
                position.x = lockRight - horzExtent;
            }
            if (position.y + vertExtent > lockUp)
            {
                //transform.position.Set(transform.position.x, lockUp - vertExtent, transform.position.z);
                position.y = lockUp - vertExtent;
            }
            if (position.x - horzExtent < lockLeft)
            {
                //transform.position.Set(lockLeft + horzExtent, transform.position.y, transform.position.z);
                position.x = lockLeft + horzExtent;
            }
            if (position.y - vertExtent < lockDown)
            {
                //transform.position.Set(transform.position.x, lockDown + vertExtent, transform.position.z);
                position.y = lockDown + vertExtent;
            }

            this.transform.position = position;
            currentSpeed += acceleration;
        } 
    }
}
