using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportScript : MonoBehaviour
{
    [SerializeField] GameObject P1Teleport;
    [SerializeField] GameObject P2Teleport;
    [SerializeField] bool isLastRoom = false;
    private GameObject[] players;
    private bool mustMove = false;
    [SerializeField] GameObject dest;
    private GameObject winningPlayer;
    [SerializeField] GameObject finalchest;
    private float waitBeforeTurnTime = 0.2f;
    private bool mustWait = false;
    private bool mustDestroy = false;
    private bool mustRead = false;
    private float readingTime = 2f;
    [SerializeField] GameObject message;
    [SerializeField] GameObject friendShipMessage;
    [SerializeField] GameObject tear;
    private bool friendShipDisplay = false;
    private float cryTime = 2f;
    private float friendShipTime = 10f;
    private bool isPlaying = false;
    [SerializeField] GameObject p1Reading;
    [SerializeField] GameObject p2Reading;

    private void Start()
    {
        if (message != null)
        {
            message.SetActive(false);
            //friendShipMessage.SetActive(false);
            tear.SetActive(false);
        }
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update()
    {
        if (mustMove == true)
        {
            Vector3 objectif = dest.transform.position - winningPlayer.transform.position;
            if (objectif.magnitude < 0.1)
            {
                mustMove = false;
                winningPlayer.GetComponent<Animator>().SetBool("up", false);
                winningPlayer.GetComponent<Animator>().SetBool("down", false);
                winningPlayer.GetComponent<Animator>().SetBool("right", false);
                winningPlayer.GetComponent<Animator>().SetBool("left", false);
                winningPlayer.GetComponent<Animator>().SetFloat("speed", 0f);
                winningPlayer.GetComponent<Animator>().SetFloat("Vertical", 0f);
                winningPlayer.GetComponent<Animator>().SetFloat("Horizontal", 0f);
                //winningPlayer.GetComponent<Animator>().Rebind();

                finalchest.GetComponent<chest>().Open();
                mustWait = true;
            }
            else
            {
                objectif.Normalize();
                winningPlayer.transform.Translate(objectif * winningPlayer.GetComponent<PlayerHandler>().MoveSpeed * Time.deltaTime);
            }
        }
        if (mustWait == true)
        {
            waitBeforeTurnTime -= Time.deltaTime;
            if (waitBeforeTurnTime < 0)
            {
                Vector3 direction = Vector2.down;
                winningPlayer.GetComponent<Animator>().SetBool("down", true);
                winningPlayer.GetComponent<Animator>().SetFloat("Vertical", -1f);
                winningPlayer.GetComponent<Animator>().SetFloat("Speed", 2f);
                mustDestroy = true;
                mustWait = false;
            }
        }
        else if (mustDestroy == true)
        {
            Destroy(winningPlayer.GetComponent<Animator>());
            mustRead = true;
            message.SetActive(true);
            mustDestroy = false;
            winningPlayer.GetComponent<SpriteRenderer>().sprite = (winningPlayer.GetComponent<PlayerHandler>().PlayerNumber == 1 ? p1Reading.GetComponent<SpriteRenderer>().sprite : p2Reading.GetComponent<SpriteRenderer>().sprite);
        }
        else if (mustRead == true)
        {
            readingTime -= Time.deltaTime;
            if (isPlaying == false)
            {
                GetComponent<AudioSource>().Play();
                isPlaying = true;
            }
            if (readingTime <= 0)
            {
                Destroy(message);
                mustRead = false;
                friendShipDisplay = true;
                //friendShipMessage.SetActive(true);
                tear.SetActive(true);
                // faire apparaitre la larme
            }
        }
        else if (friendShipDisplay == true)
        {
            Vector3 direction = (Vector3)(Vector2.down);
            cryTime -= Time.deltaTime;
            if (cryTime <= 0)
                Destroy(tear);
            else
                tear.transform.Translate(direction * 0.1f * Time.deltaTime);

            friendShipTime -= Time.deltaTime;
            //if (friendShipTime <= 0)
                //Destroy(friendShipMessage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLastRoom == true)
        {
            collision.gameObject.transform.position = P1Teleport.transform.position;
            winningPlayer = collision.gameObject;
            mustMove = true;
            winningPlayer.GetComponent<PlayerHandler>().CanMove = false;
        }
        else
        {
            players[0].transform.position = P1Teleport.transform.position;
            players[1].transform.position = P2Teleport.transform.position;

        }
    }
}
