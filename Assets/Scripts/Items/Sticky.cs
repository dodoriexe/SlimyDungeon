using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    private bool used;
    public string itemName;
    public string flavorText;

    public GameObject stickyGun;

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

                if(GameObject.Find("StickyGun"))
                {
                    if(GameObject.Find("StickyGun").GetComponent<Weapon>().fireSpeed <= .5f)
                    {

                    }
                    else
                    {
                        GameObject.Find("StickyGun").GetComponent<Weapon>().fireSpeed -= .5f;
                    }
                    GameObject.Find("StickyGun").GetComponent<SpriteRenderer>().color += new Color(-.2f, -.2f, 0, 0);
                }
                else
                {
                    GameObject stickyGunny = Instantiate(stickyGun, GameManager._instance.playerObject.GetComponent<PlayerController>().playerHands.transform);
                    stickyGunny.name = "StickyGun";
                    GameManager._instance.playerObject.GetComponent<PlayerController>().playerHands.GetComponent<PlayerHands>().bonusWeapons.Add(stickyGunny.GetComponent<Weapon>());
                }

                DisplayItemText();
                Destroy(this.gameObject);
            }

        }
    }
}
