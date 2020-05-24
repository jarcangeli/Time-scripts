using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCircularEdgeCollider : MonoBehaviour
{
    // use local scale to define 2d edge collider points
    [SerializeField]
    EdgeCollider2D edgeCollider = null;
    [SerializeField]
    SpriteRenderer edgeSprite = null;
    float xScale, yScale;
    public float edgeWidth = 0.01f; // should be set to the width of the sprite border
    public int nSegments = 12;

    void Start()
    {
        GetScale();

        edgeCollider.edgeRadius = edgeWidth;
        edgeCollider.points = CreateEdgePoints();   
    }

    void GetScale()
    {
        Debug.Log("Edge sprite bounds: " + edgeSprite.bounds);
        xScale = (edgeSprite.bounds.extents.x - edgeWidth) / 2f;
        yScale = (edgeSprite.bounds.extents.y - edgeWidth) / 2f;
    }

    Vector2[] CreateEdgePoints()
    {
        Vector2[] points = new Vector2[nSegments + 1];

        for (int i = 0; i < nSegments; ++i)
        {
            float ang = 2f * Mathf.PI * i / nSegments;
            points[i] = new Vector2(xScale * Mathf.Cos(ang), yScale * Mathf.Sin(ang));
        }
        points[nSegments] = points[0]; // end = start

        return points;
    }
}
