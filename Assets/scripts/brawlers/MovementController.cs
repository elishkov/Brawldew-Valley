using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovementController : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    public Vector2 lastMotionVector = Vector2.zero;
    public Vector2 facing = Vector2.zero;
    

    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private Character character;
    private PhotonView view;
    public ContactFilter2D movementFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float collisionOffset = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
        view = GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        if (view is null || view.IsMine)
        {
            // Move


            bool autoCollisions = false;
            if (autoCollisions)
            {
                //rigidbody2d.MovePosition(rigidbody2d.position + lastMotionVector * m_speed * Time.deltaTime);
                //rigidbody2d.velocity = new Vector2(lastMotionVector.x * m_speed, lastMotionVector.y * m_speed);
                rigidbody2d.AddForce(lastMotionVector * 0.1f * Time.deltaTime);
            }
            else
            {
                // Try to move player in input direction, followed by left right and up down input if failed
                bool success = MovePlayer(lastMotionVector);
                if (!success)
                {
                    // Try Left / Right
                    success = MovePlayer(new Vector2(lastMotionVector.x, 0));

                    if (!success)
                    {
                        success = MovePlayer(new Vector2(0, lastMotionVector.y));
                    }
                }
            }
        }
    }

    /// <summary>
    /// this is an attempt to control the collisions manually instead of physics2d
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    // Tries to move the player in a direction by casting in that direction by the amount
    // moved plus an offset. If no collisions are found, it moves the players
    // Returns true or false depending on if a move was executed
    public bool MovePlayer(Vector2 direction)
    {
        // Check for potential collisions
        int count = rigidbody2d.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            m_speed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

        if (count == 0)
        {            
            Vector2 moveVector = direction * m_speed * Time.fixedDeltaTime;

            // No collisions
            rigidbody2d.MovePosition(rigidbody2d.position + moveVector);
            return true;
        }
        else
        {
            //print($"found {castCollisions.Capacity} collisions");

            //// Print collisions
            //foreach (RaycastHit2D hit in castCollisions)
            //{
            //    print($"hit object at transform {hit.transform}");
            //}

            return false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (view is null || view.IsMine)
        {
            if (character.is_dead)
                return;

            lastMotionVector = context.ReadValue<Vector2>();

            // Swap direction of sprite depending on walk direction
            if (lastMotionVector.x > 0)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (lastMotionVector.x < 0)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            //Run
            if (lastMotionVector.x != 0 || lastMotionVector.y != 0) {
                animator.SetInteger("AnimState", 2);
                facing = lastMotionVector;
            }
            else
            {
                animator.SetInteger("AnimState", 0);
            }

            
            
        }       
    }
}
