using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStart : MonoBehaviour
{
    public Timer T;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Body")
        {
            T.StartTimer();
        }
    }
}
