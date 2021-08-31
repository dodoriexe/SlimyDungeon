using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator animator;

    public GameObject room;

    public float health = 0.0f;
    public int damage = 0;
    public float movementSpeed = 0.0f;
    public float attackSpeed = 0.0f;
    public float immortalTime = 0.0f;
    public bool isBoss = false;
    public bool disabled = false;

    private float attackCooldown = 0.0f;
    private float immortalCooldown = 0.0f;
    private float knockbackCooldown = 0.0f;

    private Projectile lastProjectile;
    private float knockbackAccel = 0.0f;

    public AudioClip deathSound;
    public AudioClip[] shotSounds;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackCooldown > 0)
        {
            knockbackAccel += lastProjectile.knockbackPower;
            transform.position += (Vector3)lastProjectile.direction * knockbackAccel;
            knockbackCooldown -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if(health <= 0)
        {
            room.GetComponent<RoomAdder>().enemies.Remove(this.gameObject);

            if(isBoss)
            {
                GameManager._instance.SpawnTrapDoor();

                int rnd = Random.Range(0, GameManager._instance.BossItemRewards.Length);

                Instantiate(GameManager._instance.BossItemRewards[rnd], room.transform.position, Quaternion.identity);
            }
            else
            {
                if(Random.Range(0, 4) == 3)
                {
                    int aRnd = Random.Range(0, GameManager._instance.EnemyItemRewards.Length);
                    Instantiate(GameManager._instance.EnemyItemRewards[aRnd], this.transform.position, Quaternion.identity);
                }
            }
            if(deathSound != null)
            {
                AudioManager.PlayClip(deathSound);
            }
            Destroy(gameObject);
        }
        attackCooldown -= Time.fixedDeltaTime;
        immortalCooldown -= Time.fixedDeltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && attackCooldown <= 0)
        {
            GameManager._instance.heartCode.Damage(damage);
            AudioManager.PlayClip(GameManager._instance.slimeHurtSounds);
            Instantiate(GameManager._instance.hurtParticles, GameManager._instance.playerObject.transform);
            attackCooldown = attackSpeed;
        }
        if(other.gameObject.CompareTag("Projectile") && immortalCooldown <= 0 && other.gameObject.GetComponent<Projectile>().shooter == GameManager._instance.playerObject)
        {
            Projectile proj = other.gameObject.GetComponent<Projectile>();
            health -= proj.damage;
            animator.Play("Damage_Flash");
            immortalCooldown = immortalTime;
            knockbackCooldown = proj.knockbackTime;
            lastProjectile = proj;
            knockbackAccel = 0.0f;
            Destroy(proj.gameObject);
        }
    }
}
