using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {
    
    [SerializeField] float speed = 4.0f;
    private bool isDead = false;

    private Animator            animator;
    
    
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	void Update () {

        // -- Handle Animations --
        
    }

    public void TakeDamage()
    {
        animator.SetTrigger("Hurt");
    }
}
