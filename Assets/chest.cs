using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public bool IsOpen = false;
    [SerializeField] Sprite OpenSprite;
    [SerializeField] Sprite CloseSprite;

    public void Open()
    {
        IsOpen = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = OpenSprite;
    }
    public void Close()
    {
        IsOpen = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = CloseSprite;
    }
}
