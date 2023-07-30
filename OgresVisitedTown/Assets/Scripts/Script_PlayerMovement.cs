using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private Rigidbody2D rb;
    private Vector2 movementDir;
    private Animator animator;
    private bool isMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isMoving = false;
    }

    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            movementDir.x = Input.GetAxisRaw("Horizontal");
            movementDir.y = Input.GetAxisRaw("Vertical");
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        animator.SetFloat("Horizontal", movementDir.x);
        animator.SetFloat("Vertical", movementDir.y);
        animator.SetBool("IsMoving", isMoving);
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            rb.MovePosition(rb.position + movementDir * speed * Time.fixedDeltaTime);
        }
    }
}
