using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Credits;
  public void Playgame()
    {
       SceneManager.LoadScene(1);
    }
  public void QuitGame()
    {
        Application.Quit();
    }

  public void activateCredits()
  {
    Credits.SetActive(true);
  }

  public void deActivate()
  {
    Credits.SetActive(false);
  }
}
