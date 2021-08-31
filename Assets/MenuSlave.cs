using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuSlave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMainMenu()
    {
        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("Canvas"));

        SceneManager.LoadScene(0);
    }
}
