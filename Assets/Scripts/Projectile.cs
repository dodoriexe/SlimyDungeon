using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum BulletType { BASIC, BOUNCE };

    public BulletType bulletType;
    public Vector2 direction;
    public float damage = 0.0f;
    public float speed = 0.0f;
    public float lifeTime = 0.0f;
    public float knockbackPower = 0.0f;
    public float knockbackTime = 0.0f;

    public GameObject shooter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (bulletType)
        {
            case BulletType.BASIC:
                transform.position += (Vector3)direction * speed * Time.deltaTime;
                break;
            case BulletType.BOUNCE:
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
        lifeTime -= Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("RoomSpawn") && !other.CompareTag("Enemy") && !other.CompareTag("Projectile") && !other.CompareTag("Room Door") && !other.CompareTag("Item"))
        {
            if(other.gameObject != shooter)
            {
                Destroy(gameObject);
            }
        }
    }
}
