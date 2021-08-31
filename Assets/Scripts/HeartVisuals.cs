using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartVisuals : MonoBehaviour
{

    public Sprite emptyHeart;
    public Sprite halfHeart;
    public Sprite fullHeart;

    private HeartCode heartCode;
    private List<HeartImage> heartImages;
    
    void Awake()
    {
        heartImages = new List<HeartImage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetHeartCode(GameManager._instance.heartCode);
    }

    public void SetHeartCode(HeartCode heartCode)
    {
        this.heartCode = heartCode;

        for (int i = 0; i < heartCode.GetHeartList().Count; i++)
        {
            CreateHeartImage().SetHeartFragment(heartCode.GetHeartList()[i].GetFragmentAmount());
        }

        heartCode.onDamaged += HeartCode_onDamaged;
        heartCode.onHealed += HeartCode_onHealed;
        heartCode.onAddNewHeart += HeartCode_onAddNewHeart;
    }

    private void HeartCode_onAddNewHeart(object sender, EventArgs e)
    {
        // Need to re-init the whole heart system.
        for (int i = 0; i < heartImages.Count; i++)
        {
            Destroy(heartImages[i].heartImage.gameObject);
        }
        heartImages.Clear();

        for (int j = 0; j < heartCode.GetHeartList().Count; j++)
        {
            CreateHeartImage().SetHeartFragment(heartCode.GetHeartList()[j].GetFragmentAmount());
        }

        //RefreshHearts();
    }

    private void HeartCode_onHealed(object sender, EventArgs e)
    {
        RefreshHearts();
    }

    private void HeartCode_onDamaged(object sender, EventArgs e)
    {
        RefreshHearts();
    }

    private void RefreshHearts()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].SetHeartFragment(heartCode.GetHeartList()[i].GetFragmentAmount());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public HeartImage CreateHeartImage()
    {
        GameObject heartGameObject = new GameObject("Heart", typeof(Image));
        heartGameObject.transform.SetParent(transform);
        heartGameObject.transform.localScale = new Vector3(1, 1, 1);
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = fullHeart;

        HeartImage heartImage = new HeartImage(this, heartImageUI);

        heartImages.Add(heartImage);

        return heartImage;
    }
}

public class HeartImage
{
    private HeartVisuals heartVisuals;
    public Image heartImage;

    public HeartImage(HeartVisuals heartVisuals, Image heartImage)
    {
        this.heartVisuals = heartVisuals;
        this.heartImage = heartImage;
    }

    public void SetHeartFragment(int fragment)
    {
        switch (fragment)
        {
            case 0: heartImage.sprite = heartVisuals.emptyHeart; break;
            case 1: heartImage.sprite = heartVisuals.halfHeart; break;
            case 2: heartImage.sprite = heartVisuals.fullHeart; break;
        }
    }

}
