using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] int damageValue = 1;
    string tag_owner = "";

    public void IgnoreCollisionWith(Collider2D toIgnore)
    {
        Physics2D.IgnoreCollision(toIgnore, GetComponent<Collider2D>());
    }

    public void SetOwnerLayer(string ownerLayer)
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile Layer"), LayerMask.NameToLayer("Projectile Layer"));
        tag_owner = ownerLayer;
        if (tag_owner == "Ennemy")
        {
            GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Ennemy");
            foreach (GameObject ennemy in ennemies)
            {
                Physics2D.IgnoreCollision(ennemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
        else if (tag_owner == "Player")
        {
            GameObject []players = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject player in players)
            {
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            PlayerHandler phandler = collision.gameObject.GetComponent<PlayerHandler>();
            phandler.TakeDamage(damageValue);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ennemy")
        {
            collision.gameObject.GetComponent<Mob_Life>().TakeDamage(damageValue);
            Destroy(gameObject);
        }
        //if (collision.gameObject.tag == "Ennemy" && tag_owner != "Ennemy")
        	//collision.gameObject.GetComponent<Mob_Life>().TakeDamage(damageValue);
    }
}
