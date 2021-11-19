
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 10f;

    public Transform feet;

    Rigidbody rb;
    MouseControls mc;

    Vector3 movement;

    bool isGrounded;
    bool isJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mc = GetComponent<MouseControls>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        isGrounded = Physics.CheckSphere(feet.position, .5f,1<<6);
        Debug.Log(isGrounded);

        isJump = Input.GetButtonDown("Jump") && isGrounded;


        if (isJump)
        {
            Jump();
        }

        HandleRotation();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    void HandleRotation()
    {

    }

    void MovePlayer()
    {
        rb.AddForce(movement.normalized * speed);

        HandleStairs();
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    void HandleStairs()
    {

    }

    void AutoStop()
    {

    }
}
