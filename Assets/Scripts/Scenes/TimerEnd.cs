using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEnd : MonoBehaviour
{
    public Timer T;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Body")
        {
            T.StopTimer();
        }
    }
}
