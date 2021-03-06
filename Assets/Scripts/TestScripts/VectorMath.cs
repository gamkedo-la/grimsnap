﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMath
{
    public static class VectorMath
    {
        public static float DistanceAbs(Vector3 vec1, Vector3 vec2)
        {
            return Mathf.Abs(Vector3.Distance(vec1, vec2));
        }

        /// <summary>
        /// if distance is greater than distance between vec1 and vec2 this will return true
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static bool IsWithinDistance(float distance, Vector3 vec1, Vector3 vec2)
        {
            return distance > DistanceAbs(vec1, vec2);
        }

        public static bool IsAroundAtPoint(Vector3 selfVector, Vector3 point, float number)
        {
            return (selfVector.x <= point.x + number && selfVector.z <= point.z + number &&
                 selfVector.x >= point.x - number && selfVector.z >= point.z - number);
           
        }
    }
}
