using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trail : MonoBehaviour
{
    void Start()
    {
        gameObject.name = "Trail";
        gameObject.tag = "Trail";
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Need a ball to have a trail
        GameObject ball = GameObject.Find("Ball");
        Transform transform = ball.transform;

        // Line color
        Color line_color = Color.red;
        lineRenderer.startColor = line_color;
        lineRenderer.endColor = line_color;
        // Line width
        lineRenderer.startWidth = transform.localScale.x;
        lineRenderer.endWidth = transform.localScale.x;
        // Set 2 points
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        // Create Box collider
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(transform.localScale.x, transform.localScale.x);
        collider.offset = new Vector2(transform.position.x, transform.position.y);

        // Set Material
        Material red_mat = new Material(Shader.Find("Sprites/Default"));
        red_mat.SetColor("_Color", line_color);
        lineRenderer.material = red_mat;
    }

    void Update()
    {
        GameObject end_point_go = GameObject.Find("Ball");
        Vector3 position_end = end_point_go.transform.position;
        GameObject start_point_go = GameObject.Find("start_point");
        Vector3 position_start = start_point_go.transform.position;

        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.SetPosition(0, position_start); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, position_end); //x,y and z position of the end point of the line

        Vector3 middle_point = (position_start + position_end) / 2f;
        // Update Box collider
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        collider.offset = new Vector2(middle_point.x, middle_point.y);
        Direction.direction current_direction = Direction.getDirectionFrom2Points(position_start, position_end);

        if (current_direction == Direction.Up)
        {
            collider.size = new Vector2(collider.size.x, (position_end.y - middle_point.y) * 2f);
        }
        if (current_direction == Direction.Down)
        {
            collider.size = new Vector2(collider.size.x, (middle_point.y - position_end.y) * 2f);
        }
        if (current_direction == Direction.Left)
        {
            collider.size = new Vector2((middle_point.x - position_end.x) * 2f, collider.size.y);
        }
        if (current_direction == Direction.Right)
        {
            collider.size = new Vector2((position_end.x - middle_point.x) * 2f, collider.size.y);
        }
    }
}
