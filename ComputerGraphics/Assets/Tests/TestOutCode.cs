using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestOutCode
{
    string result, expected;
    bool resultBool;

    [Test]
    public void TestOutCode_Case1()
    {
        OutCode outCode1 = new OutCode(new Vector2(0.6f, 2.2f));
        result = outCode1.Display();
        expected = "1000";
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestOutCode_Case2()
    {
        OutCode outCode1 = new OutCode(new Vector2(2.6f, -4.2f));
        result = outCode1.Display();
        expected = "0101";

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestOutCode_Case3()
    {
        OutCode outCode1 = new OutCode(new Vector2(0.6f, 2.2f));
        OutCode outCode2 = new OutCode(new Vector2(2.6f, -4.2f));
        resultBool = (outCode1 == outCode2);

        Assert.IsFalse(resultBool);
    }
}
