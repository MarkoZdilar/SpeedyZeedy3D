
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;
    private float speed = 10.0f;
    private Vector3 moveVector;
    private float gravity = 1.0f;
    private float animationDuration = 2.0f;
    private bool isDead = false;
    private float startTime;
    private bool soundPlayed = false;
    private bool soundPlayedFootsteps = false;
    private float jumpSpeed = 23.0f;
    private float directionY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startTime = Time.time;
    }
    void Update()
    {
        if (!soundPlayedFootsteps && (transform.position.y < 0.3 || transform.position.y > -0.1))
        {
            FindObjectOfType<AudioManager>().Play("Footsteps");
            soundPlayedFootsteps = true;
        }
        if (transform.position.y < -0.1 || transform.position.y > 0.3)
        {
            FindObjectOfType<AudioManager>().Stop("Footsteps");
            soundPlayedFootsteps = false;
        }

        if (isDead)
            return;
        if (Time.time - startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }
        moveVector = Vector3.zero;


        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;

        if (transform.position.y < 0.3 && Input.GetKeyDown(KeyCode.Space))
        {
            directionY = jumpSpeed;
        }

        moveVector.y = directionY;
        directionY -= gravity;

        if (transform.position.y < -8.0)
        {
            FindObjectOfType<AudioManager>().Play("WaterSplash");
            Death();
        }

        moveVector.z = speed;


        controller.Move(moveVector * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle") && (hit.point.z > transform.position.z + controller.radius + 0.2f))
        {
            if (!soundPlayed)
            {
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                soundPlayed = true;
            }
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
        FindObjectOfType<AudioManager>().Stop("Footsteps");
    }
    public void SetSpeed(float modifier)
    {
        speed = 5.0f + modifier;
    }

}
