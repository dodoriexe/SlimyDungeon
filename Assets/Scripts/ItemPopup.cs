using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour
{
    public GameObject itemImageObject;
    public TextMeshProUGUI UIItemNameText;
    public TextMeshProUGUI UIItemFlavorText;

    private Animator animator;

    public float showingTime;
    private float showingTimeRemaining;
    bool previouslyShowing;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameManager._instance.newItemPopup = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(showingTimeRemaining > 0)
        {
            showingTimeRemaining--;
        }
        if(showingTimeRemaining <= 0 && previouslyShowing)
        {
            previouslyShowing = false;
            animator.Play("HideItem");
        }
    }

    public void ShowNewItemPopUp(Sprite sprite, string nameText, string flavorText)
    {
        GameManager._instance.collectedItems.Add(sprite);

        itemImageObject.GetComponent<Image>().sprite = sprite;
        UIItemNameText.text = nameText;
        UIItemFlavorText.text = flavorText;

        animator.Play("ShowItem");
        showingTimeRemaining = showingTime;
        previouslyShowing = true;

    }
}
