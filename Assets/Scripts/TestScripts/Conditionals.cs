using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Conditionals
{
    public static bool IsTargetOutOfRangeAndAlive(float distanceToStop, Health target, Vector3 selfVector)
    {
        //if (!target.isDead && !VectorMath.WithinDistance(meleeRange, transform.position, raycastHit.transform.position))
        if (!target.isDead && !VectorMath.WithinDistance(distanceToStop, selfVector, target.transform.position))
            return true;
        else return false;
    }

    public static bool IsAroundAtPoint(Vector3 selfVector, Vector3 point, float number)
    {
        if (selfVector.x <= point.x + number && selfVector.z <= point.z + number &&
            selfVector.x >= point.x - number && selfVector.z >= point.z - number)
        {
            return false;
        }
        else return true;
    }
}
