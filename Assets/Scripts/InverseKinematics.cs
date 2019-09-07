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


    private Segment [ ] segments;

    // Start is called before the first frame update
    void Start () {
        Setup();
    }

    // Update is called once per frame
    void Update () {
        Reach();
    }

    /// <summary>
    /// Runs once
    /// </summary>
    void Setup () {
        segments = new Segment [ NumberOfSegments ];
        for (int i = 0; i < NumberOfSegments; i++) {
            segments [ i ] = Instantiate(SegmentPrefab).GetComponent<Segment>();
            segments [ i ].Thickness = SegmentWidth;
            segments [ i ].Length = SegmentLength;

            if (useGravity)
                segments [ i ].gameObject.AddComponent<Rigidbody>().useGravity = true;

            if (i == 0)
                segments [ i ].Target = Target;

            else
                segments [ i ].Target = segments [ i - 1 ].transform;

        }
    }


    void Reach () {
        for (int i = 0; i < NumberOfSegments; i++) {
            if (IsAttached && i == NumberOfSegments - 1) {
                segments [ i ].FollowTarget();
                segments [ i ].transform.position = attachmentPoint.position;
            }
            else
                segments [ i ].FollowTarget();
        }


        if (IsAttached) {
            for (int i = NumberOfSegments - 2; i >= 0; i--) {
                segments [ i ].transform.position = segments [ i + 1 ].EndPoint();
            }
        }

        if (UsingPole) {
            for (int i = 0; i < NumberOfSegments - 1; i++) {
                Vector3 selectedSegmentPosition;
                Vector3 segmentTowardsGrabPosition;
                Vector3 segmentTowardsRootPosition;

                //Set the correct segment locations
                if (i == 0) {
                    selectedSegmentPosition = segments [ i ].transform.position;
                    segmentTowardsGrabPosition = Target.transform.position;
                    segmentTowardsRootPosition = segments [ i + 1 ].transform.position;
                }
                else {
                    selectedSegmentPosition = segments [ i ].transform.position;
                    segmentTowardsGrabPosition = segments [ i - 1 ].transform.position;
                    segmentTowardsRootPosition = segments [ i + 1 ].transform.position;
                }

                //Calculate the normal between the segment before and segment after
                Vector3 normal = (segmentTowardsGrabPosition - segmentTowardsRootPosition).normalized;
                //Create a plane from the segment before, and the calculated normal
                Plane plane = new Plane(normal, segmentTowardsRootPosition);
                //Project the selected segment and pole onto the plane
                Vector3 projectedSegment = plane.ClosestPointOnPlane(selectedSegmentPosition);
                Vector3 projectedPole = plane.ClosestPointOnPlane(Pole.position);
                //Calculate the angle of rotation needed to get the position closest to the pole
                float angle = Vector3.SignedAngle(projectedSegment - segmentTowardsRootPosition, projectedPole - segmentTowardsRootPosition, plane.normal);
                segments [ i ].transform.position = Quaternion.AngleAxis(angle, plane.normal) * (segments [ i ].transform.position - segmentTowardsGrabPosition) + segmentTowardsGrabPosition;

            }
        }
    }
}

