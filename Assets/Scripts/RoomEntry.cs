using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(GameManager._instance.playerObject.GetComponent<PlayerController>().currentRoom != transform.parent.gameObject)
            {
                GameManager._instance.playerObject.transform.position += (GetComponentInParent<RoomAdder>().transform.position - GameManager._instance.playerObject.transform.position) * 0.3f ;
            }

            GameManager._instance.playerObject.GetComponent <PlayerController>().currentRoom = transform.parent.gameObject;
            GameManager._instance.cameraObject.GetComponent<CameraController>().currentRoom = transform.parent.gameObject;
            GameManager._instance.cameraObject.GetComponent<CameraController>().ChangeRoom();
        }
    }
}
