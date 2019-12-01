using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendshipIsMagic : MonoBehaviour
{
    private GameObject[] Players;

    private void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void OnTriggerEnter2D()
    {
        foreach(GameObject player in Players)
        {
            player.GetComponent<PlayerHandler>().PVPOn = true;
        }
    }
}
