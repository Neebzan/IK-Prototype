using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InverseKinematics : MonoBehaviour
{
    public int NumberOfSegments = 3;
    public float SegmentLength = 1.0f;
    public float SegmentWidth = .25f;
    public Segment SegmentPrefab;
    public Transform Target;
    public bool IsAttached = false;
    public Transform attachmentPoint;
    public bool UsingPole;
    public Transform Pole;
    public bool useGravity;


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
        segments = new Segment[NumberOfSegments];
        for (int i = 0; i < NumberOfSegments; i++) {
            segments[i] = Instantiate(SegmentPrefab).GetComponent<Segment>();
            segments[i].Thickness = SegmentWidth;
            segments[i].Length = SegmentLength;

            if (useGravity)
                segments [ i ].gameObject.AddComponent<Rigidbody>().useGravity = true;

            if (i == 0)
                segments[i].Target = Target;

            else
                segments[i].Target = segments[i - 1].transform;

        }
    }


    void Reach()
    {
        for (int i = 0; i < NumberOfSegments; i++) {
            if (IsAttached && i == NumberOfSegments - 1) {
                segments[i].FollowTarget();
                segments[i].transform.position = attachmentPoint.position;
            }
            else
                segments[i].FollowTarget();
        }


        if (IsAttached) {
            for (int i = NumberOfSegments - 1; i >= 0; i--) {
                if (i != NumberOfSegments - 1) {
                    segments[i].transform.position = segments[i + 1].EndPoint();
                }
            }
        }

        //if (UsingPole) {
        //    //Get the root of the IK
        //    Segment root = segments[NumberOfSegments - 1];
        //    //Get the grabbing segment
        //    Segment grabber = segments[0];

        //    //Generate normal from root to grabber
        //    Vector3 normal = grabber.transform.position - root.transform.position;

        //    //Generate plane from normal and position of root
        //    Plane plane = new Plane(normal, root.transform.position);

        //    //Project pole location onto plane
        //    Vector3 projectedPole = plane.ClosestPointOnPlane(Pole.position);

        //    //for (int i = NumberOfSegments - 1; i >= 0; i--) {
        //    //    if (i != NumberOfSegments - 1) {
        //    //        //Project segment 
        //    //        float angle = Vector3.SignedAngle()
        //    //        segments[i].transform.position = segments[i + 1].EndPoint();
        //    //    }
        //    //}
        //}
    }
}

