using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier
{
    public static readonly float DELTA = 0.01f;

    private Vector3[] points;

    public Vector3 p0 { get => points[0]; }
    public Vector3 p1 { get => points[1]; }
    public Vector3 p2 { get => points[2]; }
    public Vector3 p3 { get => points[3]; }

    private Vector3[] derivativeVectors;

    public Bezier(Vector3[] points)
    {
        this.points = points;
        CalculateDerivativeVectors();
    }

    public static Bezier CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return CubicBezier(new Vector3[] { p0, p1, p2, p3 });
    }
    
    public static Bezier CubicBezier(Vector3[] points)
    {
        if (points.Length != 4)
            throw new System.Exception("Invalid parameters, need 4 points");
        return new Bezier(points);
    }

    public Vector3 Calculate(float t)
    {
        // help : https://www.gamasutra.com/blogs/VivekTank/20180806/323709/How_to_work_with_Bezier_Curve_in_Games_with_Unity.php
        t = Mathf.Clamp(t, 0, 1);
        return Mathf.Pow(1 - t, 3) * p0 +
            3 * Mathf.Pow(1 - t, 2) * t * p1 +
            3 * (1 - t) * Mathf.Pow(t,2)  * p2 +
            Mathf.Pow(t, 3) * p3;
    }

    public Vector3 CalculateForward(float t)
    {
        var p0 = Calculate(t - DELTA);
        var p1 = Calculate(t + DELTA);
        return p1 - p0;
    }

    public float CalculateTByLength(float t, float length)
    {
        return t + length / (t * t * derivativeVectors[0] + t * derivativeVectors[1] + derivativeVectors[2]).magnitude;
    }

    private void CalculateDerivativeVectors()
    {
        // help : https://gamedev.stackexchange.com/questions/27056/how-to-achieve-uniform-speed-of-movement-on-a-bezier-curve
        derivativeVectors = new Vector3[points.Length - 1];
        derivativeVectors[0] = -3 * p0 + 9 * p1 - 9 * p2 + 3 * p3;
        derivativeVectors[1] = 6 * p0 - 12 * p1 + 6 * p2;
        derivativeVectors[2] = -3 * p0 + 3 * p1;
    }
}
