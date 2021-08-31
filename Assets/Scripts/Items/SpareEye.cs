using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpareEye : MonoBehaviour
{
    private bool used;
    public string itemName;
    public string flavorText;

    public GameObject eyeGun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayItemText()
    {
        GameManager._instance.newItemPopup.GetComponent<ItemPopup>().ShowNewItemPopUp(this.GetComponent<SpriteRenderer>().sprite, itemName, flavorText);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!used)
            {
                used = true;
                //Debug.Log("Player touched me :(");

                if (GameObject.Find("EyeGun"))
                {
                    GameObject.Find("EyeGun").GetComponent<Weapon>().additionalDamage += 0.1f;
                }
                else
                {
                    GameObject eyeGunny = Instantiate(eyeGun, GameManager._instance.playerObject.GetComponent<PlayerController>().playerHands.transform);
                    eyeGunny.name = "EyeGun";
                    GameManager._instance.playerObject.GetComponent<PlayerController>().playerHands.GetComponent<PlayerHands>().bonusWeapons.Add(eyeGunny.GetComponent<Weapon>());
                }

                DisplayItemText();
                Destroy(this.gameObject);
            }

        }
    }
}
