using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    Vector3 startPos;

    [SerializeField] float walkRate = 13;
    [SerializeField] float runRate = 18;

    [SerializeField] float ampValue= 0.00045f;
    
    [SerializeField] float toggleSpeed = 3.0f;
    [SerializeField] float runToggleSpeed = 7.0f;

    [SerializeField] RigidbodyMovement MovementScript;

    // Start is called before the first frame update
    void Start()=> startPos = transform.localPosition;

    // Update is called once per frame  
    void Update()
    {
        CheckMotion();
    }

    void CheckMotion()
    {
        float speed = MovementScript.myVelocity.magnitude;

        if(speed>toggleSpeed && MovementScript.isGrounded)
        {
            transform.localPosition += stepMotion(walkRate);

            if (speed > runToggleSpeed)
            {
                transform.localPosition += stepMotion(runRate);
            }
        }

        if (transform.localPosition != startPos && speed<toggleSpeed)
        {   
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, .01f);
        }
    }

    Vector3 stepMotion(float rate)
    {
        Vector3 pos = Vector3.zero;
        pos.x += Mathf.Cos(Time.time * rate / 2) * ampValue * 2;
        pos.y += Mathf.Sin(Time.time * rate) * ampValue;
        return pos;
    }

    
}
