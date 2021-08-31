using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCuttingKnife : MonoBehaviour
{
    private bool used;
    public string itemName;
    public string flavorText;

    // Start is called before the first frame update
    void Start()
    {
        used = false;
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

                //TODO: Play Slime Sound
                AudioManager.PlayClip(GameManager._instance.shotSounds);

                if(GameManager._instance.playerObject.GetComponent<PlayerController>().slimeCut)
                {
                    GameManager._instance.playerObject.GetComponent<PlayerController>().moveSpeed += 0.3f;
                }
                else
                {
                    Vector3 tempScale = GameManager._instance.playerObject.transform.localScale;
                    GameManager._instance.playerObject.transform.localScale -= new Vector3(tempScale.x / 5, tempScale.y / 5, 0);
                    GameManager._instance.playerObject.GetComponent<PlayerController>().moveSpeed += GameManager._instance.playerObject.GetComponent<PlayerController>().moveSpeed * 0.3f;
                    GameManager._instance.playerObject.GetComponent<PlayerController>().slimeCut = true;
                }

                DisplayItemText();
                Destroy(this.gameObject);
            }

        }
    }
}
