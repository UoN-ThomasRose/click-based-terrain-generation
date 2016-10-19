using UnityEngine;
using System.Collections;

public static class TerrainGenerator {
    public static void GenerateTerrainMesh(Terrain terrainMesh, float[,] heightmap) {
        // TODO: Fix terrain resize issues (the values of 128, 256, 512 work well but a large number of other values don't)
        TerrainData td = terrainMesh.terrainData;
        td.baseMapResolution = heightmap.GetLength(0);
        td.heightmapResolution = heightmap.GetLength(0) - 1;
        td.SetHeights(0, 0, heightmap);
        //int pps = 5; // pixels-per-sample
        td.size = new Vector3((float)heightmap.GetLength(0), 255f, (float)heightmap.GetLength(1));
        terrainMesh.Flush();
    }
}
