using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rgdb2;
    [SerializeField] float speed = 2;
    Vector2 motionVector;
    public Vector2 lastMotionVector;
    Animator animator;
    bool moving = false;

    void Awake()
    {
        rgdb2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
    }


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        motionVector = new Vector2(
            horizontal,
            vertical
            );
        
        moving = horizontal != 0 || vertical != 0;
        if (moving)
        {
            lastMotionVector = new Vector2(
                horizontal,
                vertical
                ).normalized;

            animator.SetFloat("LastHorizontal", horizontal);
            animator.SetFloat("LastVertical", vertical);
        }

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("Moving", moving);
        
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rgdb2.velocity = motionVector * speed;
    }
}
