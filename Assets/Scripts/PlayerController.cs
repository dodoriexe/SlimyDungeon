using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isFakePlayerInMenu;

    public GameObject playerHands;

    public float moveSpeed;
    public bool isDead;
    public bool frozen;


    private GameObject lastProjectileHit;

    Rigidbody2D playerRigid;
    Vector2 movement;

    public ParticleSystem deathParticles;

    public GameObject currentRoom;

    public bool slimeCut;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();

        Debug.Log(SceneManager.GetActiveScene());

        if(!isFakePlayerInMenu)
        {
            GameManager._instance.playerObject = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovemement = Input.GetAxis("Vertical");
        
        if(horizontalMovement > 0.1f || horizontalMovement < -0.1f || verticalMovemement > 0.1f || verticalMovemement < -0.1f)
        {
            movement = new Vector2(horizontalMovement, verticalMovemement);
            GetComponent<Animator>().SetBool("isMoving", true);
        }
        else
        {
            movement = new Vector2(horizontalMovement, verticalMovemement);
            GetComponent<Animator>().SetBool("isMoving", false);
        }

        GetComponent<Animator>().SetFloat("Horizontal", horizontalMovement);
        GetComponent<Animator>().SetFloat("Vertical", verticalMovemement);

    }

    public void Freeze()
    {
        frozen = true;
    }

    public void UnFreeze()
    {
        frozen = false;
    }

    public void Die()
    {
        isDead = true;
        this.GetComponent<SpriteRenderer>().sprite = null;
        Instantiate(deathParticles, this.transform.position, Quaternion.identity);
        AudioManager.PlayClip(GameManager._instance.playerDeathSound);
        Destroy(this.gameObject, 0.01f);
    }

    void FixedUpdate()
    {
        if(!frozen)
        {
            transform.position += (Vector3)movement * moveSpeed * Time.deltaTime;
        }

        if (currentRoom != null && currentRoom.GetComponent<RoomAdder>().blackScreen != null)
        {
            Destroy(currentRoom.GetComponent<RoomAdder>().blackScreen);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Projectile"))
        {
            if(other.gameObject.GetComponent<Projectile>().shooter != gameObject && other.gameObject != lastProjectileHit)
            {
                lastProjectileHit = other.gameObject;
                Projectile proj = other.gameObject.GetComponent<Projectile>();
                GameManager._instance.heartCode.Damage((int)proj.damage);

                AudioManager.PlayClip(GameManager._instance.slimeHurtSounds);
                Instantiate(GameManager._instance.hurtParticles, transform);

                //Debug.Log((int)proj.damage);

                /*
                animator.Play("Damage_Flash");
                immortalCooldown = immortalTime;
                knockbackCooldown = proj.knockbackTime;
                lastProjectile = proj;
                knockbackAccel = 0.0f;
                */

                Destroy(proj.gameObject);
            }
        }
    }
}
