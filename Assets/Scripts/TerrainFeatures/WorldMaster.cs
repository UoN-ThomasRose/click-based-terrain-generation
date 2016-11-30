using UnityEngine;
using System.Collections;

public class WorldMaster {

    // Terrain Features
    public Mountain[] mountains;
    public Hill[] hills;
    // make sure all the terrain feature classes do NOT inherit MonoBehaviour as this will stop WorldMaster from serializing

    public WorldMaster() {
        mountains = new Mountain[100];
        hills = new Hill[100];
    }
}
