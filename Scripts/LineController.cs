using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(LineController))]
public class LineController : MonoBehaviour
{
    [SerializeField] List<Transform> nodes;
    LineRenderer lr;
    [SerializeField] PolygonCollider2D polygonCollider;

    private float maxDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = nodes.Count;
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPositions(nodes.ConvertAll(n => n.position - new Vector3(0, 0, 5)).ToArray());
        //Deseable the Line if the distance is to long.
        float distance = Vector2.Distance(nodes[0].position, nodes[1].position);
        if (distance <= maxDistance)
        {
            lr.enabled = true;
            lr.SetPosition(0, nodes[0].position);
            lr.SetPosition(1, nodes[1].position);
            polygonCollider.enabled = true;
        }
        else
        {
            lr.enabled = false;
            polygonCollider.enabled = false;
        }

    }

    public Vector3[] GetPositions() {
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        return positions;
    }

    public float GetWidth() {
        return lr.startWidth;
    }
}


    