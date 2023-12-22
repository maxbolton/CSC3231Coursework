using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calcStats : MonoBehaviour
{

    [SerializeField]
    private GameObject fpsCounter;

    [SerializeField]
    private GameObject memUsage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fpsCounter.GetComponent<TextMesh>().text = (Time.frameCount / Time.time).ToString();


        long memoryUsage = System.GC.GetTotalMemory(false);

        // Convert to megabytes for readability
        float memoryUsageMB = memoryUsage / (1024f * 1024f);    
        memUsage.GetComponent<TextMesh>().text = (memoryUsageMB.ToString("F2") + " MB");
    }
}
