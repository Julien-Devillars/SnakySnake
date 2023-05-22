using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Trail : MonoBehaviour
{
    public bool mHasBeenUpdated;
    // https://i.redd.it/3fwmgurggi4a1.png
    Color mStartColor = new Color(2/255f, 55f/255f, 136/255f);
    Color mEndColor = new Color(212/255f, 0/255f, 120/255f);
    void Start()
    {
        //gameObject.name = "Trail";
        gameObject.tag = "Trail";
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Need a ball to have a trail
        GameObject ball = GameObject.Find(Utils.CHARACTER);
        Transform transform = ball.transform;

        // Line color65
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
        //Material red_mat = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material = (Material)Resources.Load("Materials/Trail", typeof(Material)); ;
        //red_mat.SetColor("_Color", line_color);
        mHasBeenUpdated = false;
    }

    void Update()
    {
        updateTrailPoint();
    }

    public void forceUpdateTrailPoint()
    {
        updateTrailPoint();
    }

    private void updateTrailPoint()
    {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        //For drawing line in the world space, provide the x,y,z values

        GameObject points = GameObject.Find("Trail Points");
        GameObject end_point_go = GameObject.Find("Ball");

        int nb_points = points.transform.childCount;

        string[] trail_name_splitted = gameObject.transform.name.Split(' ');
        if (trail_name_splitted.Length != 2)
        {
            Debug.Log("Trail name is wrong : " + gameObject.transform.name);
            return;
        }

        int trail_number = int.Parse(trail_name_splitted[1]);

        Vector3 position_end;
        Vector3 position_start;
        if (trail_number == nb_points - 1)
        {
            position_start = points.transform.GetChild(nb_points - 1).position;
            position_end = end_point_go.transform.position;
        }
        else
        {
            position_start = points.transform.GetChild(trail_number).position;
            position_end = points.transform.GetChild(trail_number + 1).position;
        }

        // Change the line color with a gradient, unfortunately we have to make it smart because we have multiple line renderer
        float color_gradient_total_size = 0f;
        float color_gradient_start = 0f;
        float color_gradient_end = 0f;
        for (int i = 0; i < nb_points; ++i)
        {
            Vector3 point_1 = points.transform.GetChild(i).position;
            Vector3 point_2;
            if (i != nb_points -1)
            {
                point_2 = points.transform.GetChild(i + 1).position;
            }
            else
            {
                point_2 = end_point_go.transform.position;
            }
            if (trail_number == i)
            {
                color_gradient_start = color_gradient_total_size;
            }
            color_gradient_total_size += Vector3.Distance(point_1, point_2);
            if (trail_number == i)
            {
                color_gradient_end = color_gradient_total_size;
            }
        }

        float scale_color_gradient_start = color_gradient_start / color_gradient_total_size;
        float scale_color_gradient_end = color_gradient_end / color_gradient_total_size;

        //Debug.Log("Trail Number : " + trail_number + " -> " + color_gradient_start + " ("+ scale_color_gradient_start +") - " + color_gradient_end + " (" + scale_color_gradient_end + ") / " + color_gradient_total_size);
        Gradient gradient = new Gradient();
        Color start_color = Color.Lerp(mStartColor, mEndColor, scale_color_gradient_start);
        Color end_color = Color.Lerp(mStartColor, mEndColor, scale_color_gradient_end);
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(start_color, 0.0f), new GradientColorKey(end_color, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

        lineRenderer.SetPosition(0, position_start); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, position_end); //x,y and z position of the end point of the line
        lineRenderer.enabled = true;

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
        mHasBeenUpdated = true;
    }

    private void OnDestroy()
    {
        //Debug.Log("Trail deleted");
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == Utils.CHARACTER)
        {
            Transform trail = GameObject.Find(Utils.TRAILS_STR).transform;
            Transform trail_child = trail.GetChild(trail.childCount - 1);
            if (transform != trail_child)
            {
                
                CharacterBehavior character = collider.gameObject.GetComponent<CharacterBehavior>();

                GameControler.status = GameControler.GameStatus.Lose;
                if (Utils.HAS_LOSE)
                {
                    SceneManager.LoadScene("Level_1");
                }
            }
        }
    }

}
