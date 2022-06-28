using UnityEngine;
using UnityEngine.InputSystem;

public class ToolCharacterController : MonoBehaviour
{
    private MovementController movementController;
    private Rigidbody2D rgbd2;
    private Character character;
    private Animator animator;


    [SerializeField] long baseMinDamagePerHit = 10;
    [SerializeField] long baseMaxDamagePerHit = 20;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;

    private bool attacking = false;

    private void Awake() 
    {
        character = GetComponent<Character>();
        movementController = GetComponent<MovementController>();
        rgbd2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        attacking = context.action.triggered;
        if (attacking)
        {
            animator.SetTrigger("Attack");
            UseTool();
        }
    }

    private void Update()
    {
       
    }

    private void UseTool()
    {
        Vector2 position = rgbd2.position + movementController.lastMotionVector * offsetDistance;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                hit.Hit();
                break;
            }
            Character target = c.GetComponent<Character>();
            if (target != null && target != character)
            {
                var damage = (long)Random.Range(baseMinDamagePerHit, baseMaxDamagePerHit);
                target.TakeDamage(damage);
                break;
            }
        }



    }

}
