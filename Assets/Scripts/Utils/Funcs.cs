using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Funcs 
{
    public static float DistanceIgnoreY(Vector3 sour, Vector3 dest)
    {
        float x = sour.x - dest.x;
        float y = sour.z - dest.z;

        return x * x + y * y;
    }
}
