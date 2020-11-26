using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Episode4 : MonoBehaviour
{
    public int frameCount;

    void Start()
    {
        float dst = GetDistanceBetweenTwoPoints(0, 0, 3, 4);
        //print(dst);
        //print("butt face");
    }

    void Update()
    {
        frameCount++;
        //print("Frames: " + frameCount);
    }

    public float GetDistanceBetweenTwoPoints(float x1, float y1, float x2, float y2)
    {
        float dX = x2 - x1;
        float dY = y2 - y1;
        float dstSquared = dX * dX + dY * dY;
        float dst = Mathf.Sqrt(dstSquared);
        return dst;
    }
}
