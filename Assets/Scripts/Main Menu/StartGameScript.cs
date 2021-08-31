using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGameScript : MonoBehaviour
{

    public GameObject GameManager;
    public GameObject player;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            StartGame();
            //GameObject.Find("IntroActivator").GetComponent<IntroSequence>().PlayIntro();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        GameObject gameManagery = Instantiate(GameManager);
        GameObject playery = Instantiate(player, Vector3.zero, Quaternion.identity);

        gameManagery.name = "GameManager";
        playery.name = "Player";

        gameManagery.GetComponent<GameManager>().playerObject = playery;
    }
}
