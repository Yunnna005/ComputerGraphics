using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestIntersect : MonoBehaviour
{
    Vector2 start, end, expected, result;
    float tolerance = 0.0001f ; int edge;
    GraphicsPipeline gp = new GraphicsPipeline();

    [Test]
    public void TestIntersect_UpEdge()
    {
        start = new Vector2(-1.5f, 0.5f);
        end = new Vector2(0.5f, -1.5f);
        edge = 0; 

        expected = new Vector2(-2, 1);

        result = gp.Intersect(start, end, edge);

        Check_Expected_with_Result(expected, result);
    }
    [Test]
    public void TestIntersect_DownEdge()
    {
        start = new Vector2(0, 0);
        end = new Vector2(1, 1);
        edge = 1;

        expected = new Vector2(-1, -1);

        result = gp.Intersect(start, end, edge);
        Check_Expected_with_Result(expected, result);
    }
    [Test]
    public void TestIntersect_LeftEdge()
    {
        start = new Vector2(0, 0);
        end = new Vector2(1, 1);
        edge = 2;
        
        expected = new Vector2(-1, -1);
        result = gp.Intersect(start, end, edge);
        Check_Expected_with_Result(expected, result);
    }

    [Test]
    public void TestIntersect_RightEdge()
    {
        start = new Vector2(0, 0);
        end = new Vector2(1, 1);
        edge = 3; 

        expected = new Vector2(1, 1);
        result = gp.Intersect(start, end, edge);
        Check_Expected_with_Result(expected, result);
    }

    [Test]
    public void TestIntersect_VerticalLine()
    {
        start = new Vector2(0, 0);
        end = new Vector2(0, 1);
        edge = 0; 

        expected = new Vector2(0, 1);
        result = gp.Intersect(start, end, edge);
        Check_Expected_with_Result(expected, result);
    }

    [Test]
    public void TestIntersect_HorizontalLine()
    {
        start = new Vector2(0, 0);
        end = new Vector2(1, 0);
        edge = 0; 

        expected = new Vector2(float.NaN, float.NaN);

        result = gp.Intersect(start, end, edge);
        Check_Expected_with_Result(expected, result);
    }

    [Test]
    public void TestIntersect_InvalidEdge()
    {
        start = new Vector2(0, 0);
        end = new Vector2(1, 1);
        edge = 4; 

        expected = new Vector2(float.NaN, float.NaN);
        result = gp.Intersect(start, end, edge);
        Check_Expected_with_Result(expected, result);
    }

    public void Check_Expected_with_Result(Vector2 expected, Vector2 result)
    {
        bool expectedIsNaN = float.IsNaN(expected.x) && float.IsNaN(expected.y);
        bool resultIsNaN = float.IsNaN(result.x) && float.IsNaN(result.y);

        if (expectedIsNaN && resultIsNaN)
        {
            return;
        }

        Assert.AreEqual(expected.x, result.x, tolerance, "X coordinate does not match.");
        Assert.AreEqual(expected.y, result.y, tolerance, "Y coordinate does not match.");

        Assert.AreEqual(expected, result);
    }
}
