using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

public class MapGenerator : MonoBehaviour {

    // Public Variables
    public int dimension;
    public string dataFileName = "TerrainData";
    public Terrain terrainMesh;
    public bool editMode;

    // Terrain Features
    public WorldMaster world = new WorldMaster();

    public MapGenerator() {
        terrainMesh = GetComponent<Terrain>();
    }

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
        for (int i = 0; i < world.mountains.Length; i++)
            world.mountains[i] = null;
        Debug.Log("Map data has been cleared.");
    }

    public void LoadXMLData() {
        Debug.Log("Loading XML data...");

        using(FileStream fs = new FileStream(dataFileName + ".xml", FileMode.Open)) {
            XmlSerializer xs = new XmlSerializer(typeof(WorldMaster));
            world = (WorldMaster) xs.Deserialize(fs);
        }

        // Debug
        for (int i = 0; i < world.mountains.Length; i++) {
            if (world.mountains[i] == null) break;
            Debug.Log("Mountain " + i + ". X = " + world.mountains[i].mapPos.x + ". Y = " + world.mountains[i].mapPos.y);
        }

        Debug.Log("XML data has loaded.");
    }

    public void SaveXMLData() {
        Debug.Log("Saving the terrain features...");

        // Save the mountain data in XML
        XmlSerializer xs = new XmlSerializer(typeof(WorldMaster));
        TextWriter tw = new StreamWriter(dataFileName + ".xml");
        xs.Serialize(tw, world);
        tw.Close();

        /*
        XmlTextWriter writer = new XmlTextWriter(dataFileName + ".xml", Encoding.UTF8);
        writer.Formatting = Formatting.Indented;
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
        writer.Close();
        */

        Debug.Log("Terrain features have been saved in XML.");
        
    }

    void Update() {
        // nothing to see here...
    }

}
