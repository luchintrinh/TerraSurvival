using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform startPoint;
    public Transform controlPoint;
    public Transform endPoint;
    public float duration = 5f;

    private float startTime;
    private Vector3 oldPosition;

    void Start()
    {
        startTime = Time.time;
        oldPosition = startPoint.position;
    }

    void Update()
    {
        float t = (Time.time - startTime) / duration;
        Vector3 newPosition = CalculateQuadraticBezierPoint(t, startPoint.position, controlPoint.position, endPoint.position);

        if (Time.time > startTime) // Ensure we've moved before calculating direction
        {
            Vector3 movementDirection = (newPosition - oldPosition).normalized;
            float rotationAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
        }

        transform.position = newPosition;
        oldPosition = newPosition;
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * p0 + 2 * u * t * p1 + tt * p2;
        return point;
    }
}
