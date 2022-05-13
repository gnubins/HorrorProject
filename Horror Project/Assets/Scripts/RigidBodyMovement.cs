using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{

    //TODO
    //Change to addforce

    [SerializeField] Animator cameraAnim;
    [SerializeField] LayerMask jumpable;

    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float crouchSpeed = 2f;
    [SerializeField] float bumpDetectionRange = 1f;
    [SerializeField] float jumpVelocity = 2.5f;
    [SerializeField] float jumpDetectionRadius;
    [SerializeField] float offset=.5f;
    [SerializeField] float climbRate = .1f;

    //Variables
    #region
    float height;

    bool run;
    bool isMoving;
    bool detectStairs;
    bool stairsTooHigh;
    bool crouch;
    bool crouched = false;

    public bool isGrounded { get; private set; }

    Rigidbody rb;
    CapsuleCollider col;

    public Vector3 velocity { get; private set; }
    Vector3 movementVector;
    Vector3 feetPos;
    Vector3 kneePos;

    Ray ray;
    RaycastHit hit;
    RaycastHit hit2;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;

        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        height = col.height;
    }

    //UpdateLoop
    #region

    // Update is called once per frame
    void Update()
    {
        cameraAnim.SetFloat("Speed", velocity.magnitude);

        velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        feetPos = transform.position - new Vector3(0,  1f, 0);
        kneePos = transform.position - new Vector3(0,  .4f, 0);

        run = Input.GetKey(KeyCode.LeftShift);
        crouch = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (crouch)
        {
            crouched = !crouched;
        }

        Crouch();

        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, offset, 0), jumpDetectionRadius,jumpable);
        isMoving = movementVector.magnitude > 2f;
        HandleStairs();
    }

    //movement
    private void FixedUpdate()
    {
        float x;
        float z;

        if (run)
        {
            x = Input.GetAxis("Horizontal") * runSpeed;
            z = Input.GetAxis("Vertical") * runSpeed;
        }
        else if (crouched)
        {
            x = Input.GetAxis("Horizontal") * crouchSpeed;
            z = Input.GetAxis("Vertical") *   crouchSpeed;
        }
        else
        {
            x = Input.GetAxis("Horizontal") * walkSpeed;
            z = Input.GetAxis("Vertical") * walkSpeed;
        }

        movementVector = new Vector3(x, 0, z);
        rb.velocity = transform.forward * z + transform.right * x + new Vector3(0, rb.velocity.y, 0);
    }

    #endregion


    //methods
    #region
    void HandleStairs()
    {
        raycastDetectStairs();
        if (detectStairs && !stairsTooHigh && isMoving)
        {
            rb.transform.position += new Vector3(0, climbRate, 0);
        }
    }

    void raycastDetectStairs()
    {

        Vector3 right = Vector3.Normalize(transform.forward + transform.right);
        Vector3 left = Vector3.Normalize(transform.forward - transform.right);

        detectStairs 
            = Physics.Raycast(feetPos,transform.forward, out hit,bumpDetectionRange, jumpable)
                    || Physics.Raycast(feetPos, right, out hit,bumpDetectionRange, jumpable)
                    || Physics.Raycast(feetPos, left, out hit ,bumpDetectionRange, jumpable);

        stairsTooHigh
                 = Physics.Raycast(kneePos,transform.forward, out hit2, bumpDetectionRange, jumpable)
                || Physics.Raycast(kneePos, right, out hit2, bumpDetectionRange, jumpable)
                || Physics.Raycast(kneePos, left,  out hit2, bumpDetectionRange, jumpable);
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);

            isGrounded = false;
        }
    }

    void Crouch()
    {
        if (crouched)
        {
            col.height = Mathf.Lerp(col.height, .5f,.5f);
        }
        else
        {
            col.height = Mathf.Lerp(col.height, height, .01f);
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.red : Color.white;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, offset, 0), jumpDetectionRadius);
        Gizmos.DrawRay(ray);
        Gizmos.DrawSphere(feetPos, .1f);
        Gizmos.DrawSphere(kneePos, .1f);
    }
   
}
