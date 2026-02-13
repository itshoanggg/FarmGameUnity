using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovement_Mouse : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float speed = 5f;

    private Vector2 movement;
    private Vector2 targetPosition; 

    private bool isMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);   
            isMoving = true;
        }

        movement = (targetPosition - rb.position).normalized ;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", isMoving ? 1 : 0);
    }

    private void FixedUpdate()
    {
        if(isMoving == true)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            if (Vector2.Distance(rb.position, targetPosition) <= 0.1f)
            {
                isMoving = false;
            }
        }
    }

}
