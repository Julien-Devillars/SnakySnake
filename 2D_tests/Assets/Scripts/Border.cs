using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderPointsConnection
{
    public BorderPointsConnection(Border _border, Vector3 _point)
    {
        border = _border;
        point = _point;
    }
    public Border border;
    public Vector3 point;
}


public class BorderPointsConnections : IEnumerator, IEnumerable
{
    public List<BorderPointsConnection> mBorderPointsConnections = new List<BorderPointsConnection>();
    private int position = -1;
    public void SortX()
    {
        mBorderPointsConnections.Sort((bp1, bp2) => bp1.point.x.CompareTo(bp2.point.x));
    }
    public void SortY()
    {
        mBorderPointsConnections.Sort((bp1, bp2) => bp1.point.y.CompareTo(bp2.point.y));
    }
    public void Clear()
    {
        mBorderPointsConnections.Clear();
    }
    public void Add(Border border , Vector3 point)
    {
        mBorderPointsConnections.Add(new BorderPointsConnection(border, point));
    }
    public int Count()
    {
        return mBorderPointsConnections.Count;
    }
    public BorderPointsConnection At(int index)
    {
        return mBorderPointsConnections[index];
    }
    public Border BorderAt(int index)
    {
        return mBorderPointsConnections[index].border;
    }
    public Vector3 PointAt(int index)
    {
        return mBorderPointsConnections[index].point;
    }
    public bool Contains(Border border)
    {
        foreach(BorderPointsConnection bp in mBorderPointsConnections)
        {
            if (bp.border == border)
            {
                return true;
            }
        }
        return false;
    }
    public bool Contains(Vector3 point)
    {
        foreach (BorderPointsConnection bp in mBorderPointsConnections)
        {
            if (bp.point == point)
            {
                return true;
            }
        }
        return false;
    }
    //IEnumerator and IEnumerable require these methods.
    public IEnumerator GetEnumerator()
    {
        return (IEnumerator)this;
    }
    public bool MoveNext()
    {
        position++;
        return (position < mBorderPointsConnections.Count);
    }
    public void Reset()
    {
        position = -1;
    }
    public object Current
    {
        get { return mBorderPointsConnections[position]; }
    }
}

public class Border : MonoBehaviour
{
    public Vector3 mStartPoint;
    public Vector3 mEndPoint;
    public Border mDuplicateBorder;
    public bool mHasError;
    public bool mIsEditable;
    public bool mNewBorderOnDelete = false;
    private LineRenderer mLineRenderer;
    private LineRenderer mOutlineRenderer;
    public BorderPointsConnections mOtherBorderOnBorders;
    public bool mHasToUpdateBorder = true;
    public bool mToDelete = false;
    public Direction.direction direction = Direction.direction.None;

    public void Awake()
    {
        gameObject.name = "Border";
        gameObject.name = gameObject.name.Replace("(Clone)", "");
        gameObject.transform.parent = GameObject.Find("Borders").transform;
        mOtherBorderOnBorders = new BorderPointsConnections();
    }

