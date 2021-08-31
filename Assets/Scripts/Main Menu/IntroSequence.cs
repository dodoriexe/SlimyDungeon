using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSequence : MonoBehaviour
{

    public float presentsTime = 0.0f;
    public float dungeonTime = 0.0f;
    public float startTime = 0.0f;

    private float actualPresentTime = 0.0f;
    private float actualDungeonTime = 0.0f;
    private float actualStartTime = 0.0f;

    private bool hasColl = false;

    CameraWaypoint wp;

    void Start()
    {
        wp = Camera.main.GetComponent<CameraWaypoint>();
    }

    void Update()
    {
        if(actualPresentTime <= 0 && presentsTime > 0 && hasColl)
        {
            actualDungeonTime = dungeonTime;
            wp.speed = 0.6f;
            wp.SetWaypoint(new Vector2(0, 45));
            presentsTime = 0;
        }
        if(actualDungeonTime <= 0 && presentsTime == 0 && dungeonTime > 0)
        {
            actualStartTime = startTime;
            wp.speed = 0.9f;
            wp.SetWaypoint(new Vector2(0, 60));
            dungeonTime = 0;
        }
        if(actualStartTime <= 0 && dungeonTime == 0 && startTime > 0)
        {
            GameObject.FindGameObjectWithTag("GameStarter").GetComponent<StartGameScript>().StartGame();
            startTime = 0;
        }
        actualPresentTime -= Time.deltaTime;
        actualDungeonTime -= Time.deltaTime;
        actualStartTime -= Time.deltaTime;
    }

    public void PlayIntro()
    {
        Camera.main.GetComponent<CameraFollow>().lockCamera = true;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        wp.activated = true;
        wp.speed = 0.75f;
        wp.SetWaypoint(new Vector2(0, 30));
        actualPresentTime = presentsTime;
        hasColl = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Projectile"))
        {
            PlayIntro();
        }
    }
}
