using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour
{
    Camera mainCam;

    protected RaycastHit hit;
    public Vector3 cursorPosWorldSpace { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit);
        cursorPosWorldSpace = hit.point;

        Debug.DrawLine(transform.position, cursorPosWorldSpace);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(cursorPosWorldSpace,.5f);
    }
}
