using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Key Mapping")] 
    public KeyCode jumpKey = KeyCode.Space;
    
    [Header("Movement")] 
    public float maxSpeed;
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool canJump = true;
    public float grappleSpeed;
    public bool grapple;
    public float softcap;

    private Vector3 flatVel;
    
    [Header("Ground Check")] 
    public float playerHeight;
    public Transform orientation;
    public LayerMask ground;
    private bool grounded;

    [Header("UI References")] 
    public TextMeshProUGUI speed;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        walking,
        noMovement,
        air,
        jump,
        grappling
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        
        StateHandler();
        PlayerInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    
    private void StateHandler()
    {
        //Walking state
        if(grounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            state = MovementState.walking;
            if (flatVel.magnitude < 5)
            {
                maxSpeed = 5;
            }
            moveSpeed = 1;
        }
        
        //Sliding across ice state
        if (grounded && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            state = MovementState.noMovement;
            maxSpeed = softcap;
        }
        
        //Air state
        if (!grounded && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            state = MovementState.air;
            maxSpeed = softcap;
        }
        
        //Jumping state
        if (!grounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            state = MovementState.jump;
        }
        
        //Grappling state
        if (grapple)
        {
            state = MovementState.grappling;
            maxSpeed = softcap;
        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        UpdateUI();
    }

    private void SpeedControl()
    {
        flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void UpdateUI()
    {
        speed.text = $"Speed: {Mathf.Ceil(flatVel.magnitude)} / {maxSpeed}";
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
    }

    public void pullTowards(Vector3 grapplePoint)
    {
        rb.velocity = rb.velocity += new Vector3(grapplePoint.x - rb.position.x, 0, grapplePoint.z - rb.position.z) * grappleSpeed;  
    }
}
