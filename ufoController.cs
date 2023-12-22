using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufoController : MonoBehaviour
{

    Light pointLight;

    [SerializeField]
    private GameObject targetPoint;
    private float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        pointLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        pointLight.intensity = (Mathf.Sin(Time.realtimeSinceStartup*2) * 100) + 100;
        Debug.Log("PLight Intensity:"+ pointLight.intensity);


        // Rotate the object around the target point on the Y-axis
        transform.RotateAround(targetPoint.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);

    }
}
