using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trail : MonoBehaviour
{
    void Start()
    {
        //gameObject.name = "Trail";
        gameObject.tag = "Trail";
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Need a ball to have a trail
        GameObject ball = GameObject.Find(Utils.CHARACTER);
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
        lineRenderer.numCapVertices = 8;
        // Hide linerenderer until update
        lineRenderer.enabled = false;

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
        Vector3 position_end = new Vector3();
        Vector3 position_start = new Vector3();

        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        //For drawing line in the world space, provide the x,y,z values

        GameObject points = GameObject.Find("Trail Points");
        GameObject start_point_go = GameObject.Find("start_point");
        GameObject end_point_go = GameObject.Find("Ball");

        int nb_points = points.transform.childCount;

        string[] trail_name_splitted = gameObject.transform.name.Split(' ');
        if(trail_name_splitted.Length != 2)
        {
            Debug.Log("Trail name is wrong : " + gameObject.transform.name);
            return;
        }

        int trail_number = int.Parse(trail_name_splitted[1]);

        if(trail_number == nb_points - 1)
        {
            position_start = points.transform.GetChild(nb_points - 1).position;
            position_end = end_point_go.transform.position;
        }
        else
        {
            position_start = points.transform.GetChild(trail_number).position;
            position_end = points.transform.GetChild(trail_number + 1).position;
        }


        lineRenderer.SetPosition(0, position_start); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, position_end); //x,y and z position of the end point of the line
        lineRenderer.enabled = true ;

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

    private void OnDestroy()
    {
        //Debug.Log("Trail deleted");
    }
}
