using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LevelEnd : MonoBehaviour
{
    public Timer T;
    public TextMeshProUGUI timerText;

    private void Update()
    {
        timerText.text = $"Your Time: {T.DisplayHighScore()}";
    }
}
