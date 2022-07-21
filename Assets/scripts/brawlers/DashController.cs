using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashController : MonoBehaviour
{
    private PhotonView view;
    private Character character;
    private MovementController movementController;
    private float lastDashTime = 0;
    private bool dashing;
    
    private Vector2 lastMotionVector;
    private float dashDistanceRemaining;
    
    [SerializeField] private Transform dashEffect;
    
    [SerializeField] private float dashCooldownDuration;
    [SerializeField] private float dashEffectWidth;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        character = GetComponent<Character>();
        movementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dashDistanceRemaining > 0)
        {
            var dashPerFrame = Time.fixedDeltaTime * dashDistance / dashDuration ;
            dashDistanceRemaining -= dashPerFrame;
            var dashVector = new Vector3(lastMotionVector.x, lastMotionVector.y, 0).normalized * dashPerFrame;
            transform.position += dashVector;            
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
                if (Time.time - lastDashTime > dashCooldownDuration)
                {
                    lastDashTime = Time.time;
                    lastMotionVector = movementController.facing;
                    GameManager.instance.dashCooldownIcon.StartCooldown(dashCooldownDuration);

                    // show dash animation
                    var beforeDashPosition = transform.position + ((Vector3)lastMotionVector * (dashEffectWidth/2));
                    var x_scale = dashDistance / dashEffectWidth;
                    
                    var dash_object = PhotonNetwork.Instantiate(dashEffect.name, beforeDashPosition, Quaternion.identity);
                    var dash_transform = dash_object.GetComponent<Transform>();
                    dash_transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(lastMotionVector));
                    dash_transform.localScale = new Vector3(x_scale, 1.5f, 1f);
                                        
                    //perform dash
                    // alternative 1: gradual multi frame dash action
                    //dashDistanceRemaining= dashDistance;
                    
                    // alternative 2: single dash action
                    Dash(movementController.facing);
                }
            }
        }
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private void Dash(Vector2 lastMotionVector)
    {
        var dashVector = new Vector3(lastMotionVector.x, lastMotionVector.y, 0).normalized * dashSpeed;
        transform.position += dashVector;
    }
}
