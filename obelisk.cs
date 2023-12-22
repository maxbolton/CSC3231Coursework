using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obelisk : MonoBehaviour
{


    [SerializeField]
    private bool isRotate = false;
    [SerializeField]
    private bool inverse = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isRotate)
        {
            if (inverse)
            {

                transform.Rotate(Vector3.up * 30f * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.up * -30f * Time.deltaTime);
            }
           
        }

        MeshRenderer m = GetComponent<MeshRenderer>();
        m.material.SetFloat("movement", Time.realtimeSinceStartup);
    }

}
