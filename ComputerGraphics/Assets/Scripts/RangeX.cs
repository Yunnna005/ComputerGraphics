using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeX
{
    internal int start = -1;
    internal int end = -1;
    public RangeX() {  }

    public void AddPoint(int x)
    {
        if(start < 0)
        {
            start = x;
        }
        else
        {
            if(start < x)
            {
                end = x;
            }
            else
            {
                end = start;
                start = x;
            }
        }
    }
}
