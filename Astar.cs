using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Astar
{
    List<Node> openList = new List<Node>();
    List<Node> closeList = new List<Node>();
    private int[,] map;

    public List<Vector2> Calculate(int[,] map, Vector2 startPos, Vector2 endPos)
    {
        this.map = map;
        
        int c = 0;
        var startNode = new Node(null, startPos);
        var endNode = new Node(null, endPos);
        
        openList.Clear();
        closeList.Clear();
        
        openList.Add(startNode);
        
        while (openList.Count > 0)
        {
            c++;
            if (c > 10000) break;
            var currentNode = FindMin(openList);
            openList.Remove(currentNode);
            closeList.Add(currentNode);

            if (endNode.Position == currentNode.Position)
            {
                Node current = currentNode;
                var path = new List<Vector2>();
                while (current != null)
                {
                    path.Add(current.Position);
                    current = current.Parent;
                }
                return path;
            }

            var children = new List<Node>();
            var neighbor = GetNeighbor(currentNode.Position);
            foreach (var newPos in neighbor)
            {
                var h = map.GetLength(0);
                var w = map.GetLength(1);
                if(newPos.x < 0 || newPos.x >= map.GetLength(1) || newPos.y < 0 || newPos.y >= map.GetLength(0)) 
                    continue;

                if (IsMaze(newPos)) continue;

                var newNode = new Node(currentNode, newPos);
                children.Add(newNode);
            }

            foreach (var child in children)
            {
                if(IsClosed(child)) continue;

                child.g = currentNode.g + 1;
                child.h = (int) (Mathf.Pow(child.Position.x - endNode.Position.x, 2) +
                          Mathf.Pow(child.Position.y - endNode.Position.y, 2));
                child.f = child.g + child.h;

                if (IsOpen(child)) continue;
                openList.Add(child);
            }
        }

        return null;
    }

    private Node FindMin(List<Node> list)
    {
        if (list == null || list.Count == 0) return null;
        
        Node minNode = list[0];
        foreach (var node in list)
        {
            if (minNode.f > node.f) minNode = node;
        }

        return minNode;
    }

    private List<Vector2> GetNeighbor(Vector2 pos)
    {
        var neighbor = new List<Vector2>();
        neighbor.Add(Vector2.up + pos);
        neighbor.Add(Vector2.down + pos);
        neighbor.Add(Vector2.left + pos);
        neighbor.Add(Vector2.right + pos);
        return neighbor;
    }

    private bool IsMaze(Vector2 pos)
    {
        return map[(int)pos.y, (int)pos.x] == 1;
    }

    private bool IsClosed(Node node)
    {
        foreach (var closeNode in closeList)
        {
            if (closeNode.Position.x == node.Position.x &&
                closeNode.Position.y == node.Position.y)
                return true;
        }

        return false;
        
    }

    private bool IsOpen(Node node)
    {
        foreach (var openNode in openList)
        {
            if (openNode.Position.x == node.Position.x &&
                openNode.Position.y == node.Position.y &&
                openNode.g < node.g) return true;
        }

        return false;
    }
}
