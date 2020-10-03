using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public Text scoreText; 
    public Image backgroundImage;
    private bool isShowned = false;
    private float transition = 6.0f;
    void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (!isShowned)
            return;
        transition += Time.deltaTime;
        backgroundImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
    }
    public void StartEndMenu (float score)
    {
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        isShowned = true;
    }
    public void StartEndMenuSnow(float score)
    {
        gameObject.SetActive(true);
        if ((int)SnowScore.metersLeft == 0)
        {
            scoreText.text = String.Format("{0:F2}", score) + " sec";
            SnowScore.metersLeft = 300;
            SnowScore.elapsedTime = 0.0f;
        }
        else
        {
            scoreText.text = "You died";
            SnowScore.metersLeft = 300;
            SnowScore.elapsedTime = 0.0f;
        }
        isShowned = true;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ToMainManu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
