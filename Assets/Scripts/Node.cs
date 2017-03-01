using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public Point GridPosition { private set; get; }

    public TileScript TileRef { get; private set; }

    public Vector2 WorldPosition { get; set; }

    public Node(TileScript tileRef)
    {
        this.TileRef = tileRef;
        this.GridPosition = tileRef.GridPosition;
        this.WorldPosition = tileRef.WorldPosition;
    }

}
