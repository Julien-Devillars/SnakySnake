using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public Vector3 mStartPoint;
    public Vector3 mEndPoint;
    public Border mDuplicateBorder;
    public bool mHasError;
    public bool mNewBorderOnDelete = false;
    private LineRenderer mLineRenderer;

    public void Awake()
    {
        gameObject.name = "Border";
        gameObject.name = gameObject.name.Replace("(Clone)", "");
        gameObject.transform.parent = GameObject.Find("Borders").transform;
    }

    public void init(Vector3 start_point, Vector3 end_point)
    {
        mStartPoint = start_point;
        mEndPoint = end_point;

        addLineRenderer();
        addTag();

        addCollider2D();
        
        mDuplicateBorder = null;
        mHasError = false;
    }
    public void setName(string new_name)
    {
        gameObject.name = new_name;
    }
    private void addLineRenderer()
    {
        //For creating line renderer object
        gameObject.AddComponent<LineRenderer>();
        mLineRenderer = gameObject.GetComponent<LineRenderer>();
        mLineRenderer.startColor = Color.blue;
        mLineRenderer.endColor = Color.blue;
        mLineRenderer.positionCount = 2;
        mLineRenderer.useWorldSpace = true;
        mLineRenderer.numCapVertices = 8;

        GameObject ball = GameObject.Find(Utils.CHARACTER);
        mLineRenderer.startWidth = ball.transform.localScale.x;
        mLineRenderer.endWidth = ball.transform.localScale.x;

        mLineRenderer.SetPosition(0, mStartPoint);
        mLineRenderer.SetPosition(1, mEndPoint);
        mLineRenderer.startColor = Color.white;
        mLineRenderer.endColor = Color.white;

        if(Utils.SHADER_ON)
        {
            mLineRenderer.material = (Material)Resources.Load("Shaders/GlowBorder", typeof(Material));
            mLineRenderer.material.color = Color.white;
        }
        else
        {
            mLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            mLineRenderer.material.color = Color.black;
        }
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
            if(border_child == gameObject)
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
        BoxCollider2D box_collider = gameObject.AddComponent<BoxCollider2D>();

        if(isVertical())
        {
            box_collider.size = new Vector2(mLineRenderer.startWidth, Mathf.Abs(mEndPoint.y - mStartPoint.y));
        }
        else if (isHorizontal())
        {
            box_collider.size = new Vector2(Mathf.Abs(mEndPoint.x - mStartPoint.x) , mLineRenderer.startWidth);
        }
    }

    private void addTag()
    {
        if (mStartPoint.x == mEndPoint.x)
        {
            gameObject.tag = "VerticalBorder";
        }
        else if (mStartPoint.y == mEndPoint.y)
        {
            gameObject.tag = "HorizontalBorder";
        }
        else
        {
            Debug.Log("Issue setting tag for border.");
            mHasError = true;
        }
    }

    public void destroy()
    {
        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        character.mBorders.Remove(this);
        character.mBorderGameObjects.Remove(gameObject);

        GameObject.DestroyImmediate(gameObject);
    }
    public bool equals(Border other_border)
    {
        return (mStartPoint == other_border.mStartPoint && mEndPoint == other_border.mEndPoint) || (mStartPoint == other_border.mEndPoint && mEndPoint == other_border.mStartPoint);
    }
    public bool onSmallFuzzyBorder(Vector3 position)
    {
        return (Utils.fuzzyCompare(position.x, mStartPoint.x) && Utils.fuzzyCompare(position.x, mEndPoint.x) && position.y >= mStartPoint.y - Utils.EPSILON() && position.y <= mEndPoint.y + Utils.EPSILON() && mStartPoint.y < mEndPoint.y)
            || (Utils.fuzzyCompare(position.y, mStartPoint.y) && Utils.fuzzyCompare(position.y, mEndPoint.y) && position.x >= mStartPoint.x - Utils.EPSILON() && position.x <= mEndPoint.x + Utils.EPSILON() && mStartPoint.x < mEndPoint.x)
            || (Utils.fuzzyCompare(position.x, mStartPoint.x) && Utils.fuzzyCompare(position.x, mEndPoint.x) && position.y <= mStartPoint.y + Utils.EPSILON() && position.y >= mEndPoint.y - Utils.EPSILON() && mStartPoint.y > mEndPoint.y)
            || (Utils.fuzzyCompare(position.y, mStartPoint.y) && Utils.fuzzyCompare(position.y, mEndPoint.y) && position.x <= mStartPoint.x + Utils.EPSILON() && position.x >= mEndPoint.x - Utils.EPSILON() && mStartPoint.x > mEndPoint.x);
    }
    public bool onFuzzyBorder(Vector3 position)
    {
        return (Utils.fuzzyCompare(position.x, mStartPoint.x) && Utils.fuzzyCompare(position.x, mEndPoint.x) && position.y >= mStartPoint.y - Utils.EPSILON() * 2f && position.y <= mEndPoint.y + Utils.EPSILON() * 2f && mStartPoint.y < mEndPoint.y)
            || (Utils.fuzzyCompare(position.y, mStartPoint.y) && Utils.fuzzyCompare(position.y, mEndPoint.y) && position.x >= mStartPoint.x - Utils.EPSILON() * 2f && position.x <= mEndPoint.x + Utils.EPSILON() * 2f && mStartPoint.x < mEndPoint.x)
            || (Utils.fuzzyCompare(position.x, mStartPoint.x) && Utils.fuzzyCompare(position.x, mEndPoint.x) && position.y <= mStartPoint.y + Utils.EPSILON() * 2f && position.y >= mEndPoint.y - Utils.EPSILON() * 2f && mStartPoint.y > mEndPoint.y)
            || (Utils.fuzzyCompare(position.y, mStartPoint.y) && Utils.fuzzyCompare(position.y, mEndPoint.y) && position.x <= mStartPoint.x + Utils.EPSILON() * 2f && position.x >= mEndPoint.x - Utils.EPSILON() * 2f && mStartPoint.x > mEndPoint.x);
    }
    public bool onBorder(Vector3 position)
    {
        return (position.x == mStartPoint.x && position.x == mEndPoint.x && position.y >= mStartPoint.y && position.y <= mEndPoint.y && mStartPoint.y < mEndPoint.y) 
            || (position.y == mStartPoint.y && position.y == mEndPoint.y && position.x >= mStartPoint.x && position.x <= mEndPoint.x && mStartPoint.x < mEndPoint.x)
            || (position.x == mStartPoint.x && position.x == mEndPoint.x && position.y <= mStartPoint.y && position.y >= mEndPoint.y && mStartPoint.y > mEndPoint.y)
            || (position.y == mStartPoint.y && position.y == mEndPoint.y && position.x <= mStartPoint.x && position.x >= mEndPoint.x && mStartPoint.x > mEndPoint.x);
    }
    public void hideColliderBorder()
    {
        BoxCollider2D box_collider = gameObject.GetComponent<BoxCollider2D>();
        if (isVertical())
        {
            box_collider.size = new Vector2(mLineRenderer.startWidth, box_collider.size.y);
        }
        else if (isHorizontal())
        {
            box_collider.size = new Vector2(box_collider.size.x, mLineRenderer.startWidth);
        }
        box_collider.enabled = false;
    }
    public Vector3 getStartPoint()
    {
        return mStartPoint;
    }
    public Vector3 getLastPoint()
    {
        return mEndPoint;
    }
    public bool isVertical()
    {
        return gameObject.tag == "VerticalBorder";
    }
    public bool isHorizontal()
    {
        return gameObject.tag == "HorizontalBorder";
    }
    public bool contains(Vector3 pos)
    {
        if(pos.x == mStartPoint.x && pos.x == mEndPoint.x)
        {
            return (mStartPoint.y <= pos.y && pos.y <= mEndPoint.y) || (mStartPoint.y >= pos.y && pos.y >= mEndPoint.y);
        }
        if (pos.y == mStartPoint.y && pos.y == mEndPoint.y)
        {
            return (mStartPoint.x <= pos.x && pos.x <= mEndPoint.x) || (mStartPoint.x >= pos.x && pos.x >= mEndPoint.x);
        }
        return false;
    }
    public static Border create(Vector3 start_point, Vector3 end_point)
    {
        GameObject new_border = new GameObject();
        Border border = new_border.AddComponent<Border>();
        border.init(start_point, end_point);

        return border;
    }
}
