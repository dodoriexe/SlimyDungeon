using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public int currentFloor;

    public int maxTotalOpenRooms;
    public int maxOpenRooms;
    public int startingHearts;

    public bool isPlayerDead;
    public bool dungeonGenerated;

    public List<GameObject> rooms;
    public GameObject spawnRoom;
    public GameObject bossRoom;
    public GameObject firstRoom; //First Room after Spawn

    public GameObject playerObject;
    public GameObject cameraObject;
    public GameObject newItemPopup;

    public GameObject[] EnemyPrefabs;
    public GameObject[] BossPrefabs;
    public GameObject[] EnemyItemRewards;
    public GameObject[] BossItemRewards;
    public GameObject trapDoorPrefab;
    public GameObject hurtParticles;

    public List<Sprite> collectedItems;

    public AudioClip[] shotSounds;
    public AudioClip playerDeathSound;
    public AudioClip[] skeletonShotSounds;
    public AudioClip[] eyeShotSounds;
    public AudioClip[] slimeHurtSounds;

    public HeartCode heartCode;

    public event EventHandler onDungeonGenerated;

    // Start is called before the first frame update
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        rooms = new List<GameObject>();
        heartCode = new HeartCode(startingHearts);
        playerObject = null;
    }

    private void Start()
    {
        dungeonGenerated = false;
        heartCode.onDead += HeartCode_OnDead;
        playerObject = GameObject.Find("Player");
    }

    public void SpawnTrapDoor()
    {
        Instantiate(trapDoorPrefab, bossRoom.GetComponent<RoomAdder>().trapDoorPos.transform);
    }

    private void HeartCode_OnDead(object sender, EventArgs e)
    {
        if(!isPlayerDead)
        {
            playerObject.GetComponent<PlayerController>().Die();
            isPlayerDead = true;

            Invoke("SwitchToGameOverScreen", 1f);
        }
    }

    void SwitchToGameOverScreen()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void SwitchToNextFloor()
    {
        dungeonGenerated = false;
        OneAndOnlyCanvas._instance.loadingScreen.SetActive(true);
        OneAndOnlyCanvas._instance.currentFloorText.SetActive(false);
        OneAndOnlyCanvas._instance.Hearts.SetActive(false);
        OneAndOnlyCanvas._instance.MiniMap.SetActive(false);

        currentFloor++;
        maxTotalOpenRooms++;
        OneAndOnlyCanvas._instance.currentFloorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Current Floor: " + currentFloor;
        maxOpenRooms = maxTotalOpenRooms;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager._instance.playerObject.GetComponent<PlayerController>().Freeze();
        rooms = new List<GameObject>();
        playerObject.GetComponentInChildren<TrailRenderer>().Clear();
        playerObject.transform.position = Vector3.zero;
        playerObject.transform.rotation = Quaternion.identity;
    }

    public void OnDungeonGenerated()
    {
        if(playerObject.GetComponent<PlayerController>().currentRoom == null)
        {
            playerObject.GetComponent<PlayerController>().currentRoom = spawnRoom;
        }


        OneAndOnlyCanvas._instance.loadingScreen.SetActive(false);
        OneAndOnlyCanvas._instance.currentFloorText.SetActive(true);
        OneAndOnlyCanvas._instance.Hearts.SetActive(true);
        OneAndOnlyCanvas._instance.MiniMap.SetActive(true);

        playerObject.GetComponent<PlayerController>().UnFreeze();
        OneAndOnlyCanvas._instance.currentFloorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Current Floor: " + currentFloor;
        dungeonGenerated = true;
        if (onDungeonGenerated != null) onDungeonGenerated(this, EventArgs.Empty);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
