using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    //Assign a GameObject in the Inspector to rotate around
    public GameObject target;
    public GameObject directLight;

    void Update()
    {
        // Spin the object around the target at 20 degrees/second.
        //directLight.transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);
    }
}
