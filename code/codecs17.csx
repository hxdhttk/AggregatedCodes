using System;
using System.Collections.Generic;

public class Solution
{
    public double LargestTriangleArea (int[][] points)
    {
        var areas = new List<double> ();
        var pointCount = points.GetUpperBound (0);
        for (var i = 0; i <= pointCount; i++)
        {
            for (var j = i + 1; j <= pointCount; j++)
            {
                for (var k = j + 1; k <= pointCount; k++)
                {
                    var d1 = Distance (points[i], points[j]);
                    var d2 = Distance (points[j], points[k]);
                    var d3 = Distance (points[i], points[k]);
                    if (d1 + d2 > d3)
                    {
                        areas.Add (TriangleAreaHeron (d1, d2, d3));
                    }
                }
            }
        }

        return areas.Count == 0 ? 0 : areas.Max ();
    }

    private double Distance (int[] p1, int[] p2)
    {
        return Math.Sqrt ((p1[0] - p2[0]) * (p1[0] - p2[0]) + (p1[1] - p2[1]) * (p1[1] - p2[1]));
    }

    private double TriangleAreaHeron (double d1, double d2, double d3)
    {
        var s = (d1 + d2 + d3) / 2;
        return Math.Sqrt (s * (s - d1) * (s - d2) * (s - d3));
    }
}