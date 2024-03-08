using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")] 
    //private PlayerMovementGrappling pmg;
    public Transform cam;
    public Transform kunaiStart;
    public LineRenderer lr;
    public PlayerMovement pm;

    [Header("Grapple Settings")] 
    public float maxGrappleDistance;
    public float grappleDelayTime;

    private Vector3 grapplePoint;

    [Header("Cooldown")] 
    public float grapplingCD;
    private float grapplingCDTimer;

    [Header("Mapping")] 
    public KeyCode grappleKey = KeyCode.Q;

    private bool grappling;
    
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }

        if (grapplingCDTimer > 0)
        {
            grapplingCDTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            lr.SetPosition(0, kunaiStart.position);
        }
    }

    private void StartGrapple()
    {
        if (grapplingCDTimer > 0)
        {
            return;
        }
        
        grappling = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance))
        {
            grapplePoint = hit.point;
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {
        pm.grapple = true;
        pm.pullTowards(grapplePoint);
        Invoke(nameof(StopGrapple), grappleDelayTime);
    }

    private void StopGrapple()
    {
        grappling = false;
        grapplingCDTimer = grapplingCD;
        lr.enabled = false;
        pm.grapple = false;
    }
}
