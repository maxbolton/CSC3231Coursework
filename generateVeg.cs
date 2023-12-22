using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class generateVeg : MonoBehaviour
{

    [SerializeField]
    private GameObject treePref;


    private int width = 500;
    private int height = 500;
    private float scale = 50f;
    private float threshold = 0.05f;

    private float minDistanceBetweenTrees = 5f;
    private float maxSlopeAngle = 30f;

    [SerializeField]
    private Terrain terrain;


    private List<Vector3> spawnedTreePositions = new List<Vector3>();
    void Start()
    {
        GenerateTreeList();
    }


    private float FindVertexHeight(int x, int z)
    {
        // Get terrain data
        TerrainData terrainData = terrain.terrainData;


        // Calculate world position of the vertex
        float normalizedX = x * 1.0f / (terrainData.heightmapResolution - 1);
        float normalizedZ = z * 1.0f / (terrainData.heightmapResolution - 1);

        float terrainWidth = terrainData.size.x;
        float terrainLength = terrainData.size.z;

        float worldX = terrain.transform.position.x + normalizedX * terrainWidth;
        float worldZ = terrain.transform.position.z + normalizedZ * terrainLength;

        if (terrain.SampleHeight(new Vector3(worldX, 0f, worldZ)) >= 47f || terrain.SampleHeight(new Vector3(worldX, 0f, worldZ)) <= 8)
        {
            return -100f;
        }
        else
        {
            return terrain.SampleHeight(new Vector3(worldX, 0f, worldZ));
        }  
    }

    void GenerateTreeList()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool treeTrue = CalculatePosition(x, y);

                if (treeTrue)
                {
                    float globalY = FindVertexHeight(x, y);

                    if (globalY == -100)
                    {
                        Debug.Log("non-valid location");
                    }
                    else
                    {
                        Vector3 treePosition = new Vector3(x, globalY, y);

                        // Check if the slope at the new tree position is within the acceptable range
                        if (IsSlopeValid(treePosition))
                        {
                            // Check if the new tree position is far enough from existing trees
                            if (IsTreePositionValid(treePosition))
                            {
                                GameObject newTree = Instantiate(treePref);
                                newTree.transform.position = treePosition;
                                spawnedTreePositions.Add(treePosition);
                            }
                        }
                    }
                }
            }
        }
    }

    bool IsTreePositionValid(Vector3 position)
    {
        foreach (Vector3 existingPosition in spawnedTreePositions)
        {
            float distance = Vector3.Distance(existingPosition, position);
            if (distance < minDistanceBetweenTrees)
            {
                return false; // Too close to an existing tree
            }
        }
        return true;
    }

    bool IsSlopeValid(Vector3 position)
    {
        float slope = terrain.terrainData.GetSteepness(position.x / terrain.terrainData.size.x, position.z / terrain.terrainData.size.z);
        return slope <= maxSlopeAngle;
    }

    bool CalculatePosition(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        // Round colour values to black or white based on threshold value
        if (sample > threshold)
        {
            return false;
        }
        else
        {
            return true;
        }


    }

    public float getthreshold()
    {
        return threshold;
    }

    public float getScale()
    {
        return scale;
    }


}