    public void Update()
    {
        if(mToDelete || mStartPoint == mEndPoint)
        {
            destroy();
            return;
        }
        if(mHasToUpdateBorder)
        {
            mOtherBorderOnBorders.Clear();
            updateBorderConnection();
            mHasToUpdateBorder = false;
        }

        Border connected_border_with_same_direction = getConnectedBorderWithSameDirection();
        if(connected_border_with_same_direction != null && isTheLongestBorder(connected_border_with_same_direction))
        {
            mergeBorder(connected_border_with_same_direction);
        }

        bool start_point_can_be_moved = borderIsInBackgroundWithoutEnemies(getShiftPlusPoint(mStartPoint));
        bool end_point_can_be_moved = borderIsInBackgroundWithoutEnemies(getShiftMinusPoint(mEndPoint));
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
                if (new_point == mEndPoint)
                {
                    destroy();
                    return;
                }
                if (new_point.x == Mathf.Infinity) return;
                replaceStartPoint(new_point);
            }
            else if(end_point_can_be_moved)
            {
                Debug.Log("Move End Point : " + name);
                Vector3 new_point = getClosestPointOnBorder(mEndPoint);
                if (new_point == mStartPoint)
                {
                    destroy();
                    return;
                }
                if (new_point.x == Mathf.Infinity) return;
                replaceEndPoint(new_point);
            }
            else
            {
                checkIfBorderShouldBeSplit();
            }
        }
    }
    public void mergeBorder(Border border_to_merge)
    {
        if (mStartPoint == border_to_merge.mEndPoint)
        {
            replaceStartPoint(border_to_merge.mStartPoint);
        }
        else if (mStartPoint == border_to_merge.mStartPoint)
        {
            replaceStartPoint(border_to_merge.mEndPoint);
        }
        else if (mEndPoint == border_to_merge.mStartPoint)
        {
            replaceEndPoint(border_to_merge.mEndPoint);
        }
        else if (mEndPoint == border_to_merge.mEndPoint)
        {
            replaceEndPoint(border_to_merge.mStartPoint);
        }
        border_to_merge.mToDelete = true;
        replaceCollider2D();
    }

    public bool isTheLongestBorder(Border border)
    {
        return Vector3.Distance(border.mStartPoint, border.mEndPoint) > Vector3.Distance(mStartPoint, mEndPoint);
    }

    public Border getConnectedBorderWithSameDirection()
    {
        for (int i = 0; i < mOtherBorderOnBorders.Count(); ++i)
        {
            Border border = mOtherBorderOnBorders.BorderAt(i);
            if (border != null && (border.isVertical() == isVertical() || border.isHorizontal() == isHorizontal()))
            {
                return border;
            }
        }
        return null;
    }

    public Vector3 getShiftPlusPoint(Vector3 point)
    {
        if(isVertical())
        {
            if(isBottomToTop())
            {
                return new Vector3(point.x, point.y + Utils.OFFSET);
            }
            else
            {
                return new Vector3(point.x, point.y - Utils.OFFSET);
            }
        }
        else
        {
            if (isLeftToRight())
            {
                return new Vector3(point.x + Utils.OFFSET, point.y);
            }
            else
            {
                return new Vector3(point.x - Utils.OFFSET, point.y);
            }
        }
    }

    public Vector3 getShiftMinusPoint(Vector3 point)
    {
        if (isVertical())
        {
            if (isBottomToTop())
            {
                return new Vector3(point.x, point.y - Utils.OFFSET);
            }
            else
            {
                return new Vector3(point.x, point.y + Utils.OFFSET);
            }
        }
        else
        {
            if (isLeftToRight())
            {
                return new Vector3(point.x - Utils.OFFSET, point.y);
            }
            else
            {
                return new Vector3(point.x + Utils.OFFSET, point.y);
            }
        }
    }

    public void init(Vector3 start_point, Vector3 end_point, bool is_editable = true)
    {
        mStartPoint = start_point;
        mEndPoint = end_point;

        addLineRenderer();
        addOutline();
        addTag();

        addCollider2D();
        
        mDuplicateBorder = null;
        mHasError = false;
        mIsEditable = is_editable;

        if (isHorizontal())
        {
            if(isLeftToRight())
            {
                direction = Direction.direction.Right;
            }
            else
            {
                direction = Direction.direction.Left;
            }
        }
        else
        {
            if (isBottomToTop())
            {
                direction = Direction.direction.Up;
            }
            else
            {
                direction = Direction.direction.Down;
            }
        }
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
        mLineRenderer.startWidth = ball.transform.localScale.x - Utils.OUTLINE_BORDER_THICKNESS;
        mLineRenderer.endWidth = ball.transform.localScale.x - Utils.OUTLINE_BORDER_THICKNESS;

        mLineRenderer.SetPosition(0, mStartPoint);
        mLineRenderer.SetPosition(1, mEndPoint);
        mLineRenderer.startColor = Color.black;
        mLineRenderer.endColor = Color.black;

        if(Utils.SHADER_ON)
        {
            //mLineRenderer.material = (Material)Resources.Load("Shaders/GlowBorder", typeof(Material));
            //mLineRenderer.material = (Material)Resources.Load("Materials/Border", typeof(Material));
            mLineRenderer.material = (Material)Resources.Load("Materials/Border", typeof(Material));
            mLineRenderer.material.color = Color.black;
        }
        else
        {
            mLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            mLineRenderer.material.color = Color.black;
        }

        mLineRenderer.sortingLayerName = mIsEditable ? "Border/Editable" : "Border/NotEditable";
    }

    private void addOutline()
    {
        GameObject outline = new GameObject();
        outline.name = "Border Outline";
        outline.transform.parent = transform;
        mOutlineRenderer = outline.AddComponent<LineRenderer>();

        mOutlineRenderer.positionCount = mLineRenderer.positionCount;
        mOutlineRenderer.useWorldSpace = mLineRenderer.useWorldSpace;
        mOutlineRenderer.numCapVertices = mLineRenderer.numCapVertices;

        mOutlineRenderer.startWidth = mLineRenderer.startWidth + Utils.OUTLINE_BORDER_THICKNESS;
        mOutlineRenderer.endWidth = mLineRenderer.endWidth + Utils.OUTLINE_BORDER_THICKNESS;

        Vector3 pos_0 = mLineRenderer.GetPosition(0);
        Vector3 pos_1 = mLineRenderer.GetPosition(1);
        mOutlineRenderer.SetPosition(0, new Vector3(pos_0.x, pos_0.y, pos_0.z + 1));
        mOutlineRenderer.SetPosition(1, new Vector3(pos_1.x, pos_1.y, pos_1.z + 1));
        mOutlineRenderer.startColor = Color.white;
        mOutlineRenderer.endColor = Color.white;
        mOutlineRenderer.material = (Material)Resources.Load("Materials/BorderOutline", typeof(Material));
        mOutlineRenderer.sortingLayerName = "Border/Outline";

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
    private void replaceCollider2D()
    {
        BoxCollider2D box_collider = gameObject.GetComponent<BoxCollider2D>();
        Destroy(box_collider);
        addCollider2D();
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
        Vector3 new_point = new Vector3(new_start_point.x, new_start_point.y, new_start_point.z);
        mStartPoint = new_point;
        mLineRenderer.SetPosition(0, new_point);
        mOutlineRenderer.SetPosition(0, new Vector3(mStartPoint.x, mStartPoint.y, mStartPoint.z + 1));
        mHasToUpdateBorder = true;
    }
    private void replaceEndPoint(Vector3 new_end_point)
    {
        Vector3 new_point = new Vector3(new_end_point.x, new_end_point.y, new_end_point.z);
        mEndPoint = new_point;
        mLineRenderer.SetPosition(1, new_point);
        mOutlineRenderer.SetPosition(1, new Vector3(mEndPoint.x, mEndPoint.y, mEndPoint.z + 1));
        mHasToUpdateBorder = true;
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

    private bool borderIsInBackgroundWithoutEnemies(Vector3 point, bool debug_on = false)
    {
        List<Background> backgrounds = Utils.getBackgrounds();

        List<Background> backgrounds_to_check = new List<Background>();
        foreach (Background background in backgrounds)
        {
            if (debug_on)
            {
                Debug.Log(background.name);
                Debug.Log(background.mMinBorderPos + " < " + point + " < " + background.mMaxBorderPos);
            }
            if (background.containsEquals(point))
            {
                backgrounds_to_check.Add(background);
            }
        }

        foreach (Background background in backgrounds_to_check)
        {
            if (debug_on)
            {
                Debug.Log("Found BG : " + background.name);
            }
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
    private void splitOrReplaceBorder(Vector3 point_1, Vector3 point_2)
    {
        if(point_1 == mStartPoint && point_2 != mEndPoint)
        {
            replaceStartPoint(point_2);
        }
        else if (point_1 == mEndPoint && point_2 != mStartPoint)
        {
            replaceEndPoint(point_2);
        }
        else if (point_2 == mStartPoint && point_1 != mEndPoint)
        {
            replaceStartPoint(point_1);
        }
        else if (point_2 == mEndPoint && point_1 != mStartPoint)
        {
            replaceEndPoint(point_1);
        }
        else
        {
            splitBorder(point_1, point_2);
            mToDelete = true;
        }
    }

    private bool checkIfBorderShouldBeSplit()
    {
        if(this == null)
        {
            return false;
        }

        if(isVertical())
        {
            mOtherBorderOnBorders.SortY();
        }
        if(isHorizontal())
        {
            mOtherBorderOnBorders.SortX();
        }

        for(int i = 0; i < mOtherBorderOnBorders.Count() - 1; ++i)
        {
            Vector3 p1 = mOtherBorderOnBorders.PointAt(i);
            Vector3 p2 = mOtherBorderOnBorders.PointAt(i + 1);

            Vector3 center = (p1 + p2) / 2f;
            bool should_split = borderIsInBackgroundWithoutEnemies(center);
            if(should_split)
            {
                splitOrReplaceBorder(p1, p2);
                mOtherBorderOnBorders.Clear();
                updateBorderConnection();
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
            if (point == border.mStartPoint || point == border.mEndPoint) continue;

            if (contains(border.mStartPoint))
            {
                possible_points.Add(border.mStartPoint);
            }
            if (contains(border.mEndPoint))
            {
                possible_points.Add(border.mEndPoint);
            }
            if (border.contains(mStartPoint))
            {
                possible_points.Add(mStartPoint);
            }
            if (border.contains(mEndPoint))
            {
                possible_points.Add(mEndPoint);
            }
        }

        if (possible_points.Count == 0) return Vector3.positiveInfinity;

        Vector3 closest_point = Vector3.positiveInfinity;
        foreach (Vector3 possible_point in possible_points)
        {
            if (possible_point == point) continue;
            //if (borderIsInBackgroundWithoutEnemies(possible_point)) continue;
            //if (possible_point == mStartPoint || possible_point == mEndPoint) continue;

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
                if (!GameObject.Find(border.name) || border == null || border.mToDelete) continue;
                if (mOtherBorderOnBorders.Contains(border))
                {
                    //Debug.Log("Key " + border.name + " is already present in " + name);
                    continue;
                }

                mOtherBorderOnBorders.Add(border, border.mStartPoint);
            }
            if (contains(border.mEndPoint))
            {
                if (!GameObject.Find(border.name) || border == null || border.mToDelete) continue;

                if (mOtherBorderOnBorders.Contains(border))
                {
                    //Debug.Log("Key " + border.name + " is already present in " + name);
                    continue;
                }
                mOtherBorderOnBorders.Add(border, border.mEndPoint);
            }
        }
        
    }
}
