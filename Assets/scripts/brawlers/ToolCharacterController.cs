using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolCharacterController : MonoBehaviour
{
    private MovementController movementController;
    private Rigidbody2D rgbd2;
    private Character character;
    private Animator animator;
    private PhotonView view;

    
    [SerializeField] long baseMinDamagePerHit = 10;
    [SerializeField] long baseMaxDamagePerHit = 20;
    [SerializeField] float critChance = 0.03f;
    [SerializeField] float baseCritMultiplier = 2;
    [SerializeField] Vector2 toolOffsetVector= new(1f,1f);
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeField] LayerMask hurtLayers;

    private bool attacking = false;    

    private void Start()
    {
        character = GetComponent<Character>();
        movementController = GetComponent<MovementController>();
        rgbd2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
    }

    private void Awake() 
    {
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (view.IsMine)
        {
            if (character.is_dead)
            {
                return;
            }

            attacking = context.action.triggered;
            if (attacking)
            {
                animator.SetTrigger("Attack");
                UseTool();
            }
        }
    }

    private void Update()
    {
    }

    private void UseTool()
    {
        Vector2 position = rgbd2.position + toolOffsetVector + getSpriteDirectionOffset();

        //Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea, toolLayers);
        //foreach (Collider2D c in colliders)
        //{
        //    ToolHit hit = c.GetComponent<ToolHit>();
        //    if (hit != null)
        //    {
        //        hit.Hit();
        //        break;
        //    }
        //}


        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea, hurtLayers);
        foreach (Collider2D c in colliders)
        {
            if (c.TryGetComponent<Damagable>(out Damagable target))
            {
                ToolHit hit = target.toolTarget;
                if (hit != null)
                {
                    hit.Hit();
                    break;
                }

                Character targetCharacter = target.Character;
                if (targetCharacter != null && targetCharacter != character && !targetCharacter.is_dead)
                {
                    CalculateDamage(out bool crit_happened, out int actual_damage);
                    targetCharacter.ApplyDamage(actual_damage, crit_happened);
                    break;
                }
            }
            
        }
    }

    private void CalculateDamage(out bool crit_happened, out int actual_damage)
    {
        float actualCritMultiplier = 1;
        var actual_base_damage = (long)Random.Range(baseMinDamagePerHit, baseMaxDamagePerHit);
        crit_happened = Random.Range(0f, 1f) < critChance;
        if (crit_happened)
        {
            actualCritMultiplier = baseCritMultiplier;
        }
        actual_damage = (int)(actual_base_damage * actualCritMultiplier);
    }

    void OnDrawGizmosSelected()
    {
        Start();
        Vector2 position = rgbd2.position + toolOffsetVector + getSpriteDirectionOffset();
        Gizmos.DrawWireSphere(position, sizeOfInteractableArea);
    }

    private Vector2 getSpriteDirectionOffset()
    {
        // default sprite position is left facing
        // when facing right a slight offset is required
        return movementController.FacingLeft ? new(0, 0) : new(0.5f, 0);
    }
}
