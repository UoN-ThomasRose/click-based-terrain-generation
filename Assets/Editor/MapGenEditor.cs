using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (MapGenerator))]
public class MapGenEditor : Editor {

    MapGenerator mapGen;
    string loadXmlFileName = "TerrainData";
    string saveXmlFileName = "TerrainData";

    public override void OnInspectorGUI() {

        mapGen = (MapGenerator)target;

        DrawDefaultInspector();

        //if (DrawDefaultInspector())
        //  if (mapGen.autoUpdate)
        //      mapGen.GenerateMap();

        //if (GUILayout.Button("Generate")) {
        //  mapGen.GenerateMap();
        //}

        //if (GUILayout.Button("Flatten")) {
        //  mapGen.FlattenMap();
        //}

        // XML Options
        GUILayout.Label("XML Data Settings", EditorStyles.boldLabel);

        saveXmlFileName = EditorGUILayout.TextField("Save File", saveXmlFileName);
        if (GUILayout.Button("Save XML Data")) {
            mapGen.SaveFeatures();
        }

        loadXmlFileName = EditorGUILayout.TextField("Load File", loadXmlFileName);
        if (GUILayout.Button("Load XML Data")) {
            mapGen.LoadXMLData();
        }

        if (GUILayout.Button("Clear Map Data")) {
            mapGen.ClearMapData();
        }

    }

    /* NOT WORKING
    void OnSceneGUI() {
        if (Event.current.type == EventType.MouseDown) {
            if (mapGen.editMode) {
                if (Event.current.button == 0) { // left click
                    mapGen.PlaceFeature();
                }
                if (Event.current.button == 1) { // right click (TODO: Map this to something better!)
                    mapGen.SaveFeatures();
                }
            } else { Debug.Log("Edit mode is disabled! Please enable it in the script options.");  }
        }
    }
    */

}
