using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    public bool isStartRoom;
    public NeededDoorType neededDoor;
    public RoomTemplates roomTemplates;

    bool alreadySpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        roomTemplates = GameObject.Find("RoomTemplates").GetComponent<RoomTemplates>();
        Invoke("Spawn", 1f);
    }

    void Spawn()
    {
        roomTemplates.ResetWaitTime();
        if(isStartRoom)
        {
            GameObject startingRoomy = Instantiate(roomTemplates.fourWayRoom, transform.position, Quaternion.identity);
            startingRoomy.GetComponent<RoomAdder>().isFirstRoom = true;

            Instantiate(roomTemplates.roomDestroyer, startingRoomy.transform);
            alreadySpawned = true;
            return;
        }

        if(!alreadySpawned)
        {
            int roomToSpawn;
            switch (neededDoor)
            {
                case NeededDoorType.TOP:
                    if(GameManager._instance.maxOpenRooms <= 0)
                    {
                        GameObject.Instantiate(roomTemplates.closingTopRoom, transform.position, Quaternion.identity);
                        alreadySpawned = true;
                        break;
                    }

                    roomToSpawn = Random.Range(0, roomTemplates.topDoorRooms.Length);
                    GameObject.Instantiate(roomTemplates.topDoorRooms[roomToSpawn], transform.position, Quaternion.identity);
                    alreadySpawned = true;
                    break;
                case NeededDoorType.BOTTOM:
                    if (GameManager._instance.maxOpenRooms <= 0)
                    {
                        GameObject.Instantiate(roomTemplates.closingBottomRoom, transform.position, Quaternion.identity);
                        alreadySpawned = true;
                        break;
                    }

                    roomToSpawn = Random.Range(0, roomTemplates.downDoorRooms.Length);
                    GameObject.Instantiate(roomTemplates.downDoorRooms[roomToSpawn], transform.position, Quaternion.identity);
                    alreadySpawned = true;
                    break;
                case NeededDoorType.LEFT:
                    if (GameManager._instance.maxOpenRooms <= 0)
                    {
                        GameObject.Instantiate(roomTemplates.closingLeftRoom, transform.position, Quaternion.identity);
                        alreadySpawned = true;
                        break;
                    }

                    roomToSpawn = Random.Range(0, roomTemplates.leftDoorRooms.Length);
                    GameObject.Instantiate(roomTemplates.leftDoorRooms[roomToSpawn], transform.position, Quaternion.identity);
                    alreadySpawned = true;
                    break;
                case NeededDoorType.RIGHT:
                    if (GameManager._instance.maxOpenRooms <= 0)
                    {
                        GameObject.Instantiate(roomTemplates.closingRightRoom, transform.position, Quaternion.identity);
                        alreadySpawned = true;
                        break;
                    }

                    roomToSpawn = Random.Range(0, roomTemplates.rightDoorRooms.Length);
                    GameObject.Instantiate(roomTemplates.rightDoorRooms[roomToSpawn], transform.position, Quaternion.identity);
                    alreadySpawned = true;
                    break;
            }

            if(GameManager._instance.maxOpenRooms > 0)
            {
                GameManager._instance.maxOpenRooms--;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RoomSpawn"))
        {
            if(Vector2.Distance(this.transform.position, GameManager._instance.firstRoom.transform.position) < 0.1f)
            {
                Debug.Log("It worked!");
                alreadySpawned = true;
                return;
            }

            if(other.GetComponent<RoomSpawn>() && other.GetComponent<RoomSpawn>().alreadySpawned == false && alreadySpawned == false)
            {
                Debug.Log(transform.parent.name + "  " + other.transform.parent.name);
                Instantiate(roomTemplates.closedRoom, transform.position, Quaternion.identity);
            }
            alreadySpawned = true;
        }
    }
}

public enum NeededDoorType
{
    TOP,
    BOTTOM,
    LEFT,
    RIGHT
}
