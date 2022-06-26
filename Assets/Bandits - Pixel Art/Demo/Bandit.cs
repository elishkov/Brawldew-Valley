using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {
    
    [SerializeField] float speed = 4.0f;
    private bool combatIdle = false;
    private bool isDead = false;

    private MovementController  movementController;
    private Animator            animator;
    
    
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        movementController = GetComponent<MovementController>();
    }
	
	// Update is called once per frame
	void Update () {

        // -- Handle Animations --
        
    }

    public void TakeDamage()
    {
        animator.SetTrigger("Hurt");
    }
}
