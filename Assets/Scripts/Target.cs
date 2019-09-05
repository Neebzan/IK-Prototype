using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float speedMultiplier = 1.0f;

    public float xScale = 1.0f;
    public float yScale = 1.0f;

    void Update()
    {
        float xPos = speedMultiplier * (Mathf.PerlinNoise(Time.time * xScale, 0f) - .5f);
        float yPos = speedMultiplier * (Mathf.PerlinNoise(Time.time * yScale, 0f));
        float zPos = speedMultiplier * (Mathf.PerlinNoise(Time.time * xScale, Time.time * yScale) - .5f);
        transform.position = new Vector3(xPos, yPos, zPos);
    }
}
