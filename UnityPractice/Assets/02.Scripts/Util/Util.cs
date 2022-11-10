using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static Vector2 TransWorldPositionToCanvasPostion(Vector2 _worldPostion, Vector2 _canvasSizeDelta)
    {
        Vector2 newPos = Camera.main.WorldToViewportPoint(_worldPostion);
        newPos.x *= _canvasSizeDelta.x;
        newPos.y *= _canvasSizeDelta.y;

        return newPos;
    }

    public static Vector2 TransWorldPositionToCanvasPostion(Vector2 _worldPostion, Vector2 _canvasSizeDelta, Vector2 _anchor)
    {
        Vector2 newPos = Camera.main.WorldToViewportPoint(_worldPostion);
        newPos.x *= _canvasSizeDelta.x;
        newPos.y *= _canvasSizeDelta.y;

        newPos.x -= _canvasSizeDelta.x * _anchor.x;
        newPos.y -= _canvasSizeDelta.y * _anchor.y;

        return newPos;
    }

    public static Vector3 GetBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1f - t;
        float t2 = t * t;
        float u2 = u * u;

        Vector3 result =
            (u2) * p0 +
            (2f * u * t) * p1 +
            (t2) * p2;
        return result;
    }
    public static Vector3 GetBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1f - t;
        float t2 = t * t;
        float u2 = u * u;
        float u3 = u2 * u;
        float t3 = t2 * t;

        Vector3 result =
            (u3) * p0 +
            (3f * u2 * t) * p1 +
            (3f * u * t2) * p2 +
            (t3) * p3;
        return result;
    }
}
