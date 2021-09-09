using UnityEngine;

public static class Bezier
{
    public static Vector3 GetPoint(Vector3[] points, float t)
    {
        if (points.Length == 4)
        {
            t = Mathf.Clamp01(t);
            return
                (1f - t) * (1f - t) * (1f - t) * points[0] +
                3f * (1f - t) * (1f - t) * t * points[1] +
                3f * (1f - t) * t * t * points[2] +
                t * t * t * points[3];
        }
        else return Vector3.zero;
    }

    public static Vector3 GetFirstDerivative(Vector3[] points, float t)
    {
        if (points.Length == 4)
        {
            t = Mathf.Clamp01(t);
            return
                3f * (1f - t) * (1f - t) * (points[1] - points[0]) +
                6f * (1f - t) * t * (points[2] - points[1]) +
                3f * t * t * (points[3] - points[2]);
        }
        else return Vector3.zero;
    }

}
