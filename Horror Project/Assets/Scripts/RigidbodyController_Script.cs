using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyController_Script : MonoBehaviour
{
    [SerializeField] float startUp;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float walkVelocityMag;
    [SerializeField] float runVelocityMag;
    [SerializeField] float jumpVelocity;
    [SerializeField] float detectionRadius;

    [SerializeField] Vector3 offSet;

    [SerializeField] LayerMask jumpable;

    Rigidbody rb;

    bool noInput;
    bool isGrounded;
    bool runButton;
    bool sneakButton;

    float inputX;
    float inputY;
    float targetVelocityMag;

    Vector3 movementVector;
    Vector3 inputVector;
    Vector3 horizontalVelocity;


    // Start is called before the first frame update
    void Start()
    {
        targetVelocityMag = walkVelocityMag;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Debug
        //Debug.Log(horizontalVelocity.magnitude);

        //bools
        runButton = Input.GetKey(KeyCode.LeftShift);
        sneakButton = Input.GetKey(KeyCode.LeftControl);
        noInput = inputVector.magnitude < 0.1f;
        isGrounded = Physics.CheckSphere(transform.position - offSet, detectionRadius, jumpable);

        //floats
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        
        //Vector3s
        inputVector = new Vector3(inputX, 0, inputY);
        movementVector = transform.right * inputX + transform.forward * inputY;

        horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        //methods
        if(isGrounded && Input.GetButtonDown("Jump"))
        {

            Jump();

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //methods
        if (isGrounded)
        {

            movementCycle();

            if (noInput)
            {

                Brake();
            }

        }

    }

    void movementCycle()
    {

       if(rb.velocity.magnitude < targetVelocityMag)
       {

            rb.AddForce(transform.forward * walkSpeed * inputY);
            rb.AddForce(transform.right * walkSpeed * inputX);

            if (runButton)
            {
                //Debug.Log("Run;");
            }
            
            else if (sneakButton)
            {
            }
       }
    }

    //methods

    void JumpStart()
    {
        rb.velocity += transform.forward * 10f;
    }

    void Brake()
    {
        if ( rb.velocity.magnitude > 0.1f )
        {
            Debug.Log("brake");
            rb.AddForce(-horizontalVelocity.normalized * runSpeed);
        }
    }

    void Jump()
    {
        rb.velocity += transform.up * jumpVelocity;
    }

    private void OnDrawGizmos()
    {
        Vector3 DirPos = (transform.position + rb.velocity.normalized);

        Gizmos.DrawLine(transform.position, DirPos);

        Gizmos.color = isGrounded ? Color.red : Color.white;
        Gizmos.DrawWireSphere(transform.position - offSet, detectionRadius);

    }
}
