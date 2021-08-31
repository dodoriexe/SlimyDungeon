using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAdder : MonoBehaviour
{

    public GameObject trapDoorPos;
    public GameObject blackScreen;
    public BossRoomType bossRoomType;

    public List<GameObject> Doors;
    public List<GameObject> enemies;

    public bool hadEnemies;
    public bool hasEnemies;
    public bool isFirstRoom;
    public bool isSpawnRoom;
    private bool itemChanceRoll;

    void Awake()
    {
        foreach (GameObject door in Doors)
        {
            door.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager._instance.rooms.Add(this.gameObject);

        if(isFirstRoom)
        {
            GameManager._instance.firstRoom = this.gameObject;
        }
        itemChanceRoll = true;
    }

    private void Update()
    {
        if(hasEnemies && GameManager._instance.playerObject != null && GameManager._instance.playerObject.GetComponent<PlayerController>().currentRoom == this.gameObject)
        {
            foreach (GameObject door in Doors)
            {
                door.gameObject.SetActive(true);
            }
        }

        if (GameManager._instance.playerObject != null && !GameManager._instance.isPlayerDead && GameManager._instance.playerObject.GetComponent<PlayerController>().currentRoom != this.gameObject && Doors.Count != 0 && Doors[0].activeSelf)
        {
            foreach (GameObject door in Doors)
            {
                door.gameObject.SetActive(false);
            }
        }

        if (hadEnemies && enemies.Count == 0)
        {
            hasEnemies = false;
            foreach (GameObject door in Doors)
            {
                door.gameObject.SetActive(false);
            }

            if(itemChanceRoll)
            {
                if (!isSpawnRoom && this.gameObject != GameManager._instance.bossRoom && Random.Range(0, 7) == 4)
                {
                    Instantiate(GameManager._instance.BossItemRewards[Random.Range(0, GameManager._instance.BossItemRewards.Length)], this.transform.position, Quaternion.identity);
                }
                itemChanceRoll = false;
            }

        }
    }
}

public enum BossRoomType
{
    NONE,
    DOORBELOW,
    DOORABOVE,
    DOORLEFT,
    DOORRIGHT
}
