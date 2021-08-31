using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusHeart : MonoBehaviour
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
        if(other.CompareTag("Player"))
        {
            if(!used)
            {
                used = true;
                //Debug.Log("Player touched me :(");
                GameManager._instance.heartCode.AddNewHeart();
                DisplayItemText();
                Destroy(this.gameObject);
            }

        }
    }
}
