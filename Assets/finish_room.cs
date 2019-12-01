using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finish_room : MonoBehaviour
{
    private GameObject[] blockers;
    [SerializeField] GameObject roomHandler;
    [SerializeField] int RoomNumber;

    void Start()
    {
        blockers = GameObject.FindGameObjectsWithTag("Blocker");
    }
    private void OnTriggerEnter2D()
    {
        foreach (GameObject blocker in blockers)
        {
            blocker.SetActive(false);
        }
        roomHandler.GetComponent<RoomHandler>().FinishRoom(RoomNumber);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
