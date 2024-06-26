using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineController),typeof(PolygonCollider2D))]
public class LineCollision : MonoBehaviour
{
    public float currentDamage = 50f;
    //The Line Manager Class
    LineController lc;

    //The collider for the line
    PolygonCollider2D polygonCollider2D;

    //The points to draw a collision shape between
    List<Vector2> colliderPoints = new List<Vector2>(); 

    void Start()
    {
        lc = GetComponent<LineController>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }


    void Update()
    {
        colliderPoints = CalculateColliderPoints();
        polygonCollider2D.SetPath(0, colliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        if (colliderPoints != null) colliderPoints.ForEach(p => Gizmos.DrawSphere(p, 0.1f));
    }

    private List<Vector2> CalculateColliderPoints() {
        // Get All positions on the line renderer
        Vector3[] positions = lc.GetPositions();

        // Get the Width of the Line
        float width = lc.GetWidth();

        // Calculate the slope (m)
        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);

        // Clamping values to prevent excessive spreading
        float maxDelta = width / 4f;

        float deltaX = Mathf.Clamp((width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f)), -maxDelta, maxDelta);
        float deltaYUpper = Mathf.Clamp((width / 4f) * (1 / Mathf.Pow(1 + m * m, 0.5f)), -maxDelta, maxDelta); // Reduced deltaY for upper points
        float deltaYLower = Mathf.Clamp((width / 8f) * (1 / Mathf.Pow(1 + m * m, 0.5f)), -maxDelta, maxDelta); // Further reduced deltaY for lower points

        // Calculate the Offset from each point to the collision vertex
        Vector3[] offsetsUpper = new Vector3[2];
        Vector3[] offsetsLower = new Vector3[2];
        offsetsUpper[0] = new Vector3(-deltaX, deltaYUpper);
        offsetsUpper[1] = new Vector3(deltaX, -deltaYUpper);
        offsetsLower[0] = new Vector3(-deltaX, deltaYLower);
        offsetsLower[1] = new Vector3(deltaX, -deltaYLower);

        // Generate the Colliders Vertices
        List<Vector2> colliderPositions = new List<Vector2> {
        positions[0] + offsetsUpper[0],
        positions[1] + offsetsUpper[0],
        positions[1] + offsetsLower[1],
        positions[0] + offsetsLower[1]
    };

        return colliderPositions;
    }




    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(currentDamage);
        }
    }
}
