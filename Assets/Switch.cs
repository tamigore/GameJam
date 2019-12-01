using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private GameObject[] blockers;
    private bool activated = false;
    private int activated_by = 0;

    [SerializeField] Sprite state_unpushed;
    [SerializeField] Sprite state_pushed;
    // Start is called before the first frame update
    void Start()
    {
        blockers = GameObject.FindGameObjectsWithTag("Blocker");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        activated_by += 1;
        if (activated == false)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = state_pushed;
            foreach (GameObject blocker in blockers)
            {
                blocker.SetActive(blocker.active == true ? false : true);
            }
            activated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        activated_by -= 1;
        if (activated_by <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = state_unpushed;
            activated = false;
            foreach (GameObject blocker in blockers)
            {
                blocker.SetActive(blocker.active == true ? false : true);
            }
            activated_by = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
