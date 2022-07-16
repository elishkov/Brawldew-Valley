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
    private Rigidbody2D rigidbody2d;
    private MovementController movementController;
    private float lastDashTime = 0;
    private bool dashing;
    private float dashCooldown = 2f;
    private Vector2 lastMotionVector;
    private int dash_stage;

    public float dash_speed;
    public float staged_dash_speed;
    public int dash_frame_count;
    public CooldownIcon cooldownIcon;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        character = GetComponent<Character>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        movementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dash_stage > 0)
        {
            dash_stage -= 1;
            var jump = new Vector3(lastMotionVector.x, lastMotionVector.y, 0).normalized * staged_dash_speed * Time.deltaTime;
            print(jump);
            transform.position += jump;
        }
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
                if (Time.time - lastDashTime > dashCooldown)
                {
                    cooldownIcon.StartCooldown(dashCooldown);
                    lastDashTime = Time.time;
                    lastMotionVector = movementController.facing;
                    //animator.SetTrigger("Dash");
                    //Dash(movementController.lastMotionVector);
                    dash_stage = dash_frame_count;
                }
            }
        }
    }

    private void Dash(Vector2 lastMotionVector)
    {
        var jump = new Vector3(lastMotionVector.x, lastMotionVector.y, 0).normalized * dash_speed * Time.deltaTime;
        transform.position += jump;
    }
}
