using UnityEngine;
using System.Collections;

public class Mountain {

    public Vector2 mapPos;

    public Mountain() {

    }

    public Mountain(Vector2 mapPos) {
        this.mapPos = mapPos;
    }

    public Mountain(int xPos, int yPos) {
        mapPos = new Vector2(xPos, yPos);
    }
}
