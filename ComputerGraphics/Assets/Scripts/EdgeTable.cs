using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTable
{
    public EdgeTable() 
    {
     edgeTable = new Dictionary<int, RangeX> ();
    }

    internal Dictionary<int, RangeX> edgeTable;

    internal void Add(List<Vector2Int> points)
    {
        foreach (Vector2Int point in points)
        {
            if (edgeTable.ContainsKey(point.y))
            {
                edgeTable[point.y].AddPoint(point.x);
            }
            else
            {
                edgeTable[point.y] = new RangeX();
                edgeTable[point.y].AddPoint(point.x);
            }
        }
    }
}
