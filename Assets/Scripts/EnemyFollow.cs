using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    private EnemyAI ai;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager._instance.playerObject.GetComponent<PlayerController>().frozen && !ai.disabled && GameManager._instance.playerObject != null && !GameManager._instance.isPlayerDead && GameManager._instance.playerObject.GetComponent<PlayerController>().currentRoom == ai.room)
        {
            Vector2 direction = GameManager._instance.playerObject.transform.position - transform.position;
            direction.Normalize();
            if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }
            transform.position += (Vector3)direction * ai.movementSpeed * Time.deltaTime;
        }
    }
}
