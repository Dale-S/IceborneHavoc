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
    public GameObject kunai;
    public GameObject hitEffect;
    private GameObject currKunai;
    private GameObject currImpact;
    public AudioSource swingingSound;

    [Header("Swinging Settings")] 
    public float maxSwingDistance = 30f;
    private Vector3 swingPoint;

    [Header("Cooldown")] 
    public float swingingCD;
    private float swingingCDTimer;

    [Header("Mapping")] 
    public KeyCode swingKey = KeyCode.Mouse1;

    private bool swinging;

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

        if (joint != null)
        {
            swingAdjust();
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
            if (hit.transform.gameObject.layer == 8)
            {
                return;
            }
            swingPoint = hit.point;
            swingingSound.Play();
            currKunai = Instantiate(kunai, swingPoint, Quaternion.LookRotation(cam.forward));
            currImpact = Instantiate(hitEffect, swingPoint, Quaternion.identity);
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

    private void swingAdjust()
    {
        //Debug.Log(Input.GetAxisRaw("Mouse ScrollWheel"));
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint) - 4f;
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        } 
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint);
            if (extendedDistanceFromPoint <= maxSwingDistance)
            {
                extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + 4f;
            }
            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }
    }

    private void StopSwing()
    {
        swingingSound.Stop();
        Destroy(currKunai);
        Destroy(currImpact);
        lr.positionCount = 0;
        Destroy(joint);
    }

    private void DrawRope()
    {
        if (!joint) return;
        
        lr.SetPosition(0, kunaiStart.position);
        lr.SetPosition(1, swingPoint - (cam.forward / 4));
    }
}
