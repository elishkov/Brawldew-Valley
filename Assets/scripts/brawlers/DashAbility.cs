using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbility : MonoBehaviour
{
    private PhotonView view;
    private Character character;
    private Rigidbody2D m_body2d;
    private MovementController movementController;
    private bool is_mid_dash = false;

    public float dash_speed = 150.0f;
    private bool dashing;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        character = GetComponent<Character>();
        m_body2d = GetComponent<Rigidbody2D>();
        movementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (view.IsMine)
        {
            if (character.is_dead)
            {
                return;
            }
            dashing = context.action.triggered;
            if (dashing)
            {
                //animator.SetTrigger("Dash");
                Dash(movementController.lastMotionVector);
            }
        }
    }

    private void Dash(Vector2 lastMotionVector)
    {
        print(lastMotionVector);
        print($"dashing to {lastMotionVector.x},{lastMotionVector.y}");
        print($"new vector{new Vector3(lastMotionVector.x, lastMotionVector.y, 0)}");
        var jump = new Vector3(lastMotionVector.x, lastMotionVector.y, 0).normalized * dash_speed * Time.deltaTime;
        print($"actual dash {jump}");
        transform.position += jump;
    }
}
