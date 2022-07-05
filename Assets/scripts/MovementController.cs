using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovementController : NetworkBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    public Vector2 lastMotionVector = Vector2.zero;

    private Rigidbody2D m_body2d;
    private Animator animator;
    private Character character;

    // Start is called before the first frame update
    void Awake()
    {
        m_body2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
    }

    public override void FixedUpdateNetwork()
    {
        if (character.is_dead)
            return;

        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            lastMotionVector = new Vector2(data.direction.x, data.direction.y);

            // Swap direction of sprite depending on walk direction
            if (lastMotionVector.x > 0)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (lastMotionVector.x < 0)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            // Move
            m_body2d.velocity = new Vector2(lastMotionVector.x * m_speed, lastMotionVector.y * m_speed);

            //Animate
            if (lastMotionVector.x != 0 || lastMotionVector.y != 0)
                animator.SetInteger("AnimState", 2);
            else
                animator.SetInteger("AnimState", 0);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {        
        //if (character.is_dead)
        //    return;

        //lastMotionVector = context.ReadValue<Vector2>();
        
        //// Swap direction of sprite depending on walk direction
        //if (lastMotionVector.x > 0)
        //    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        //else if (lastMotionVector.x < 0)
        //    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        ////Run
        //if (lastMotionVector.x != 0 || lastMotionVector.y != 0)
        //    animator.SetInteger("AnimState", 2);
        //else
        //    animator.SetInteger("AnimState", 0);
       
    }
}
