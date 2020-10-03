using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowLevelPlayerMotor : MonoBehaviour
{

    private CharacterController chController;
    private float zeroTime;
    private float cameraAnimationDuration = 2.0f;
    private float chSpeed = 7.0f;
    private float jumpForce = 26.0f;
    private float vectorY;
    private float gravityForce = 1.0f;
    private bool soundOn;
    private bool footstepsPlayedSound = false;
    private bool footsteps3Played = false;
    private bool overTheFenceSoundPlayed = false;
    private bool dead;
    private bool gameOver = false;
    private Vector3 movingVector;

    void Start()
    {
        chController = GetComponent<CharacterController>();
        zeroTime = Time.time;
    }

    void Update()
    {
        if (dead)
            return;
        if (!footstepsPlayedSound && (transform.position.y < 0.3 || transform.position.y > -0.1))
        {
            FindObjectOfType<AudioManager>().Play("Footsteps2");
            footstepsPlayedSound = true;
        }
        if (transform.position.y < -0.1 || transform.position.y > 0.3)
        {
            FindObjectOfType<AudioManager>().Stop("Footsteps2");
            footstepsPlayedSound = false;
        }
        if ((transform.position.x < -9 || transform.position.x > 9) && !overTheFenceSoundPlayed)
        {
            FindObjectOfType<AudioManager>().Play("OverTheFence");
            footstepsPlayedSound = false;
            gameOver = true;
            overTheFenceSoundPlayed = true;
            FindObjectOfType<AudioManager>().Stop("Footsteps2");
            Death();
        }
        movingVector = Vector3.zero;

        if (Time.time - zeroTime < cameraAnimationDuration)
        {
            
            chController.Move(Vector3.forward * chSpeed * Time.deltaTime);
            return;
        }

        // X

        movingVector.x = Input.GetAxisRaw("Horizontal") * (chSpeed + 10);

        // Y

        if (transform.position.y < 0.3 && Input.GetKeyDown(KeyCode.Space))
        {
            vectorY = jumpForce;
        }

        movingVector.y = vectorY;
        vectorY -= gravityForce;

        // Z
        SnowScore.metersLeft -= Time.deltaTime * 2;
        if((int)SnowScore.metersLeft == 0)
        {
            if(PlayerPrefs.GetFloat("HighscoreInSeconds") == 0.0)
                PlayerPrefs.SetFloat("HighscoreInSeconds", SnowScore.elapsedTime);
            else if (PlayerPrefs.GetFloat("HighscoreInSeconds") > SnowScore.elapsedTime)
                PlayerPrefs.SetFloat("HighscoreInSeconds", SnowScore.elapsedTime);
            Death();
            gameOver = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            movingVector.z = chSpeed * 3;
            SnowScore.metersLeft -= Time.deltaTime * 6;

            FindObjectOfType<AudioManager>().Stop("Footsteps2");
            footstepsPlayedSound = false;
            if (footsteps3Played == false)
            {
                FindObjectOfType<AudioManager>().Play("Footsteps3");
                footsteps3Played = true;
            }  
        }
        else if (footsteps3Played == true && !Input.GetKey(KeyCode.UpArrow))
        {
            FindObjectOfType<AudioManager>().Stop("Footsteps3");
            footsteps3Played = false;
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            movingVector.z = chSpeed / 3;
            SnowScore.metersLeft -= Time.deltaTime;
        }
        else
            movingVector.z = chSpeed;

        chController.Move(movingVector * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle") && (hit.point.z > transform.position.z + chController.radius + 0.2f) && gameOver == false)
        {
            if (!soundOn)
            {
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                soundOn = true;
            }
            FindObjectOfType<AudioManager>().Stop("Footsteps2");
            FindObjectOfType<AudioManager>().Stop("Footsteps3");
            Death();
        }
    }

    private void Death()
    {
        FindObjectOfType<AudioManager>().Stop("Footsteps2");
        FindObjectOfType<AudioManager>().Stop("Footsteps3");
        dead = true;
        GetComponent<SnowScore>().OnDeath();
    }
}
