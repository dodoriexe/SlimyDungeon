using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject currentRoom;

    bool firstRoomInit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager._instance.cameraObject == null)
        {
            GameManager._instance.cameraObject = gameObject;
        }

        if(!firstRoomInit)
        {
            currentRoom = GameManager._instance.rooms[0];
            ChangeRoom();
            firstRoomInit = true;
            //GameManager._instance.playerObject.GetComponent<PlayerController>().UnFreeze();
        }

        if(Vector2.Distance(transform.position, currentRoom.transform.position) > 0.3f)
        {
            GameManager._instance.playerObject.GetComponent<PlayerController>().Freeze();
        }
        if(GameManager._instance.dungeonGenerated && Vector2.Distance(transform.position, currentRoom.transform.position) < 1f)
        {
            GameManager._instance.playerObject.GetComponent<PlayerController>().UnFreeze();
        }

        if(Vector2.Distance(transform.position, currentRoom.transform.position) > .1f)
        {
            ChangeRoom();
        }

        /*
         *         else
        {
            if(GameManager._instance.playerObject.GetComponent<PlayerController>().frozen)
            {
                GameManager._instance.playerObject.GetComponent<PlayerController>().UnFreeze();
            }
        }
        */
    }

    public void ChangeRoom()
    {
        transform.position = Vector3.Lerp(transform.position, currentRoom.transform.position, .1f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
