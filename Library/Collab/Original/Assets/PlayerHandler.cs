using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using CodeMonkey;
using CodeMonkey.Utils;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController controllerP1;
    [SerializeField] RuntimeAnimatorController controllerP2;
    [SerializeField] GameObject player;
    [SerializeField] GameObject healthSystem;
    [SerializeField] GameObject roomHandler;
    public int hearthNumber = 20;

    public int AttackSpeed = 100;
    public int AttackSpeedBonus = 0;
    public float MoveSpeed = 20f;
    public bool PVPOn = false;
    public bool CanMove = true;
    public int PlayerNumber = 2;

    private moving moving_script;
    private HeartsHealthVisual health_script;
    [SerializeField] int RealHealth;
    private bool isDead = false;

    public bool IsDead()
    {
        return (isDead);
    }

    private void Start()
    {
        moving_script = player.GetComponent<moving>();
        moving_script.moveSpeed = MoveSpeed;
        moving_script.CanMove = CanMove;
        health_script = healthSystem.GetComponent<HeartsHealthVisual>();
        RealHealth = hearthNumber * 4;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = (PlayerNumber == 1 ? controllerP1 : controllerP2);

        //   CMDebug.ButtonUI(new Vector2(50 * moving_script.playerNumber * 5, -200), "Heal 4", () => Heal(4));

    }
    // Update is called once per frame
    void Update()
    {
        moving_script.CanMove = CanMove;
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision !");
    }

    public void TakeDamage(int damage)
    {
        health_script.heartsHealthSystem.Damage(damage);
        RealHealth -= damage;
        if(RealHealth <= 0)
        {
            RealHealth = 0;
            if (PVPOn == true && isDead == false)
                roomHandler.GetComponent<RoomHandler>().FinishRoom(3);
            if (isDead == false)
                Die();
        }
    }

    public void Heal(int health)
    {
        if (RealHealth <= 0)
            Revive();
        RealHealth += health;
        if (RealHealth > hearthNumber * 4)
            RealHealth = hearthNumber * 4;
        health_script.heartsHealthSystem.Heal(health);
    }

    private void Die()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        CanMove = false;
        isDead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log("I DIED !");
    }

    private void Revive()
    {
        CanMove = true;
        isDead = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log("I REVIVED !");
    }
}
