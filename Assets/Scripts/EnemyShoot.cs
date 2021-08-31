using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    private EnemyAI ai;
    public GameObject ghostBallPrefab;

    private float currentAttackSpeedCooldown;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<EnemyAI>();
        currentAttackSpeedCooldown = ai.attackSpeed;
    }

    private void FixedUpdate()
    {
        if (GameManager._instance.playerObject != null && !GameManager._instance.isPlayerDead && GameManager._instance.playerObject.GetComponent<PlayerController>().currentRoom == ai.room)
        {
            currentAttackSpeedCooldown -= Time.fixedDeltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager._instance.playerObject.GetComponent<PlayerController>().frozen && !GameManager._instance.isPlayerDead && GameManager._instance.playerObject.GetComponent<PlayerController>().currentRoom == ai.room)
        {
            if(currentAttackSpeedCooldown <= 0)
            {
                Vector2 direction = GameManager._instance.playerObject.transform.position - transform.position;
                direction.Normalize();

                float angle = Mathf.Atan2(transform.position.y - direction.y, transform.position.x - direction.x) - Mathf.PI;

                GameObject tempGhostBall = Instantiate(ghostBallPrefab, transform.position, Quaternion.Euler(new Vector3(0f, 0f, angle * Mathf.Rad2Deg)));
                Projectile tempProjectile = tempGhostBall.GetComponent<Projectile>();
                if(ai.shotSounds != null)
                {
                    AudioManager.PlayClip(ai.shotSounds);
                }
                tempProjectile.direction = direction;
                tempProjectile.shooter = this.gameObject;
                currentAttackSpeedCooldown = ai.attackSpeed;
            }
        }
    }
}
