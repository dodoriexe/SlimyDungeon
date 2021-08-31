using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupersonicAccelerator : MonoBehaviour
{
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

                if(GameManager._instance.playerObject.GetComponent<PlayerController>().playerHands.GetComponent<PlayerHands>().kitchenGun.fireSpeed <= .1f)
                {

                }
                else if (GameManager._instance.playerObject.GetComponent<PlayerController>().playerHands.GetComponent<PlayerHands>().kitchenGun.fireSpeed <= .6f)
                {
                    GameManager._instance.playerObject.GetComponent<PlayerController>().playerHands.GetComponent<PlayerHands>().kitchenGun.fireSpeed = 0.1f;
                    GameManager._instance.playerObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f);
                }
                else
                {
                    GameManager._instance.playerObject.GetComponent<PlayerController>().playerHands.GetComponent<PlayerHands>().kitchenGun.fireSpeed -= 0.5f;
                    GameManager._instance.playerObject.GetComponent<SpriteRenderer>().color += new Color(-.3f, -.3f, 0f);
                }

                DisplayItemText();
                Destroy(this.gameObject);
            }

        }
    }
}
