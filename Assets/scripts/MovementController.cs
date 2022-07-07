using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovementController : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    public Vector2 lastMotionVector = Vector2.zero;

    private Rigidbody2D m_body2d;
    private Animator animator;
    private Character character;
    private PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        m_body2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            // Move
            m_body2d.velocity = new Vector2(lastMotionVector.x * m_speed, lastMotionVector.y * m_speed);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (view.IsMine)
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
            if (lastMotionVector.x != 0 || lastMotionVector.y != 0)
                animator.SetInteger("AnimState", 2);
            else
                animator.SetInteger("AnimState", 0);
        }       
    }
}
