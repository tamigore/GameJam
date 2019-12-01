using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    public float    moveSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 movement;
    public Vector2 direction;

    bool isMoving = false;
    private int playerNumber;
    public bool CanMove = true;
    [SerializeField] Transform firePoint;
    public Component[] animators;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerNumber = GetComponent<PlayerHandler>().PlayerNumber;
        Debug.Log(playerNumber);
        direction.x = 0;
        direction.y = 0;
        animators = GetComponents<Animator>();
    }

    void Update()
    {
        foreach (Animator animator in animators)
        {
            if (CanMove == true)
            {
                movement.x = Input.GetAxisRaw("HorizontalP" + playerNumber);
                movement.y = Input.GetAxisRaw("VerticalP" + playerNumber);
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("speed", movement.sqrMagnitude);
                if (movement.x == 1)
                {
                    animator.SetBool("right", true);
                    animator.SetBool("up", false);
                    animator.SetBool("down", false);
                    animator.SetBool("left", false);
                }
                if (movement.x == -1)
                {
                    animator.SetBool("left", true);
                    animator.SetBool("right", false);
                    animator.SetBool("up", false);
                    animator.SetBool("down", false);
                }
                if (movement.y == 1)
                {
                    animator.SetBool("up", true);
                    animator.SetBool("right", false);
                    animator.SetBool("down", false);
                    animator.SetBool("left", false);
                }
                if (movement.y == -1)
                {
                    animator.SetBool("down", true);
                    animator.SetBool("right", false);
                    animator.SetBool("up", false);
                    animator.SetBool("left", false);
                }
                if (movement.x != 0 || movement.y != 0)
                    isMoving = true;
                else
                    isMoving = false;
                if (isMoving == true)
                {
                    movement.Normalize();
                    direction.x = movement.x;
                    direction.y = movement.y;
                    float Angle = Vector2.Angle(firePoint.up, direction);
                    //Debug.Log(Angle);
                    firePoint.Rotate(new Vector3(0, 0, Angle));
                }
            }
            else
            {
                movement.x = 0f;
                movement.y = 0f;
                direction.x = 0f;
                direction.y = 0f;
            }
            if (CanMove == true)
            {
                movement.x = Input.GetAxisRaw("HorizontalP" + playerNumber);
                movement.y = Input.GetAxisRaw("VerticalP" + playerNumber);
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("speed", movement.sqrMagnitude);
                if (movement.x == 1)
                {
                    animator.SetBool("right", true);
                    animator.SetBool("up", false);
                    animator.SetBool("down", false);
                    animator.SetBool("left", false);
                }
                if (movement.x == -1)
                {
                    animator.SetBool("left", true);
                    animator.SetBool("right", false);
                    animator.SetBool("up", false);
                    animator.SetBool("down", false);
                }
                if (movement.y == 1)
                {
                    animator.SetBool("up", true);
                    animator.SetBool("right", false);
                    animator.SetBool("down", false);
                    animator.SetBool("left", false);
                }
                if (movement.y == -1)
                {
                    animator.SetBool("down", true);
                    animator.SetBool("right", false);
                    animator.SetBool("up", false);
                    animator.SetBool("left", false);
                }
                if (movement.x != 0 || movement.y != 0)
                    isMoving = true;
                else
                    isMoving = false;
                if (isMoving == true)
                {
                    movement.Normalize();
                    direction.x = movement.x;
                    direction.y = movement.y;
                    float Angle = Vector2.Angle(firePoint.up, direction);
                    //Debug.Log(Angle);
                    firePoint.Rotate(new Vector3(0, 0, Angle));
                }
            }
            else
            {
                movement.x = 0f;
                movement.y = 0f;
                direction.x = 0f;
                direction.y = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
