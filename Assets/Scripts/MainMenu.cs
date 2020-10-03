using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text highscoreText;
    public Text highscoreTextInSeconds;
    void Start()
    {
        highscoreText.text = "Longest run: " + ((int)PlayerPrefs.GetFloat("Highscore")).ToString() + "m";
        highscoreTextInSeconds.text = "Best time: " + String.Format("{0:F2}", PlayerPrefs.GetFloat("HighscoreInSeconds")).ToString() + "sec";
    }

    public void Togame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void TimeRun()
    {
        SceneManager.LoadScene("SnowLevel");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
