using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireSpeed = 0.0f; // Time Betweeen Shots
    public int clipAmount = 0;
    public float reloadTime = 0.0f;
    public float additionalDamage; // This is needed for items to give more damage.

    private int ammoInClip = 0;
    private float currReloadTime = 0.0f;
    private float currFireCooldown = 0.0f;

    private bool reloading = false;

    public Projectile projectile;

    public bool isEyeGun;
    public bool inTutorial;

    // Start is called before the first frame update
    void Start()
    {
        ammoInClip = clipAmount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(ammoInClip <= 0 && currReloadTime <= 0)
        {
            ammoInClip = clipAmount;
            reloading = false;
        }
        currReloadTime -= Time.fixedDeltaTime;
        currFireCooldown -= Time.fixedDeltaTime;
    }

    public void Shoot()
    {
        if(ammoInClip <= 0 && currReloadTime <= 0 && !reloading)
        {

            currReloadTime = reloadTime;
            reloading = true;
            // Play animation
        }
        if(currReloadTime <= 0 && ammoInClip > 0 && currFireCooldown <= 0)
        {
            Vector2 characterPos = Camera.main.WorldToScreenPoint(transform.parent.position);
            Vector2 mousePos = Input.mousePosition;

            float angle = Mathf.Atan2(characterPos.y - mousePos.y, characterPos.x - mousePos.x) - Mathf.PI;

            Projectile proj = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0f, 0f, angle * Mathf.Rad2Deg))).GetComponent<Projectile>();

            try
            {
                if (isEyeGun)
                {
                    AudioManager.PlayClip(GameManager._instance.eyeShotSounds);
                }
                else if(GetComponentInParent<PlayerController>().isFakePlayerInMenu)
                {
                    AudioManager.PlayClip(GetComponentInParent<TutorialSounds>().shotSounds);
                }
                else
                {
                    AudioManager.PlayClip(GameManager._instance.shotSounds);
                }
            }
            catch { }

            proj.shooter = transform.parent.parent.gameObject;
            proj.damage = proj.damage + additionalDamage;

            proj.direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            ammoInClip--;
            currFireCooldown = fireSpeed;
        }
    }
}
