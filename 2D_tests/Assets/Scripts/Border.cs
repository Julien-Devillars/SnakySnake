using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public Vector3 mStartPoint;
    public Vector3 mEndPoint;
    public Border mDuplicateBorder;
    public bool mHasError;
    public bool mIsEditable;
    public bool mNewBorderOnDelete = false;
    private LineRenderer mLineRenderer;
    public Dictionary<Border, Vector3> mOtherBorderOnBorder;
    public bool mHasToUpdateBorder = true;

    public void Awake()
    {
        gameObject.name = "Border";
        gameObject.name = gameObject.name.Replace("(Clone)", "");
        gameObject.transform.parent = GameObject.Find("Borders").transform;
        mOtherBorderOnBorder = new Dictionary<Border, Vector3>();
    }

    public void Update()
    {
        if(mHasToUpdateBorder)
        {
            mOtherBorderOnBorder.Clear();
            updateBorderConnection();
            mHasToUpdateBorder = false;
        }
        bool start_point_can_be_moved = borderIsInBackgroundWithoutEnemies(mStartPoint);
        bool end_point_can_be_moved = borderIsInBackgroundWithoutEnemies(mEndPoint);
        if(mIsEditable)
        {

            if (start_point_can_be_moved && end_point_can_be_moved)
            {
                destroy();
            }
            else if (start_point_can_be_moved)
            {
                Debug.Log("Move Start Point : " + name);
                Vector3 new_point = getClosestPointOnBorder(mStartPoint);
                if (new_point == Vector3.zero) return;
                replaceStartPoint(new_point);
            }
            else if(end_point_can_be_moved)
            {
                Debug.Log("Move End Point : " + name);
                Vector3 new_point = getClosestPointOnBorder(mEndPoint);
                if (new_point == Vector3.zero) return;
                replaceEndPoint(new_point);
            }
            checkIfBorderShouldBeSplit();
        }
    }

    public void init(Vector3 start_point, Vector3 end_point, bool is_editable = true)
    {
        mStartPoint = start_point;
        mEndPoint = end_point;

        addLineRenderer();
        addTag();

        addCollider2D();
        
        mDuplicateBorder = null;
        mHasError = false;
        mIsEditable = is_editable;
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
    private void replaceStartPoint(Vector3 new_start_point)
    {
        mStartPoint = new_start_point;
        mLineRenderer.SetPosition(0, new_start_point);
    }
    private void replaceEndPoint(Vector3 new_end_point)
    {
        mEndPoint = new_end_point;
        mLineRenderer.SetPosition(1, new_end_point);
    }
    public Vector3 getStartPoint()
    {
        return mStartPoint;
    }
    public Vector3 getLastPoint()
    {
        return mEndPoint;
    }
    public bool isLeftToRight()
    {
        return mStartPoint.x < mEndPoint.x;
    }
    public bool isBottomToTop()
    {
        return mStartPoint.y < mEndPoint.y;
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
    public static Border create(Vector3 start_point, Vector3 end_point, bool is_editable = true)
    {
        GameObject new_border = new GameObject();
        Border border = new_border.AddComponent<Border>();
        border.init(start_point, end_point, is_editable);

        return border;
    }

    private bool borderIsInBackgroundWithoutEnemies(Vector3 point)
    {
        List<Background> backgrounds = Utils.getBackgrounds();

        List<Background> backgrounds_to_check = new List<Background>();
        foreach (Background background in backgrounds)
        {
            if(background.containsEquals(point))
            {
                backgrounds_to_check.Add(background);
            }
        }

        foreach (Background background in backgrounds_to_check)
        {
            if (background.hasEnemies())
            {
                return false;
            }
        }

        return true;
    }
    private void splitBorder(Vector3 end_point_1,Vector3 start_point_2)
    {
        Border new_border_1;
        Border new_border_2;
        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();

        if(isBottomToTop() || isLeftToRight())
        {
            new_border_1 = Border.create(mStartPoint, end_point_1);
            new_border_2 = Border.create(start_point_2, mEndPoint);
            character.addBorder(new_border_1);
            character.addBorder(new_border_2);
        }
        else
        {
            new_border_1 = Border.create(mEndPoint, end_point_1);
            new_border_2 = Border.create(start_point_2, mStartPoint);
            character.addBorder(new_border_1);
            character.addBorder(new_border_2);
        }
        destroy();
    }
    private bool splitOrReplaceBorder(Vector3 point_1, Vector3 point_2)
    {
        if(point_1 == mStartPoint)
        {
            replaceStartPoint(point_2);
            return false;
        }
        else if (point_1 == mEndPoint)
        {
            replaceEndPoint(point_2);
            return false;
        }
        else if (point_2 == mStartPoint)
        {
            replaceStartPoint(point_1);
            return false;
        }
        else if (point_2 == mEndPoint)
        {
            replaceEndPoint(point_1);
            return false;
        }
        else
        {
            splitBorder(point_1, point_2);
            return true;
        }
    }
    private bool checkIfBorderShouldBeSplit()
    {
        List<Vector3> possible_points = new List<Vector3>();
        foreach(Vector3 point in mOtherBorderOnBorder.Values)
        {
            possible_points.Add(point);
        }

        if(isVertical())
        {
            possible_points.Sort((p1, p2) => p1.y.CompareTo(p2.y));
        }
        if(isHorizontal())
        {
            possible_points.Sort((p1, p2) => p1.x.CompareTo(p2.x));
        }

        for(int i = 0; i < possible_points.Count - 1; ++i)
        {
            Vector3 p1 = possible_points[i];
            Vector3 p2 = possible_points[i + 1];

            Vector3 center = (p1 + p2) / 2f;
            bool should_split = borderIsInBackgroundWithoutEnemies(center);
            if(should_split)
            {
                bool is_split = splitOrReplaceBorder(p1, p2);
                if(!is_split)
                {
                    mOtherBorderOnBorder.Clear();
                    updateBorderConnection();
                }
                break;
            }

        }

        return true;
    }
    private Vector3 getClosestPointOnBorder(Vector3 point)
    {
        List<Border> borders = Utils.getBorders();

        List<Vector3> possible_points = new List<Vector3>();
        foreach (Border border in borders)
        {
            if (border == this) continue;

            if (contains(border.mStartPoint))
            {
                possible_points.Add(border.mStartPoint);
            }
            if (contains(border.mEndPoint))
            {
                possible_points.Add(border.mEndPoint);
            }
        }

        if (possible_points.Count == 0) return Vector3.zero;

        Vector3 closest_point = Vector3.positiveInfinity;
        foreach (Vector3 possible_point in possible_points)
        {
            if (borderIsInBackgroundWithoutEnemies(possible_point)) continue;

            if (Vector3.Distance(possible_point, point) < Vector3.Distance(closest_point, point))
            {
                closest_point = possible_point;
            }
        }

        return closest_point;
    }
    void updateBorderConnection()
    {
        foreach (Border border in Utils.getBorders())
        {
            if (border == this) continue;

            if(contains(border.mStartPoint))
            {
                if (!GameObject.Find(border.name)) continue;
                if (mOtherBorderOnBorder.ContainsKey(border)) Debug.Log("Key " + border.name + " is already present in " + name);
                mOtherBorderOnBorder.Add(border, border.mStartPoint);
                //Debug.Log(name + " has " + border.name + " start point linked");
            }
            if (contains(border.mEndPoint))
            {
                if (!GameObject.Find(border.name)) continue;
                if (mOtherBorderOnBorder.ContainsKey(border)) Debug.Log("Key " + border.name + " is already present in " + name);
                mOtherBorderOnBorder.Add(border, border.mEndPoint);
                //Debug.Log(name + " has " + border.name + " end point linked");
            }
        }
        
    }
}
