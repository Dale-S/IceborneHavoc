using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public float currSpeed;
    
    [Header("Ground Check")] 
    public float playerHeight;
    public Transform orientation;
    public LayerMask ground;
    public bool grounded;
    public ParticleSystem slideEffect;
    public AudioSource slidingSound;
    private bool sliding = false;
    private bool inAir = false;
    public AudioSource airEffect;
    public AudioSource landingEffect;

    [Header("UI References")] 
    public TextMeshProUGUI speed;
    public Camera cam;
    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;
    public MovementState state;
    public Animator hammerAnim;
    public PlayerAttack PA;

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

        if (grounded && flatVel.magnitude > 5)
        {
            if (sliding == false)
            {
                slideEffect.Play();
                slidingSound.Play();
                sliding = true;
            }
        }
        else
        {
            slideEffect.Stop();
            slidingSound.Stop();
            sliding = false;
        }

        if (!grounded && flatVel.magnitude >= 6)
        {
            if (inAir == false)
            {
                airEffect.Play();
                inAir = true;
            }
        }
        else
        {
            if (inAir == true)
            {
                landingEffect.Play();
                airEffect.Stop();
                inAir = false;
                if (!PA.inAttack)
                {
                    hammerAnim.Play("fall");
                    Invoke(nameof(backToIdle), 0.42f);
                }
            }
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
        currSpeed = flatVel.magnitude;
        
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

    private void backToIdle()
    {
        hammerAnim.Play("Idle");
    }
}
