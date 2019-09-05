using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public float Length;
    public float Thickness;
    public float Angle;
    private Color color;
    public Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(Thickness, Thickness, Length);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.position + transform.forward * 10.0f);
    }

    public void FollowTarget()
    {
        Vector3 between = Target.transform.position - transform.position;
        float angleBetween = Vector3.Angle(transform.forward, Target.transform.position);
        Vector3 newDir = Vector3.RotateTowards(transform.forward, between, angleBetween * .5f, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
        transform.position = Target.position + between.normalized * -1 * Length;

    }

    public Vector3 EndPoint()
    {
        return transform.position + transform.forward * Length;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(EndPoint(), 0.1f);
    //}
}
