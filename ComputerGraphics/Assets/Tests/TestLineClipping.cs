using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestLineClipping
{
    GraphicsPipeline gp = new GraphicsPipeline();
    Vector2 start, end;
    bool result;
    [Test]
    public void TestLineClipping_Case1()
    {
        start = new Vector2(0.1f, 0.1f);
        end = new Vector2(0.2f, 0.2f);

        result = gp.LineClipping(ref start, ref end);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestLineClipping_Case2()
    {
        start = new Vector2(-5f, -1f);
        end = new Vector2(-2f, -2f);

        result = gp.LineClipping(ref start, ref end);

        Assert.IsFalse(result);
    }
}
