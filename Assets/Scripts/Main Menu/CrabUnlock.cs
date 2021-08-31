using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabUnlock : MonoBehaviour
{

    GameObject[] crabs;
    private bool unlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        crabs = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(crabsDead() && !unlocked)
        {
            unlocked = true;
            Destroy(GameObject.FindGameObjectWithTag("Room Door"));
            GameObject[] clearTags = GameObject.FindGameObjectsWithTag("ClearTag");
            foreach(GameObject ob in clearTags)
            {
                Destroy(ob);
            }
        }
    }

    bool crabsDead()
    {
        crabs = GameObject.FindGameObjectsWithTag("Enemy");
        return (crabs.Length == 0);
    }
}
