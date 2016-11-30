using UnityEngine;
using System.Collections;

public class Hill {

    public Vector2 mapPos;
    public float roughness;
    public float peakHeight;
    public Vector2 groundDimensions;

    public Hill() {

    }

    public Hill(Vector2 mapPos) {
        this.mapPos = mapPos;
    }

    public Hill(int xPos, int yPos) {
        mapPos = new Vector2(xPos, yPos);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
