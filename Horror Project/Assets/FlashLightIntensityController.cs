using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightIntensityController : MonoBehaviour
{

    Light light;
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            if(3 < Vector3.Distance(hit.point, transform.position))
            {
                light.intensity = 50f;
                Debug.Log("X");
            }
        }
    }
}
