using UnityEngine;

public static class Utilities
{
    private const float MinDistance = 0.1f;

    public static bool IsCloseTo(this Vector3 positionA, Vector3 positionB, float distance = MinDistance)
    {
        return (positionA - positionB).sqrMagnitude < (distance * distance);
    }

    public static bool IsCloseToXZ(this Vector3 positionA, Vector3 positionB, float distance = MinDistance)
    {
        positionA.y = 0;
        positionB.y = 0;

        return IsCloseTo(positionA, positionB, distance);
    }
}
