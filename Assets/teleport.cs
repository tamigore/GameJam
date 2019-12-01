using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    [SerializeField] GameObject teleportTo;
    private bool available = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (available == true)
        {
            teleportTo.GetComponent<teleport>().available = false;
            collision.gameObject.transform.position = teleportTo.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (available == false)
            available = true;
    }

}
