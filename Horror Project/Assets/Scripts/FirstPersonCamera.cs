using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    [SerializeField] Transform cameraTransform;

    [SerializeField,Range(10,3000)] float Sensitivity = 100f;

    float xRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90, 90);

        cameraTransform.localRotation = Quaternion.Euler(xRot, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}
