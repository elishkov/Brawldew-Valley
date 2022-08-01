using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashController : MonoBehaviour
{
    // dependencies
    private PhotonView view;
    private Character character;
    private Rigidbody2D rigidbody2d;
    private TrailRenderer trailRenderer;    
    private MovementController movementController;

    
    // inputs
    [SerializeField] private float dashCooldownDuration;
    [SerializeField] private float dashVelocity;
    [SerializeField] private float dashDuration;
    [SerializeField] private bool dynamicDashDirectionUpdate = true;

    // collisions
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;

    // dash controls
    private Vector2 dashDirection;
    private bool isDashing = false;
    private bool canDash = true;
    private bool dashing;

    void Start()
    {
        view = GetComponent<PhotonView>();
        character = GetComponent<Character>();
        movementController = GetComponent<MovementController>();
        trailRenderer = GetComponent<TrailRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dynamicDashDirectionUpdate)
        {
            dashDirection = movementController.facing;
        }

        // Check for potential collisions
        int count = rigidbody2d.Cast(
            dashDirection, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            dashVelocity * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset
        
        if (count == 0 && isDashing)
        {
            rigidbody2d.MovePosition(rigidbody2d.position + dashVelocity * Time.fixedDeltaTime * dashDirection);
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

            if (dashing && canDash)
            {
                isDashing = true;
                canDash = false;
                //trailRenderer.emitting = true;
                view.RPC("UpdateTrailEmissions", RpcTarget.AllBuffered, true);
                dashDirection = movementController.facing;

                StartCoroutine(StopDashing());                
            }
        }
    }

    [PunRPC]
    public void UpdateTrailEmissions(bool emitting)
    {
        trailRenderer.emitting = emitting;
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashDuration);
        //trailRenderer.emitting = false;
        view.RPC("UpdateTrailEmissions", RpcTarget.AllBuffered, false);
        isDashing = false;        
        GameManager.instance.dashCooldownIcon.StartCooldownAnimation(dashCooldownDuration);        
        yield return new WaitForSeconds(dashCooldownDuration);
        canDash = true;
    }
}
