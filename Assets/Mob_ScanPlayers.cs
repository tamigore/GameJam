using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_ScanPlayers : MonoBehaviour
{
    public GameObject[] players;
    public float closeDistance = 2.5f;
    public Vector3[] playerspos;
    public float[] playerDist;
    [SerializeField] string mode = "walk";
    public float shootingRange = 3f;
    public int closestPlayer = -1;
    private bool EveryoneDead = false;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        playerspos = new Vector3[players.Length];
        playerDist = new float[players.Length];
    }

    // Update is called once per frame
    void Update()
    {
        ScanPlayers();
        SetMode();
        if (mode == "shoot")
        {
            GetComponent<Mob_Attack>().Attack_Players();
        }
        else if (mode == "walk")
        {
            GetComponent<Mob_Mooving>().Walk();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, closeDistance);
    }

    void ScanPlayers()
    {
        int i = 0;
        foreach (GameObject player in players)
        {
            playerspos[i] = player.GetComponent<Transform>().position;
            playerspos[i].z = 0;
            i++;
        }
        Vector3 mypos = GetComponent<Transform>().position;
        mypos.z = 0;
        i = 0;
        foreach (Vector3 pos in playerspos)
        {
            Vector3 dist = pos - mypos;
            playerDist[i] = dist.magnitude;
            i++;
        }
    }

    void SetMode()
    {
        int i = 0;
        bool everyone_is_dead = true;
        float closestDist = Mathf.Infinity;

        foreach (float dist in playerDist)
        {
            if (dist < closestDist && players[i].GetComponent<PlayerHandler>().IsDead() == false)
            {
                everyone_is_dead = false;
                closestDist = dist;
                closestPlayer = i;
            }
            i++;
        }
        EveryoneDead = everyone_is_dead;
        if (closestDist < closeDistance)
        {
            mode = "shoot";
        }
        else if (closestDist > shootingRange)
        {
            mode = "walk";
        }
    }

    void FixedUpdate()
    {
        if (EveryoneDead == false)
        {
            if (mode == "walk")
            {
                GetComponent<Mob_Mooving>().moveCharacter(GetComponent<Mob_Mooving>().movement);
            }
            if (mode == "shoot")
            {
                GetComponent<Mob_Attack>().Attack_Players();
            }
        }
    }
}
