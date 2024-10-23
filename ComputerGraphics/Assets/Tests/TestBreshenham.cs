using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static UnityEngine.GraphicsBuffer;

public class TestBreshenham : MonoBehaviour 
{
    GraphicsPipeline gp = new GraphicsPipeline();
    Vector2Int start, end;
    List<Vector2Int> expected, result;

    [Test]
    public void TestBreshMethod_1()
    {
        start = new Vector2Int(12, 31);
        end = new Vector2Int(20, 35);
        expected = new List<Vector2Int>
        {
            new Vector2Int(12, 31),
            new Vector2Int(13, 31),
            new Vector2Int(14, 32),
            new Vector2Int(15, 32),
            new Vector2Int(16, 33),
            new Vector2Int(17, 33),
            new Vector2Int(18, 34),
            new Vector2Int(19, 34),
            new Vector2Int(20, 35),
        };

        result = gp.Bresh(start, end);

        Check_Expected_with_Result(expected, result);
    }

    [Test]
    public void TestBreshMethod_2()
    {
        start = new Vector2Int(12, 31);
        end = new Vector2Int(20, 28);
        expected = new List<Vector2Int>
        {
            new Vector2Int(12, 31),
            new Vector2Int(13, 31),
            new Vector2Int(14, 30),
            new Vector2Int(15, 30),
            new Vector2Int(16, 30),
            new Vector2Int(17, 29),
            new Vector2Int(18, 29),
            new Vector2Int(19, 28),
            new Vector2Int(20, 28),
        };

        result = gp.Bresh(start, end);

        Check_Expected_with_Result(expected, result);
    }

    //One more test needs to be created for Bresh() method to check when dy<0

    public void Check_Expected_with_Result(List<Vector2Int> expected, List<Vector2Int> result)
    {
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], result[i], $"Element at index {i} should be equal.");
        }
    }
}
