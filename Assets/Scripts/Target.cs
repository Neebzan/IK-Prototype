using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float speedMultiplier = 1.0f;

    private Vector3 startingPosition;

    public float xScale = 1.0f;
    public float yScale = 1.0f;

    private void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        float xPos = speedMultiplier * (Mathf.PerlinNoise(Time.time * xScale + startingPosition.x, 0f) - .5f);
        float yPos = speedMultiplier * (Mathf.PerlinNoise(Time.time * yScale + startingPosition.x, 0f));
        float zPos = speedMultiplier * (Mathf.PerlinNoise(Time.time * xScale + startingPosition.x, Time.time * yScale + startingPosition.y) - .5f);
        transform.position = new Vector3(xPos, yPos, zPos) + startingPosition;
    }
}
