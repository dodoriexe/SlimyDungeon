using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpCrab : MonoBehaviour
{
    public Sprite crabTexture;

    SpriteRenderer render;
    Animator animator;
    EnemyAI faker;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        faker = GetComponent<EnemyAI>();
        render = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        animator.enabled = false;
        body.mass = 9999;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Projectile proj = other.gameObject.GetComponent<Projectile>();
            animator.Play("Damage_Flash");
            Destroy(proj.gameObject);
            faker.disabled = false;
            animator.enabled = true;
            body.mass = 1;
            Destroy(this);
        }
    }
}
