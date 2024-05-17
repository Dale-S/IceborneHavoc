using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
