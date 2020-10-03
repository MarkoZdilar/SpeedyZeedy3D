using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowScore : MonoBehaviour
{
    public static float elapsedTime = 0.0f;
    public static float metersLeft = 300;
    public Text scoreText;
    public Text meterText;
    public DeathMenu deathMenu;
    private bool dead = false;
    void Update()
    {
        if (dead)
            return;
        meterText.text = "Meters left: " + ((int)metersLeft).ToString() + "m";
        elapsedTime += Time.deltaTime;
        scoreText.text = String.Format("{0:F2}", elapsedTime) + " sec";
    }
    public void OnDeath()
    {
        dead = true;
        deathMenu.StartEndMenuSnow(elapsedTime);
    }
}
