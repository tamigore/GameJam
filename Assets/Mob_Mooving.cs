using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Mooving : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 movement;
    public float moveSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk()
    {
        Vector3 direction = GetComponent<Mob_ScanPlayers>().playerspos[GetComponent<Mob_ScanPlayers>().closestPlayer] - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
    }

    public void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
    }
}
