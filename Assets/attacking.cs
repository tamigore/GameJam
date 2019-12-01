using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacking : MonoBehaviour
{
    private moving moving_script;
    private Vector2 Direction;

    private float attackTimer = 0;
    [SerializeField] float startAttackTimer = 0.3f;
    public float attackRange = 2f;
    private int player;
    
    void Start()
    {
        moving_script = GetComponent<moving>();
        player = GetComponent<PlayerHandler>().PlayerNumber;
    }

    void FixedUpdate()
    {
        Direction = moving_script.direction;
        if (Input.GetButtonDown("AttackP" + player))
        {
            if (attackTimer <= 0)
            {
                Attack();
                attackTimer = startAttackTimer;
            }
        }
        if (attackTimer > 0)
        {
            attackTimer -= Time.fixedDeltaTime;
        }
    }

    void Attack()
    {
        Collider2D[] ennemiesToDamage = Physics2D.OverlapCircleAll(GetComponent<Transform>().position, attackRange);
        int i = 0;
        foreach(Collider2D collider in ennemiesToDamage)
        {
            if (collider.gameObject.tag == "Ennemy")
                Debug.Log("Hit an ennemy ! " + collider.gameObject.name); // do damage
            else if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<PlayerHandler>().PlayerNumber != player)
            {
                Debug.Log("Hit a player ! " + collider.gameObject.name); // do damage
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Direction * 1000, ForceMode2D.Impulse);
            }
            i++;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetComponent<Transform>().position + new Vector3(Direction.x * 0.5f, Direction.y * 0.5f, 0), attackRange);
    }
}
