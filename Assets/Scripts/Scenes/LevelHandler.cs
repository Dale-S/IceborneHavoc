using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Debug.Log("Button Clicked");
        SceneManager.LoadScene(2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Body")
        {
            SceneManager.LoadScene(2);
        }
    }
}
