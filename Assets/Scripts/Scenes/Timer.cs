using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private bool startTimer = false;
    private float currTime = 0;
    public float HighScore = 0;

    private void Update()
    {
        if (startTimer)
        {
            currTime += 1 * Time.deltaTime;
            timerText.text = $"{Mathf.Floor(currTime / 600)}{Mathf.Floor(currTime / 60) % 10}:{Mathf.Floor(currTime / 10) % 6}{Mathf.Floor(currTime) % 10}";
        }
    }

    public void StartTimer()
    {
        startTimer = true;
    }

    public void StopTimer()
    {
        startTimer = false;
        HighScore = currTime;
    }
}
