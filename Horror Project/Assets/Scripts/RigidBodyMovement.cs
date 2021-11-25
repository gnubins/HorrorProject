
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float radius = .5f;

    LayerMask stairs = 1 << 7;
    LayerMask ground = 1 << 6;

    public Transform feet;

    Rigidbody rb;
    MouseControls mouseControls;

    Vector3 movement;

    bool isGrounded;
    bool isJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mouseControls = GetComponent<MouseControls>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), rb.velocity.y, Input.GetAxis("Vertical")));

        isGrounded = Physics.CheckSphere(feet.position, .5f,ground);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(feet.position, stairs);
    }

    void HandleRotation()
    {

    }

    void MovePlayer()
    {
        rb.velocity=movement * speed;

        HandleStairs();
    }

    void Jump()
    {

    }

    void HandleStairs()
    {

    }

    void AutoStop()
    {

    }
}
