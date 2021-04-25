using UnityEditor;
using UnityEngine;
using System.Collections;

public class TerrainPerlinNoise : ScriptableWizard
{

    public float Tiling = 10.0f;

    [MenuItem("Terrain/Generate from Perlin Noise")]
    public static void CreateWizard(MenuCommand command)
    {
        ScriptableWizard.DisplayWizard("Perlin Noise Generation Wizard", typeof(TerrainPerlinNoise));
    }

    void OnWizardUpdate()
    {
        helpString = "This small generation tool allows you to generate perlin noise for your terrain.";
    }

    void OnWizardCreate()
    {
        GameObject obj = Selection.activeGameObject;

        if (obj.GetComponent<Terrain>())
        {
            GenerateHeights(obj.GetComponent<Terrain>(), Tiling);
        }
    }

    public void GenerateHeights(Terrain terrain, float tileSize)
    {
        float[,] heights = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];

        for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
        {
            for (int k = 0; k < terrain.terrainData.heightmapResolution; k++)
            {
                heights[i, k] = Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapResolution) * tileSize, ((float)k / (float)terrain.terrainData.heightmapResolution) * tileSize) / 10.0f;
            }
        }

        terrain.terrainData.SetHeights(0, 0, heights);
    }
}