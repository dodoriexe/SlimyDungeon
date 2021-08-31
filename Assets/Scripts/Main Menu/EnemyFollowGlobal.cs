using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowGlobal : MonoBehaviour
{

    private FakeEnemyAI ai;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<FakeEnemyAI>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ai.disabled) {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            if (go != null)
            {
                Vector2 direction = go.transform.position - transform.position;
                direction.Normalize();
                if(direction.x < 0)
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
}
