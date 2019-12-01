using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Life : MonoBehaviour
{
    [SerializeField] int life = 5;
    private GameObject room_handler;
    public void TakeDamage(int value)
    {
        life -= value;
        if (life <= 0)
        {

            room_handler.GetComponent<RoomHandler>().EnnemyKilled();
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        room_handler = GameObject.FindGameObjectWithTag("RoomHandler");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
