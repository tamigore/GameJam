using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject bulletPrefab;

    public float bulletForce = 20f;
    public int player;
    private moving moving_script;
    [SerializeField] Vector2 Direction;

    private float attackTimer = 0;
    [SerializeField] float startAttackTimer = 0.2f;
    private PlayerHandler ph;
    void Start()
    {
        moving_script = GetComponent<moving>();
        ph = GetComponent<PlayerHandler>();
        player = ph.PlayerNumber;
    }

    void Update()
    {
        if (ph.IsDead() == false)
        {
            Direction = moving_script.direction;
     //      Vector3 Direction3 = new Vector3(D)
       //     Direction.
            //Direction = (Vector2)(firePoint.transform.position - transform.position);
            if (Input.GetButton("AttackP" + player))
            {
                if (attackTimer <= 0)
                {
                    Shoot();
                    attackTimer = startAttackTimer;
                }
            }
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Bullet bullet_script = bullet.GetComponent<Bullet>();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (GetComponent<PlayerHandler>().PVPOn == false)
            bullet_script.SetOwnerLayer("Player");
        bullet_script.IgnoreCollisionWith(GetComponent<Collider2D>());
        rb.AddForce(Direction * bulletForce, ForceMode2D.Impulse);
    }
}
