using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] Rigidbody2D playerRb;
    public LineRenderer lr;
    public float distance = 1.0f;
    public float smoothTime = 1.0f;
    public int numPoints = 3;
    public Vector3 originOffset;
    public Vector3[] points;
    public Vector3[] pointsVel;

    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[numPoints];
        pointsVel = new Vector3[numPoints];
        for(int i = 0; i < numPoints; i++) {
            points[i] = transform.parent.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        points[0] = transform.position + new Vector3(originOffset.x * controller.direction, originOffset.y, originOffset.z);

        for(int i = 1; i < numPoints; i++) {
            points[i] = Vector3.SmoothDamp(points[i], points[i - 1] + (transform.up * distance * -1), ref pointsVel[i], smoothTime);
        }
        lr.SetPositions(points);
    }

    public void Reset() {
        for(int i = 0; i < numPoints; i++) {
            points[i] = transform.position + new Vector3(originOffset.x * controller.direction, originOffset.y, originOffset.z);
        }
    }
}
