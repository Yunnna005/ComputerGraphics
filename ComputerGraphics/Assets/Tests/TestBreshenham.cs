using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class TestBreshenham : MonoBehaviour 
{
    GraphicsPipeline gp = new GraphicsPipeline();
    Vector2Int start, end;
    List<Vector2Int> expected, result;

    [Test]
    public void TestBreshMethod_HorizontalLine()
    {
        start = new Vector2Int(0, 0);
        end = new Vector2Int(5, 0);
        expected = new List<Vector2Int>
        {
            new Vector2Int(0, 0),
            new Vector2Int(1, 0),
            new Vector2Int(2, 0),
            new Vector2Int(3, 0),
            new Vector2Int(4, 0),
            new Vector2Int(5, 0),
        };

        result = gp.Bresh(start, end);

        Check_Expected_with_Result(expected, result);
    }

    [Test]
    public void TestBreshMethod_VerticalLine()
    {
        start = new Vector2Int(0, 0);
        end = new Vector2Int(0, 5);
        expected = new List<Vector2Int>
        {
            new Vector2Int(0, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, 2),
            new Vector2Int(0, 3),
            new Vector2Int(0, 4),
            new Vector2Int(0, 5),
        };

        result = gp.Bresh(start, end);

        Check_Expected_with_Result(expected, result);
    }

    [Test]
    public void TestBreshMethod_DiagonalLine_PositiveSlope()
    {
        start = new Vector2Int(0, 0);
        end = new Vector2Int(5, 5);
        expected = new List<Vector2Int>
        {
            new Vector2Int(0, 0),
            new Vector2Int(1, 1),
            new Vector2Int(2, 2),
            new Vector2Int(3, 3),
            new Vector2Int(4, 4),
            new Vector2Int(5, 5),
        };

        result = gp.Bresh(start, end);

        Check_Expected_with_Result(expected, result);
    }

    [Test]
    public void TestBreshMethod_DiagonalLine_NegativeSlope()
    {
        start = new Vector2Int(5, 5);
        end = new Vector2Int(0, 0);
        expected = new List<Vector2Int>
        {
            new Vector2Int(0, 0),
            new Vector2Int(1, 1),
            new Vector2Int(2, 2),
            new Vector2Int(3, 3),
            new Vector2Int(4, 4),
            new Vector2Int(5, 5),
        };

        result = gp.Bresh(start, end);

        Check_Expected_with_Result(expected, result);
    }

    public void Check_Expected_with_Result(List<Vector2Int> expected, List<Vector2Int> result)
    {
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], result[i], $"Element at index {i} should be equal.");
        }
    }
}
