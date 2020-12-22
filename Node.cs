using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node Parent;
    public Vector2 Position;

    public int f;
    public int g;
    public int h;

    public Node(Node parent, Vector2 position)
    {
        Parent = parent;
        Position = position;
    }
}
