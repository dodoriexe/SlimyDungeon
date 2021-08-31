using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    public GameObject prefab;
    public Weapon kitchenGun;

    public List<Weapon> bonusWeapons;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gun = Instantiate(prefab, transform.parent.position, Quaternion.identity);
        gun.transform.parent = transform;
        kitchenGun = gun.GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0.1)
        {
            kitchenGun.Shoot();
            foreach (Weapon weapon in bonusWeapons)
            {
                weapon.Shoot();
            }
        }
    }


}
