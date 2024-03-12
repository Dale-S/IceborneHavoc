using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform kunaiStart;
    public LineRenderer lr;
    public PlayerMovement pm;
    public Transform player;
    private SpringJoint joint;
    private Vector3 currentGrapplePosition;

    [Header("Swinging Settings")] 
    public float maxSwingDistance = 30f;
    private Vector3 swingPoint;

    [Header("Cooldown")] 
    public float swingingCD;
    private float swingingCDTimer;

    [Header("Mapping")] 
    public KeyCode swingKey = KeyCode.Mouse1;

    private bool swinging;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(swingKey))
        {
            StartSwinging();
        }
        if(Input.GetKeyUp(swingKey))
        {
            StopSwing();
        }
    }
    
    private void LateUpdate()
    {
        DrawRope();
    }

    private void StartSwinging()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance))
        {
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 2.5f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = kunaiStart.position;
        }

        swinging = true;
    }

    private void StopSwing()
    {
        Debug.Log("Swing Stop");
        lr.positionCount = 0;
        Destroy(joint);
    }

    private void DrawRope()
    {
        if (!joint) return;
        
        lr.SetPosition(0, kunaiStart.position);
        lr.SetPosition(1, swingPoint);
    }
}
