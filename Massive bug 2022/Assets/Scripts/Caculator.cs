using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caculator
{
    //degree from 0 to 360
    public static Vector2 NormalizedDirection2D(float angleInDegree)
    {
        float angleInRad = angleInDegree * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad));
    }
    public static float Vector2ToAngleInDegree(Vector2 inputVector)
    {
        float returnValue = Mathf.Acos(inputVector.normalized.x) * Mathf.Rad2Deg;
        if (inputVector.y < 0) returnValue = 360 - returnValue;
        return returnValue;
    }
    public static float AngleOf2VectorInDegree(Vector2 v1, Vector2 v2)
    {
        float angle = Mathf.Abs(Vector2ToAngleInDegree(v1) - Vector2ToAngleInDegree(v2));
        if (angle > 180) angle = Mathf.Abs(angle - 360);
        return angle;
    }
}
