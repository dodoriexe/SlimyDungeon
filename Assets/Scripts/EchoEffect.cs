using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    public float timeBetweenSpawns;
    public float startTimeBetweenSpawns;

    public GameObject echo;

    private void Update()
    {
        

        if(GameManager._instance.playerObject != null && timeBetweenSpawns <= 0 && GetComponentInParent<EnemyAI>().room == GameManager._instance.playerObject.GetComponent<PlayerController>().currentRoom)
        {
            //Spawn Echo
            GameObject echoy = Instantiate(echo, transform.position, transform.rotation);
            Destroy(echoy, 1f);
            echoy.transform.localScale = new Vector3(5,5);

            timeBetweenSpawns = startTimeBetweenSpawns;
        }
        else
        {
            timeBetweenSpawns -= Time.deltaTime;
        }
    }

}
