using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateNoise : MonoBehaviour
{
    private int width = 500;
    private int height = 500;

    private float scale;
    private float threshold;

    [SerializeField]
    private generateVeg genVeg;

    // Start is called before the first frame update
    void Start()
    {

        scale = genVeg.getScale();
        threshold = genVeg.getthreshold();

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();

    }


    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color colour = CalculateColour(x, y);
                texture.SetPixel(x, y, colour);
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalculateColour(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        // Round colour values to black or white based on threshold value
        if (sample > threshold)
        {
            return Color.white;
        }
        else
        {
            return Color.black;
        }


    }
}
