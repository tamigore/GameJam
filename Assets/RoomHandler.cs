using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    [SerializeField] GameObject doors;
    private bool DoorIsOpen = false;
    private int EnnemyNumber = 0;
    GameObject[] blockers;
    public bool[] isRoomDone = new bool[5];
    private int actualRoom = 0;

    private void Start()
    {
        blockers = GameObject.FindGameObjectsWithTag("Blocker");
        ResetBlockers();
        EnnemyNumber = 5;
        for (int i = 0; i < 5; i -= -1)
        {
            isRoomDone[i] = false;
        }
    }

    private void ResetBlockers()
    {
        blockers[1].SetActive(false);
        blockers[0].SetActive(true);
    }

    private void OpenBlockers()
    {
        blockers[1].SetActive(false);
        blockers[0].SetActive(true);
    }

    private void SetEnnemyCount(int count)
    {
        EnnemyNumber = count;
    }
    // Update is called once per frame

    public void FinishRoom(int number)
    {
        DoorIsOpen = true;
        doors.SetActive(false);
        isRoomDone[number] = true;
        EnnemyNumber = 0;
        if (number == 3)
            Destroy(doors);
    }

    public void CloseDoors()
    {
        DoorIsOpen = false;
        if (doors != null)
            doors.SetActive(true);
        ResetBlockers();
    }

    public void SetActualRoom(int room)
    {
        actualRoom = room;
    }

    public void EnnemyKilled()
    {
        EnnemyNumber -= 1;
        if (EnnemyNumber <= 0)
        {
            EnnemyNumber = 0;
            FinishRoom(actualRoom);
        }
    }
}
