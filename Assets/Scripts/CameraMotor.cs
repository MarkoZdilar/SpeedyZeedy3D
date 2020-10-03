using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    private Transform lookAt;
    private Vector3 startOffset;
    private Vector3 moveVector;

    private float transition = 0.0f;
    private float animationDuration = 2.0f;
    private Vector3 animationOffset = new Vector3(0,8,0);

    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
    }

    void Update()
    {
        moveVector = lookAt.position + startOffset;

        moveVector.x = 0;

        moveVector.y = Mathf.Clamp(moveVector.y, 3, 15);

        if(transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += (1 / animationDuration) * Time.deltaTime;
            transform.LookAt(lookAt.position + Vector3.up);
        }
    }
}
