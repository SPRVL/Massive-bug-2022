using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCurve : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [Range(0,1)][SerializeField] float test;
    [SerializeField] float vertical;

    private void Update()
    {
        vertical = curve.Evaluate(test);
    }
    /*
    private Vector2 BerzierPoint(Keyframe k1, Keyframe k2)
    {
        
    }

    private Vector2 LinearPoint(Vector2 start, Vector2 end, float t)
    {
        t = Mathf.Clamp(t, 0, 1f);
        return Vector2.Lerp(start, end,t);
    }*/
}
