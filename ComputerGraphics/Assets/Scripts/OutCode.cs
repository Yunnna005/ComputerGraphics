using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class OutCode
{
    private Vector2 point;

    public bool up, down, left, right;

    public OutCode(Vector2 point)
    {
        this.point = point;
        up = point.y > 1;
        down = point.y < -1;
        left = point.x < -1;
        right = point.x > 1;
    }
    public OutCode(bool upIn, bool downIn, bool leftIn, bool rightIn)
    {
        this.up = upIn;
        this.down = downIn;
        this.left = leftIn;
        this.right = rightIn;
    }

    public string Display()
    {
        string result = "";

        result += (up) ? "1" : "0";
        result += (down) ? "1" : "0";
        result += (left) ? "1" : "0";
        result += (right) ? "1" : "0";

        Debug.Log(result);
        return result;
    }

    public static OutCode operator  +(OutCode oc1, OutCode oc2) 
    {
        return new OutCode(oc1.up || oc2.up, oc1.down || oc2.down, oc1.left || oc2.left, oc1.right || oc2.right);
    }

    public static OutCode operator *(OutCode oc1, OutCode oc2)
    {
        return new OutCode(oc1.up && oc2.up, oc1.down && oc2.down, oc1.left && oc2.left, oc1.right && oc2.right);
    }

    public static bool operator ==(OutCode oc1, OutCode oc2)
    {
        return (oc1.up == oc2.up) && (oc1.down == oc2.down) && (oc1.left == oc2.left) && (oc1.right == oc2.right);
    }

    public static bool operator !=(OutCode oc1, OutCode oc2)
    {

        return !(oc1 == oc2);
    }

}


