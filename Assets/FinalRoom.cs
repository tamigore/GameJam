using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoom : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] players;
    [SerializeField] int RoomNumber;
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerHandler>().PVPOn = true;
            Destroy(gameObject);
        }
    }
}
