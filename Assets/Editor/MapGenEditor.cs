using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Xml;
using System.Text;

[CustomEditor (typeof (MapGenerator))]
public class MapGenEditor : Editor {

    /**
     * TODO: Add XML file loading
    **/

    MapGenerator mapGen;

    public override void OnInspectorGUI() {

        mapGen = (MapGenerator)target;

        DrawDefaultInspector();

        // XML Options
        GUILayout.Label("XML Data Controls", EditorStyles.boldLabel);

        if (GUILayout.Button("Save Map")) {
            mapGen.SaveXMLData();
        }

        if (GUILayout.Button("Load Map")) {
            mapGen.LoadXMLData();
        }

        if (GUILayout.Button("Clear Map")) {
            mapGen.ClearMapData(); // clear the map program data
            mapGen.SaveXMLData(); ; // save the changes to the working file
        }

    }
  
    void OnSceneGUI() {
        if (Event.current.type == EventType.MouseDown) {
            if (mapGen.editMode) {
                if (Event.current.button == 0) { // left click
                    PlaceFeature();
                }
                if (Event.current.button == 1) { // right click (TODO: Map this to something better!)
                    //mapGen.SaveFeatures();
                }
            } else { Debug.Log("Edit mode is disabled! Please enable it in the script options.");  }
        }
    }

    private void PlaceFeature() {
        RaycastHit hit;
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        if (Physics.Raycast(ray, out hit, 10000f)) {

            // Get Terrain Click Coordinates
            Vector2 terrainClick = new Vector2((int)(mapGen.dimension * hit.textureCoord.x), (int)(mapGen.dimension * hit.textureCoord.y));

            // Find Empty Mountain Index
            int index = 0;
            for (int i = 0; i < mapGen.worldMountains.Length; i++) {
                if (mapGen.worldMountains[i] == null) {
                    index = i;
                    break;
                }
            }

            // DEBUG CUBE SPAWN - START 
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(terrainClick.x, 0, terrainClick.y);
            // DEBUG - END 

            // Store Mountain Object at Click Coordinates
            mapGen.worldMountains[index] = new Mountain();
            mapGen.worldMountains[index].mapPos = terrainClick;
            Debug.Log("Mountain Index: " + index + ". Mountain X: " + mapGen.worldMountains[index].mapPos.x + ". Mountain Y: " + mapGen.worldMountains[index].mapPos.y + ".");
        }
    }

}
