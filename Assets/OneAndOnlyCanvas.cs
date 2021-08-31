using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneAndOnlyCanvas : MonoBehaviour
{

    public static OneAndOnlyCanvas _instance;

    public GameObject MiniMap;
    public GameObject currentFloorText;
    public GameObject Hearts;
    public GameObject loadingScreen;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
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
