using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
void Update ()
{
    transform.Rotate (0,1,0*Time.deltaTime); //rotates 50 degrees per second around z axis
}
}