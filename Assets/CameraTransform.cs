using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    [SerializeField] GameObject roomHandler;
    [SerializeField] int roomNumber;
    void OnTriggerEnter2D(Collider2D other)
	{
    	if (other.tag == "Player") Camera.main.transform.position = transform.position + new Vector3(0,0,-10);
        RoomHandler roomHandlerScript = roomHandler.GetComponent<RoomHandler>();
        if (roomHandlerScript.isRoomDone[roomNumber] == false)
        {
            roomHandlerScript.CloseDoors();
        }
        else
            roomHandlerScript.FinishRoom(roomNumber);
    }
}
