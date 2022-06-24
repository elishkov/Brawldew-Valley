using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCharacterController : MonoBehaviour
{
    Bandit character;
    Rigidbody2D rgbd2;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;


    private void Awake() 
    {
        character = GetComponent<Bandit>();
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
        Vector2 position = rgbd2.position + character.lastMotionVector * offsetDistance;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                hit.Hit();
                break;
            }
        }



    }

}
