using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InverseKinematics : MonoBehaviour
{
    public int numberOfSegments = 3;
    public float segmentLength = 1.0f;
    public float segmentWidth = .25f;
    public Transform Target;
    public Segment SegmentPrefab;
    public bool isAttached = false;
    public Transform attachmentPoint;

    private Segment[] segments;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        Reach();
    }

    void Setup()
    {
        segments = new Segment[numberOfSegments];
        for (int i = 0; i < numberOfSegments; i++)
        {
            segments[i] = Instantiate(SegmentPrefab).GetComponent<Segment>();
            segments[i].Thickness = segmentWidth;
            segments[i].Length = segmentLength;
            if (i == 0)
                segments[i].Target = Target;

            else
                segments[i].Target = segments[i - 1].transform;

        }
    }


    void Reach()
    {
        for (int i = 0; i < numberOfSegments; i++)
        {
            if (isAttached && i == numberOfSegments - 1)
            {
                segments[i].FollowTarget();
                segments[i].transform.position = attachmentPoint.position;
            }
            else
                segments[i].FollowTarget();
        }


        if (isAttached)
        {
            for (int i = numberOfSegments - 1; i >= 0; i--)
            {
                if (i != numberOfSegments - 1)
                {
                    segments[i].transform.position = segments[i + 1].EndPoint();
                }
            }
        }
    }
}

