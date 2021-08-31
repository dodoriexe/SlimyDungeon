using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberKnife : MonoBehaviour
{
    public GameObject playerKnifePrefab;
    public GameObject playerKnifeRightPrefab;

    private bool used;
    public string itemName;
    public string flavorText;

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

                if(GameObject.Find("Playerholdknife") != null)
                {
                    if(GameObject.Find("PlayerholdknifeR") != null)
                    {
                        GameManager._instance.playerObject.GetComponent<PlayerController>().playerHands.GetComponentInChildren<Weapon>().additionalDamage += 1;
                    }
                    else
                    {
                        Instantiate(playerKnifeRightPrefab, GameManager._instance.playerObject.transform).name = "PlayerholdknifeR";
                        GameManager._instance.playerObject.GetComponent<PlayerController>().playerHands.GetComponentInChildren<Weapon>().additionalDamage += 1;
                    }
                }
                else
                {
                    Instantiate(playerKnifePrefab, GameManager._instance.playerObject.transform).name = "Playerholdknife";
                    GameManager._instance.playerObject.GetComponent<PlayerController>().playerHands.GetComponentInChildren<Weapon>().additionalDamage += 1;
                }

                DisplayItemText();
                Destroy(this.gameObject);
            }

        }
    }
}
