using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject listObject;
    public GameObject emptyImage;
    public GameObject scoreObject;

    private void Awake()
    {
        scoreObject.GetComponent<TMPro.TextMeshProUGUI>().text += GameManager._instance.currentFloor;
        Destroy(OneAndOnlyCanvas._instance.gameObject);
        foreach (Sprite item in GameManager._instance.collectedItems)
        {
            GameObject tempImage = Instantiate(emptyImage, listObject.transform);
            tempImage.GetComponent<Image>().sprite = item;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
