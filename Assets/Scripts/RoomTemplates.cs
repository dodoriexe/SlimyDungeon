using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] topDoorRooms;
    public GameObject[] rightDoorRooms;
    public GameObject[] downDoorRooms;
    public GameObject[] leftDoorRooms;

    public GameObject closedRoom;
    public GameObject fourWayRoom;
    public GameObject roomDestroyer;

    public GameObject closingTopRoom;
    public GameObject closingBottomRoom;
    public GameObject closingLeftRoom;
    public GameObject closingRightRoom;

    public float totalWaitTime;
    private float waitTime;
    private bool spawnedBoss;
    private bool populatedRooms;
    public GameObject bossMarker;

    public List<GameObject> potentialBossRooms;

    public List<GameObject> tempRoomList;


    // Start is called before the first frame update
    void Start()
    {
        waitTime = totalWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(waitTime <= 0)
        {
            if(spawnedBoss == false)
            {
                potentialBossRooms = new List<GameObject>();

                foreach (GameObject room in GameManager._instance.rooms)
                {
                    if (room.GetComponent<RoomAdder>().bossRoomType != BossRoomType.NONE)
                    {
                        potentialBossRooms.Add(room);
                    }
                }

                int randomBossRoom = Random.Range(0, potentialBossRooms.Count);

                GameObject boss = GameManager._instance.BossPrefabs[Random.Range(0, GameManager._instance.BossPrefabs.Length)];

                GameObject tBoss = Instantiate(boss, potentialBossRooms[randomBossRoom].transform.position, Quaternion.identity);
                tBoss.GetComponent<EnemyAI>().health = boss.GetComponent<EnemyAI>().health + (GameManager._instance.currentFloor) * 0.5f;
                tBoss.GetComponent<EnemyAI>().damage = boss.GetComponent<EnemyAI>().damage + (Mathf.RoundToInt((GameManager._instance.currentFloor) * 0.5f));
                Instantiate(bossMarker, potentialBossRooms[randomBossRoom].transform.position, Quaternion.identity);

                GameManager._instance.bossRoom = potentialBossRooms[randomBossRoom];
                tBoss.GetComponent<EnemyAI>().room = potentialBossRooms[randomBossRoom];

                potentialBossRooms[randomBossRoom].name += "(B)";
                potentialBossRooms[randomBossRoom].GetComponent<RoomAdder>().hasEnemies = true;
                potentialBossRooms[randomBossRoom].GetComponent<RoomAdder>().hadEnemies = true;
                potentialBossRooms[randomBossRoom].GetComponent<RoomAdder>().enemies.Add(tBoss);

                spawnedBoss = true;

            }

            if (populatedRooms == false)
            {
                tempRoomList = GameManager._instance.rooms;
                GameManager._instance.spawnRoom = tempRoomList[0];

                tempRoomList.RemoveAt(0);
                tempRoomList.Remove(GameManager._instance.bossRoom);

                int mobCount;
                GameObject[] enemyPrefabs = GameManager._instance.EnemyPrefabs;

                foreach (GameObject room in tempRoomList)
                {
                    mobCount = Random.Range(0, GameManager._instance.currentFloor + 1);

                    for (int i = 0; i < mobCount; i++)
                    {
                        GameObject tempEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], room.transform.position, Quaternion.identity);
                        tempEnemy.GetComponent<EnemyAI>().room = room;
                        tempEnemy.GetComponent<EnemyAI>().health = tempEnemy.GetComponent<EnemyAI>().health + (GameManager._instance.currentFloor * 0.5f);
                        tempEnemy.GetComponent<EnemyAI>().damage = tempEnemy.GetComponent<EnemyAI>().damage + (Mathf.RoundToInt(GameManager._instance.currentFloor * 0.5f));
                        room.GetComponent<RoomAdder>().enemies.Add(tempEnemy);
                        room.GetComponent<RoomAdder>().hasEnemies = true;
                        room.GetComponent<RoomAdder>().hadEnemies = true;
                    }
                }
                populatedRooms = true;
                GameManager._instance.OnDungeonGenerated();
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    public void ResetWaitTime()
    {
        waitTime = totalWaitTime;
    }
}
