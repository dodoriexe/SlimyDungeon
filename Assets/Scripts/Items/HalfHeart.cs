using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfHeart : MonoBehaviour
{

    private bool used;

    // Start is called before the first frame update
    void Start()
    {
        used = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!used)
            {
                List<HeartCode.Heart> tempList = GameManager._instance.heartCode.GetHeartList();
                if (tempList[tempList.Count - 1].GetFragmentAmount() < 2)
                {
                    used = true;
                    GameManager._instance.heartCode.Heal(1);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
