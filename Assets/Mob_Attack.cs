using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Attack : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    private Vector2 Direction;
    public float bulletForce = 5f;
    private float AttackTimer = 0;
    [SerializeField] float startAttackTimer = 0.8f;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack_Players()
    {
        Vector3 tmp = GetComponent<Mob_ScanPlayers>().playerspos[GetComponent<Mob_ScanPlayers>().closestPlayer] - transform.position;
        Direction.x = tmp.x;
        Direction.y = tmp.y;
        Direction.Normalize();
        float Angle = Vector2.Angle(firePoint.up, Direction);
        firePoint.Rotate(new Vector3(0, 0, Angle));
        if (AttackTimer <= 0)
        {
            Shoot();
            AttackTimer = startAttackTimer;
        }
        if (AttackTimer > 0)
        {
            AttackTimer -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet_script = bullet.GetComponent<Bullet>();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        bullet_script.IgnoreCollisionWith(GetComponent<Collider2D>());
        bullet_script.SetOwnerLayer(gameObject.tag);
        rb.AddForce(Direction * bulletForce, ForceMode2D.Impulse);
    }
}
