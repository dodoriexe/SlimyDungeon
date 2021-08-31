using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    bool used;

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
        if(other.CompareTag("Player"))
        {
            if(!used)
            {
                used = true;
                GameManager._instance.SwitchToNextFloor();
            }
            
        }
    }
}
