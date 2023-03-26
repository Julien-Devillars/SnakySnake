using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border
{
    Background mBackground;
    public GameObject mBorder;
    Vector3 mStartPoint;
    Vector3 mEndPoint;
    
    public Border(Background _background, Vector3 start_point, Vector3 end_point)
    {
        mBackground = _background;
        mStartPoint = start_point;
        mEndPoint = end_point;
        mBorder = new GameObject("Border");

        addLineRenderer();
        addTag();

        addCollider2D();

        mBorder.transform.parent = GameObject.Find("Borders").transform;

        mBorder.name = mBorder.name.Replace("(Clone)", "");
    }
    public void setName(string new_name)
    {
        mBorder.name = new_name;
    }
    private void addLineRenderer()
    {
        //For creating line renderer object
        mBorder.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = mBorder.GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        GameObject ball = GameObject.Find("Ball");
        lineRenderer.startWidth = ball.transform.localScale.x;
        lineRenderer.endWidth = ball.transform.localScale.x;

        lineRenderer.SetPosition(0, mStartPoint);
        lineRenderer.SetPosition(1, mEndPoint);

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material.color = Color.white;
    }
    public bool checkColliderAlreadyExist()
    {
        GameObject border_parent = GameObject.Find("Borders");
        for (int i = 0; i < border_parent.transform.childCount; ++i)
        {
            GameObject border_child = border_parent.transform.GetChild(i).gameObject;
            LineRenderer border_child_line = border_child.GetComponent<LineRenderer>();
            BoxCollider2D border_child_collider = border_child.GetComponent<BoxCollider2D>();
            Vector3 line_start_point = border_child_line.GetPosition(0);
            Vector3 line_end_point = border_child_line.GetPosition(1);
            if(border_child == mBorder)
            {
                continue;
            }
            if ((line_start_point == mStartPoint && line_end_point == mEndPoint) || (line_start_point == mEndPoint && line_end_point == mStartPoint) && border_child_collider.enabled == true)
            {
                return true;
            }
        }
        return false;
    }
    private void addCollider2D()
    {
        //bool hide_collider = false;// checkColliderAlreadyExist();
        mBorder.AddComponent<BoxCollider2D>();
        BoxCollider2D box_collider = mBorder.GetComponent<BoxCollider2D>();

        LineRenderer lineRenderer = mBorder.GetComponent<LineRenderer>();
        if(mBorder.tag == "VerticalBorder")
        {
            box_collider.size = new Vector2(lineRenderer.startWidth, Mathf.Abs(mEndPoint.y - mStartPoint.y));
        }
        else if (mBorder.tag == "HorizontalBorder")
        {
            box_collider.size = new Vector2(Mathf.Abs(mEndPoint.x - mStartPoint.x)  /*box_collider.size.x*/, lineRenderer.startWidth);
        }
        //box_collider.enabled = !hide_collider;
    }

    private void addTag()
    {
        if (mStartPoint.x == mEndPoint.x)
        {
            mBorder.tag = "VerticalBorder";
        }
        else if (mStartPoint.y == mEndPoint.y)
        {
            mBorder.tag = "HorizontalBorder";
        }
        else
        {
            Debug.Log("Issue setting tag for border.");
        }
    }

    public void Destroy()
    {
        /*
        GameObject border_parent = GameObject.Find("Borders");
        List<LineRenderer> borders_checked = new List<LineRenderer>();
        for (int i = 0; i < border_parent.transform.childCount; ++i)
        {
            GameObject border_child = border_parent.transform.GetChild(i).gameObject;
            LineRenderer border_child_line = border_child.GetComponent<LineRenderer>();
            Vector3 line_start_point = border_child_line.GetPosition(0);
            Vector3 line_end_point = border_child_line.GetPosition(1);
            if ((line_start_point == mStartPoint && line_end_point == mEndPoint) || (line_start_point == mEndPoint && line_end_point == mStartPoint))
            {
                bool is_already_checked = false;
                foreach(LineRenderer line_renderer_checked in borders_checked)
                {
                    Vector3 line_checked_start_point = line_renderer_checked.GetPosition(0);
                    Vector3 line_checked_end_point = line_renderer_checked.GetPosition(1);
                    if ((line_checked_start_point == mStartPoint && line_checked_end_point == mEndPoint) || (line_checked_start_point == mEndPoint && line_checked_end_point == mStartPoint))
                    {
                        is_already_checked = true;
                        break;
                    }
                }
                if(is_already_checked)
                {
                    continue;
                }
                Collider2D border_child_collider = border_child.GetComponent<Collider2D>();
                border_child_collider.enabled = true;
                borders_checked.Add(border_child_line);
            }
        }*/
        BoxCollider2D border_child_collider = mBorder.GetComponent<BoxCollider2D>();
        border_child_collider.enabled = false;
        GameObject.DestroyImmediate(mBorder);
    }
    public bool equals(Border other_border)
    {
        return (mStartPoint == other_border.mStartPoint && mEndPoint == other_border.mEndPoint) || (mStartPoint == other_border.mEndPoint && mEndPoint == other_border.mStartPoint);
    }
    public void hideColliderBorder()
    {
        BoxCollider2D box_collider = mBorder.GetComponent<BoxCollider2D>();
        LineRenderer lineRenderer = mBorder.GetComponent<LineRenderer>();
        if (mBorder.tag == "VerticalBorder")
        {
            box_collider.size = new Vector2(lineRenderer.startWidth, box_collider.size.y);
        }
        else if (mBorder.tag == "HorizontalBorder")
        {
            box_collider.size = new Vector2(box_collider.size.x, lineRenderer.startWidth);
        }
        box_collider.enabled = false;
    }
}
