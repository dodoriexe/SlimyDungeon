using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBossBehaviour : MonoBehaviour
{
    public GameObject skullShotPrefab;

    private EnemyAI ai;
    private bool movingNegative;
    private Vector3 startingPosition;
    private float currentAttackSpeedCooldown;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<EnemyAI>();
        movingNegative = true;

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
        startingPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager._instance.isPlayerDead && GameManager._instance.playerObject.GetComponent<PlayerController>().currentRoom == ai.room)
        {
            switch (ai.room.GetComponent<RoomAdder>().bossRoomType)
            {
                case BossRoomType.NONE:
                    // This should never happen.
                    throw new System.Exception("ERROR: Boss Spawned in a room it shouldn't have.");
                case BossRoomType.DOORBELOW:

                    DoorBelowMovement();

                    break;
                case BossRoomType.DOORABOVE:

                    DoorBelowMovement();

                    break;
                case BossRoomType.DOORLEFT:
                    DoorSideMovement();
                    break;
                case BossRoomType.DOORRIGHT:
                    DoorSideMovement();
                    break;
                default:
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if(!GameManager._instance.playerObject.GetComponent<PlayerController>().frozen && GameManager._instance.playerObject != null && !GameManager._instance.isPlayerDead && GameManager._instance.playerObject.GetComponent<PlayerController>().currentRoom == ai.room)
        {
            if(currentAttackSpeedCooldown <= 0)
            {
                ShootAtPlayer();
                Invoke("ShootAtPlayer", 0.3f);
                Invoke("ShootAtPlayer", 0.6f);
                currentAttackSpeedCooldown = ai.attackSpeed;
            }
            currentAttackSpeedCooldown -= Time.deltaTime;
        }
    }

    void SwitchMovement()
    {
        if(movingNegative)
        {
            movingNegative = false;
        }
        else
        {
            movingNegative = true;
        }
    }

    void DoorBelowMovement()
    {
        if(movingNegative)
        {
            this.transform.position += Vector3.left * ai.movementSpeed;
            if(transform.position.x < startingPosition.x + -5.5f)
            {
                SwitchMovement();
            }
        }
        else
        {
            this.transform.position += Vector3.right * ai.movementSpeed;
            if (transform.position.x > startingPosition.x + 5.5f)
            {
                SwitchMovement();
            }
        }
    }

    void DoorSideMovement()
    {
        if (movingNegative)
        {
            this.transform.position += Vector3.down * (ai.movementSpeed / 4);
            if (transform.position.y < startingPosition.y + -2f)
            {
                ShootAtPlayer();
                SwitchMovement();
            }
        }
        else
        {
            this.transform.position += Vector3.up * (ai.movementSpeed / 4);
            if (transform.position.y > startingPosition.y + 2f)
            {
                ShootAtPlayer();
                SwitchMovement();
            }
        }
    }

    private void ShootAtPlayer()
    {
        Vector2 direction = GameManager._instance.playerObject.transform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(transform.position.y - direction.y, transform.position.x - direction.x) - Mathf.PI;

        GameObject tempGhostBall = Instantiate(skullShotPrefab, transform.position, Quaternion.Euler(new Vector3(0f, 0f, angle * Mathf.Rad2Deg)));
        Projectile tempProjectile = tempGhostBall.GetComponent<Projectile>();
        AudioManager.PlayClip(GameManager._instance.skeletonShotSounds);
        tempProjectile.direction = direction;
        tempProjectile.shooter = this.gameObject;
        currentAttackSpeedCooldown = ai.attackSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 8)
        {
            SwitchMovement();
        }
    }
}
