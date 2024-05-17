using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEnd : MonoBehaviour
{
    public Timer T;
    public GameObject EndScreen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Body")
        {
            T.StopTimer();
            EndScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
