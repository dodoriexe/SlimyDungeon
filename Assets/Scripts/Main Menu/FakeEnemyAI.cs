using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemyAI : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator animator;

    public GameObject room;

    public float health = 0.0f;
    public int damage = 0;
    public float movementSpeed = 0.0f;
    public float attackSpeed = 0.0f;
    public float immortalTime = 0.0f;

    public bool disabled = false;

    private float attackCooldown = 0.0f;
    private float immortalCooldown = 0.0f;
    private float knockbackCooldown = 0.0f;

    private Projectile lastProjectile;
    private float knockbackAccel = 0.0f;

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
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        attackCooldown -= Time.fixedDeltaTime;
        immortalCooldown -= Time.fixedDeltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!disabled)
        {
            if (other.gameObject.CompareTag("Player") && attackCooldown <= 0)
            {
                other.gameObject.GetComponent<Animator>().Play("Damage_Flash");
                attackCooldown = attackSpeed;
            }
            if (other.gameObject.CompareTag("Projectile") && immortalCooldown <= 0)
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
}
