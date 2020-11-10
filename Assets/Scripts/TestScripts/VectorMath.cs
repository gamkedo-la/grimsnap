using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorMath 
{
    public static float DistanceAbs(Vector3 vec1, Vector3 vec2) 
    {
        return Mathf.Abs(Vector3.Distance(vec1, vec2));
    }
}
