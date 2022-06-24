using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCharacterController : MonoBehaviour
{
    Bandit bandit;
    Rigidbody2D rgbd2;
    Character character; 
    [SerializeField] long damagePerHit = 15;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;


    private void Awake() 
    {
        character = GetComponent<Character>();
        bandit = GetComponent<Bandit>();
        rgbd2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            UseTool();
        }
    }

    private void UseTool()
    {
        Vector2 position = rgbd2.position + bandit.lastMotionVector * offsetDistance;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        print("attack!!!");
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
                target.Damage(damagePerHit);
                break;
            }
        }



    }

}
