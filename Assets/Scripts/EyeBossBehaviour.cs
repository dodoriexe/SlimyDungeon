using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBossBehaviour : MonoBehaviour
{

    public GameObject eyeShotPrefab;

    private EnemyAI ai;
    private Vector3 startingPosition;
    private float currentAttackSpeedCooldown;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<EnemyAI>();

        switch (ai.room.GetComponent<RoomAdder>().bossRoomType)
        {
            case BossRoomType.NONE:
                // This should never happen.
                throw new System.Exception("ERROR: Boss Spawned in a room it shouldn't have.");
            case BossRoomType.DOORBELOW:
                transform.position += new Vector3(0, 1.8f);
                break;
            case BossRoomType.DOORABOVE:
                transform.position += new Vector3(0, -1.8f);
                break;
            case BossRoomType.DOORLEFT:
                transform.position += new Vector3(6, 0);
                break;
            case BossRoomType.DOORRIGHT:
                transform.position += new Vector3(-6, 0);
                break;
            default:
                break;
        }
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!GameManager._instance.playerObject.GetComponent<PlayerController>().frozen && GameManager._instance.playerObject != null && !GameManager._instance.isPlayerDead && GameManager._instance.playerObject.GetComponent<PlayerController>().currentRoom == ai.room)
        {
            if (currentAttackSpeedCooldown <= 0)
            {
                Invoke("ShootAtPlayer", 0.1f);
                Invoke("ShootAtPlayer", 0.2f);
                Invoke("ShootAtPlayer", 0.3f);
                Invoke("ShootAtPlayer", 0.4f);
                Invoke("ShootAtPlayer", 0.5f);
                Invoke("ShootAtPlayer", 0.6f);
                Invoke("ShootAtPlayer", 0.7f);
                Invoke("ShootAtPlayer", 0.8f);
                currentAttackSpeedCooldown = ai.attackSpeed;
            }
            currentAttackSpeedCooldown -= Time.deltaTime;
        }
        transform.position = startingPosition;
    }

    private void ShootAtPlayer()
    {
        Vector2 direction = GameManager._instance.playerObject.transform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(transform.position.y - direction.y, transform.position.x - direction.x) - Mathf.PI;

        GameObject tempGhostBall = Instantiate(eyeShotPrefab, transform.position, Quaternion.Euler(new Vector3(0f, 0f, angle * Mathf.Rad2Deg)));
        Projectile tempProjectile = tempGhostBall.GetComponent<Projectile>();
        AudioManager.PlayClip(GameManager._instance.eyeShotSounds);
        tempProjectile.direction = direction;
        tempProjectile.shooter = this.gameObject;
        currentAttackSpeedCooldown = ai.attackSpeed;
    }


}
