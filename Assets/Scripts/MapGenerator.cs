using UnityEngine;
using System.Collections;
using System.Xml;
using System.Text;

public class MapGenerator : MonoBehaviour {

    // Public Variables
    public int dimension;
    public Terrain terrainMesh;
    public string dataFileName = "TerrainData";
    public bool editMode; 

    // Terrain Features
    public Mountain[] worldMountains = new Mountain[100];

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
        // TODO: Add code for XML loading
    }

    public void SaveXMLData() {
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

    void Update() {
        // nothing to see here...
    }

}
