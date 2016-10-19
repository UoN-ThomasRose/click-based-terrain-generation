using UnityEngine;
using System.Collections;
using System.Text;
using System.Xml;

public class MapGenerator : MonoBehaviour {

    // Public Variables
    public int dimension;
    public Terrain terrainMesh;
    public string dataFileName = "TerrainData";
    public bool editMode; 
    //public bool autoUpdate;

    // Terrain Features - TODO: Find a way to load data from XML
    private Mountain[] worldMountains = new Mountain[100];

    public void GenerateMap() {
        terrainMesh.terrainData.size = new Vector3(dimension, 255, dimension);

        float[,] map = new float[dimension, dimension];

        // Flatten the map - set all height values to 0
        for (int x = 0; x < dimension; x++)
            for (int y = 0; y < dimension; y++)
                map[x, y] = 0;

        TerrainGenerator.GenerateTerrainMesh(terrainMesh, map);
    }

    public void FlattenMap() {
        float[,] map = new float[dimension, dimension];

        // Flatten the map - set all height values to 0
        for (int x = 0; x < dimension; x++)
            for (int y = 0; y < dimension; y++)
                map[x, y] = 0;

        TerrainGenerator.GenerateTerrainMesh(terrainMesh, map);
    }

    public void ClearMapData() {
        for (int i = 0; i < worldMountains.Length; i++)
            worldMountains[i] = null;
        Debug.Log("Map data has been cleared.");
    }

    public void LoadXMLData() {
        Debug.Log("Loading XML data...");


    }

    /*
     * =============================================
     * ============ THIS DOES NOT WORK! ============
     * ============================================-
     * 
    */
    public void PlaceFeature() { 
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Event.current.mousePosition /*Input.mousePosition*/); 

        if(Physics.Raycast(ray, out hit, 10000f)) {

            // Get Terrain Click Coordinates
            Vector2 terrainClick = new Vector2((int)(dimension * hit.textureCoord.x), (int)(dimension * hit.textureCoord.y));

            // Find Empty Mountain Index
            int index = 0;
            for (int i = 0; i < worldMountains.Length; i++) {
                if (worldMountains[i] == null) {
                    index = i;
                    break;
                }
            }

            /* DEBUG CUBE SPAWN - START */
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
            /* DEBUG - END */

            // Store Mountain Object at Click Coordinates (TODO: Fix these calculations! They are way off now for some reason!)
            worldMountains[index] = new Mountain();
            worldMountains[index].mapPos = terrainClick;
            Debug.Log("Mountain Index: " + index + ". Mountain X: " + worldMountains[index].mapPos.x + ". Mountain Y: " + worldMountains[index].mapPos.y + ".");
        }
    }

    public void SaveFeatures() { 
        XmlTextWriter writer = new XmlTextWriter(dataFileName + ".xml", Encoding.UTF8); 
        writer.Formatting = Formatting.Indented;

        // Save the mountain data in XML
        for(int i = 0; i < worldMountains.Length; i++) {
            if (worldMountains[i] == null) break;

            writer.WriteStartElement("Mountain");
                writer.WriteStartElement("Index");
                    writer.WriteString(i.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("X Position");
                    writer.WriteString(worldMountains[i].mapPos.x.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("Y Position");
                    writer.WriteString(worldMountains[i].mapPos.y.ToString());
                writer.WriteEndElement();
            writer.WriteEndElement();
        }

        Debug.Log("Terrain features have been saved in XML.");
        writer.Close();
    }

    /* 
     * =====================================
     * ============ THIS WORKS! ============
     * =====================================
     * */
    void Update() {

        // PlaceFeature()
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {

                // Get Terrain Click Coordinates
                Vector2 terrainClick = new Vector2((int)(dimension * hit.textureCoord.x), (int)(dimension * hit.textureCoord.y));

                // Find Empty Mountain Index
                int index = 0;
                for (int i = 0; i < worldMountains.Length; i++) {
                    if (worldMountains[i] == null) {
                        index = i;
                        break;
                    }
                }

                /* DEBUG CUBE SPAWN */
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(terrainClick.x, 0, terrainClick.y);
                /* DEBUG END */

                // Store Mountain Object at Click Coordinates (TODO: Fix these calculations! They are way off now for some reason!)
                worldMountains[index] = new Mountain();
                worldMountains[index].mapPos = terrainClick;
                Debug.Log("Mountain Index: " + index + ". Mountain X: " + worldMountains[index].mapPos.x + ". Mountain Y: " + worldMountains[index].mapPos.y + ".");
            }
        }

        if(Input.GetMouseButtonDown(1)) {
            ClearMapData();
        }

        // SaveFeatures()
        if (Input.GetMouseButtonDown(2)) {
            XmlTextWriter writer = new XmlTextWriter(dataFileName + ".xml", Encoding.UTF8);
            writer.Formatting = Formatting.Indented;

            // Save the mountain data in XML
            for (int i = 0; i < worldMountains.Length; i++) {
                if (worldMountains[i] == null) break;

                writer.WriteStartElement("Mountain");
                writer.WriteStartElement("Index");
                writer.WriteString(i.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("X Position");
                writer.WriteString(worldMountains[i].mapPos.x.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("Y Position");
                writer.WriteString(worldMountains[i].mapPos.y.ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();
            }

            Debug.Log("Terrain features have been saved in XML.");
            writer.Close();
        }
    }

}
