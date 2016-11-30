﻿using UnityEngine;
using System.Collections;

public class Mountain {

    public Vector2 mapPos;
    public float roughness;
    public float peakHeight;
    public Vector2 groundDimensions;

    public Mountain() {

    }

    public Mountain(Vector2 mapPos) {
        this.mapPos = mapPos;
    }

    public Mountain(int xPos, int yPos) {
        mapPos = new Vector2(xPos, yPos);
    }

    //public string toString() {
        //return "X pos: " + mapPos.x + ". Y pos:" + mapPos.y + ".";
    //}
}
