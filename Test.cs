using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var map = new int[,]
        {
            {0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            {1, 1, 1, 0, 1, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0},
            {0, 0, 0, 0, 0, 1, 1, 1, 1, 1},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        };
        Astar astar = new Astar();
        
        var start = new Vector2(0,0);
        var end = new Vector2(9,9);
        var result = astar.Calculate(map, start, end);
        if (result == null)
        {
            Debug.LogError("result in null");
            return;
        }

        StringBuilder sb = new StringBuilder();
        foreach (var r in result)
        {
            sb.Append($" ({r.x} {r.y}) ");
        }
        Debug.Log(sb.ToString());
    }
}
