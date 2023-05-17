using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class CharacterBehavior : MonoBehaviour
{
    public float mSpeed;

    private Dictionary<Direction.direction, Vector3> mDirections;
    public Direction.direction mCurrentDirection;
    private bool mDirectionUpdated;
    private bool mCanMove;
    private bool mCanAddLine;

    public List<Background> mBackgrounds;

    public List<Border> mBorders;
    [HideInInspector] public Vector3 mMinBorderPos;
    [HideInInspector] public Vector3 mMaxBorderPos;

    private List<GameObject> mEnemies;

    private List<GameObject> mTrailPoints;
    private GameObject mTrailPointsGO;
    private List<GameObject> mTrails;
    private GameObject mTrailGO;
    private GameObject mLastPosition_go;

    // Start is called before the first frame update
    void Start()
    {
        mDirections = Direction.directions;
        mCanMove = true;
        mCanAddLine = true;

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinBorderPos = new Vector3(-width, -height, 0);
        mMaxBorderPos = new Vector3(width, height, 0);

        transform.position = new Vector3(mMinBorderPos.x, mMinBorderPos.y, 0);
        mBorders = new List<Border>();
        makeBorders();

        mBackgrounds = new List<Background>();
        mBackgrounds.Add(new Background(mMinBorderPos, mMaxBorderPos, "0"));

        mEnemies = new List<GameObject>();
        GameObject enemies_go = GameObject.Find("Enemies"); 
        for (int i = 0; i < enemies_go.transform.childCount; ++i)
        {
            GameObject enemy = enemies_go.transform.GetChild(i).gameObject;
            mEnemies.Add(enemy);
            mBackgrounds[0].addEnemy(enemy);
        }
        mBackgrounds[0].changeBackgroundColor();

        mLastPosition_go = new GameObject();
        mLastPosition_go.name = "start_point";

        mTrailPointsGO = new GameObject();
        mTrailPointsGO.name = "Trail Points";
        mTrailPoints = new List<GameObject>();

        mTrailGO = new GameObject();
        mTrailGO.name = "Trails";
        mTrails = new List<GameObject>();
    }

    void addBorder(Border border)
    {
        border.setName("Border_" + mBorders.Count);
        mBorders.Add(border);
    }

    private void makeBorders()
    {
        Vector3 top_left = new Vector3(mMinBorderPos.x, mMaxBorderPos.y, mMinBorderPos.z);
        Vector3 top_right = new Vector3(mMaxBorderPos.x, mMaxBorderPos.y, mMinBorderPos.z);
        Vector3 bot_left = new Vector3(mMinBorderPos.x, mMinBorderPos.y, mMinBorderPos.z);
        Vector3 bot_right = new Vector3(mMaxBorderPos.x, mMinBorderPos.y, mMinBorderPos.z);

        Border top = new Border(top_left, top_right);
        Border right = new Border(top_right, bot_right);
        Border bot = new Border(bot_right, bot_left);
        Border left = new Border(bot_left, top_left);
        addBorder(top);
        addBorder(right);
        addBorder(bot);
        addBorder(left);
    }

    // Update is called once per frame
    void Update()
    {
        Background current_bg = GetBackground(transform.position);

        Direction.direction new_direction = getInputDirection();
        updateDirection(new_direction);
        if(mTrailPoints.Count == 0)
        {
            setOnBorderSameDirection();
        }

        Background next_bg = getNextBackground();

        bool should_create_line = isInBackground();

        if (next_bg == null || (next_bg != current_bg && !next_bg.hasEnemies()))//onBorder() && !should_create_line)
        {
            //if (mTrailPoints.Count > 0)
            //{
            //    setOnBorder();
            //}
            deleteLine();
        }
        else
        {
            if (mDirectionUpdated && (current_bg == null || current_bg.hasEnemies())) // Leave border to create trail inside BG
            {
                addLine();
            }
            else if (next_bg != null && next_bg != current_bg && next_bg.hasEnemies() && (current_bg  == null || current_bg != null && !current_bg.hasEnemies()))
            {
                //setOnBorder();
                addLine();
            }
        }
        moveBall();
        countScore();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private Vector3 getNextPosition()
    {
        return transform.position + Direction.directions[mCurrentDirection] * transform.localScale.x;
    }
    private Background getNextBackground()
    {
        return GetBackground(getNextPosition());
    }
    private bool isInBackground()
    {
        Background next_bg = getNextBackground();
        if(next_bg == null)
        {
            //Debug.Log("Next move cannot find BG");
            return false;
        }
        if(!next_bg.hasEnemies())
        {
            return false;
        }
        return true;
    }

    void addLine()
    {
        if (!mCanAddLine)
            return;

        GameObject current_point = new GameObject();
        current_point.gameObject.transform.parent = mTrailPointsGO.gameObject.transform;
        current_point.name = "Trail Points " + mTrailPoints.Count.ToString();
        current_point.transform.position = transform.position;
        mTrailPoints.Add(current_point);

        GameObject current_trail = new GameObject();
        current_trail.gameObject.transform.parent = mTrailGO.gameObject.transform;
        current_trail.name = "Trail " + mTrails.Count.ToString();
        current_trail.AddComponent<Trail>();
        mTrails.Add(current_trail);
        StartCoroutine(waiterAddLine());
                
    }

    void deleteLine()
    {
        if (mTrailPoints.Count == 0)
            return;

        foreach (GameObject trail_point in mTrailPoints)
        {
            Destroy(trail_point);
        }
        mTrailPoints.Clear();

        List<Background> deleted_bgs = new List<Background>();
        foreach (GameObject trail in mTrails)
        {
            // Transform line to border
            LineRenderer lineRenderer = trail.GetComponent<LineRenderer>();
            //Utils.fixGivenPointsIfNeeded(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
            if (trail == mTrails[mTrails.Count - 1])
            {
                Vector3 pos_on_border = getOnBorderNotSameDirection(lineRenderer.GetPosition(1));
                lineRenderer.SetPosition(1, pos_on_border);
            }
            Border line_to_border = new Border(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
            addBorder(line_to_border);

            // Delete trail
            List<Vector3> points = Utils.getIntermediatePointFromTrail(line_to_border);
            List<Background> bgs = new List<Background>();
            foreach(Vector3 point in points)
            {
                Background bg = GetBackground(point);
                if(bg != null && !bgs.Contains(bg))
                {
                    bgs.Add(bg);
                }
            }

            foreach(Background bg in bgs)
            {
                Vector3 start_point = lineRenderer.GetPosition(0);
                Vector3 middle_point = lineRenderer.GetPosition(1);

                Vector3 bg_start_point = bg.getPointFromBackground(start_point, middle_point);
                Vector3 bg_end_point = bg.getPointFromBackground(middle_point, start_point);
                splitBackground(bg, bg_start_point, bg_end_point);// bg.split(start_point, middle_point);
                deleted_bgs.Add(bg);
            }

            Destroy(trail);
        }
        mTrails.Clear();

        List<GameObject> ennemies_to_reassign = new List<GameObject>();
        foreach(Background deleted_bg in deleted_bgs)
        {
            ennemies_to_reassign.AddRange(deleted_bg.getEnemies());
        }

        // Clean backgrounds information (connection & enemies) before assigned it again
        foreach (Background bg_1 in mBackgrounds)
        {
            foreach (GameObject ennemy_to_reassign in ennemies_to_reassign)
            {
                if (bg_1.mEnemyList.Contains(ennemy_to_reassign))
                {
                    bg_1.mEnemyList.Remove(ennemy_to_reassign);
                }
            }
        }

        foreach (Background bg_1 in mBackgrounds)
        {
            bg_1.mConnectedBackground.Clear();
            foreach (GameObject ennemy_to_reassign in ennemies_to_reassign)
            {
                if(bg_1.containsEquals(ennemy_to_reassign.transform.position))
                {
                    bg_1.addEnemy(ennemy_to_reassign);
                }
            }
            
            Vector3 center_1 = bg_1.getCenterPoint();
            foreach (Background bg_2 in mBackgrounds)
            {
                if (bg_1 == bg_2)
                    continue;
                
                Vector3 center_2 = bg_2.getCenterPoint();
                Vector3 mix_point_1 = new Vector3(center_1.x, center_2.y);
                Vector3 mix_point_2 = new Vector3(center_2.x, center_1.y);
                bool c1_is_connected = true;
                bool c2_is_connected = true;
                foreach(Border border in mBorders)
                {
                    bool mix1_c1_intersect = !Utils.intersect(border.mStartPoint, border.mEndPoint, mix_point_1, center_1);
                    bool mix1_c2_intersect = !Utils.intersect(border.mStartPoint, border.mEndPoint, mix_point_1, center_2);
                    bool mix2_c1_intersect = !Utils.intersect(border.mStartPoint, border.mEndPoint, mix_point_2, center_1);
                    bool mix2_c2_intersect = !Utils.intersect(border.mStartPoint, border.mEndPoint, mix_point_2, center_2);

                    bool mix1_is_ok = mix1_c1_intersect && mix1_c2_intersect;
                    bool mix2_is_ok = mix2_c1_intersect && mix2_c2_intersect;

                    c1_is_connected &= mix1_is_ok;
                    c2_is_connected &= mix2_is_ok;
                }
                if(c1_is_connected || c2_is_connected)
                {
                    bg_1.addConnection(bg_2);
                }
            }
        }

        // Propagate connection
        Utils.propagateBackgrounds(mBackgrounds);

        // Add ennemis related to propagate
        foreach(Background bg in mBackgrounds)
        {
            bg.addConnectedEnemy();
            bg.changeBackgroundColor();
        }
        
        // Draw bg
    }

    Direction.direction getInputDirection()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            return Direction.Left;
        }
        else if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
        {
            return Direction.Up;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            return Direction.Right;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            return Direction.Down;
        }
        return Direction.None;
    }

    public bool updateDirection(Direction.direction new_direction)
    {
        if(!mCanAddLine || !mCanMove)
        {
            return false;
        }

        bool can_move_backward = mTrailPoints.Count == 0;

        if ((new_direction == Direction.Left && mCurrentDirection != Direction.Left)  && (can_move_backward || mCurrentDirection != Direction.Right))
        {
            StartCoroutine(waiter());
            mCurrentDirection = Direction.Left;
            return true;
        }
        else if ((new_direction == Direction.Up && mCurrentDirection != Direction.Up) && (can_move_backward || mCurrentDirection != Direction.Down))
        {
            StartCoroutine(waiter());
            mCurrentDirection = Direction.Up;
            return true;
        }
        else if ((new_direction == Direction.Right && mCurrentDirection != Direction.Right) && (can_move_backward || mCurrentDirection != Direction.Left))
        {
            StartCoroutine(waiter());
            mCurrentDirection = Direction.Right;
            return true;
        }
        else if ((new_direction == Direction.Down && mCurrentDirection != Direction.Down) && (can_move_backward || mCurrentDirection != Direction.Up))
        {
            StartCoroutine(waiter());
            mCurrentDirection = Direction.Down;
            return true;
        }
        return false;
    }

    IEnumerator waiter()
    {
        mCanMove = false;
        mDirectionUpdated = true;
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        mCanMove = true;
    }
    IEnumerator waiterAddLine()
    {
        mCanAddLine = false;
        yield return new WaitForSeconds(Utils.ADD_LINE_UPDATE_TIME);
        mCanAddLine = true;
    }

    Background GetBackground(Vector3 pos)
    {
        Background current_bg = null;
        foreach (Background bg in mBackgrounds)
        {
            if (bg.contains(pos))
            {
                current_bg = bg;
            }
        }
        if (current_bg == null)
        {
            //Debug.Log("Current BG not found!");
            return null;
        }
        return current_bg;
    }

    void splitBackground(Background current_bg, Vector3 start_point, Vector3 end_point)
    {
        List<Background> background_splitten = current_bg.split(start_point, end_point);
        if (background_splitten == null || background_splitten.Count != 2)
        {
            return;
        }
        Background bg_1 = background_splitten[0];
        Background bg_2 = background_splitten[1];

        mBackgrounds.Remove(current_bg);
        mBackgrounds.Add(bg_1);
        mBackgrounds.Add(bg_2);
    }

    void countScore()
    {
        Score.Instance.mCurrentScore = 0;
        foreach (Background bg in mBackgrounds)
        {
            if (!bg.hasEnemies())
            {
                Score.Instance.mCurrentScore += bg.getArea();
            }
        }
    }

    bool onBorder(Vector3 pos)
    {
        foreach (Border border in mBorders)
        {
            if(border.onFuzzyBorder(pos))
            {
                return true;
            }
        }
        return false;
    }

    void setOnBorder()
    {
        Vector3 pos = transform.position;
        foreach (Border border in mBorders)
        {
            if (border.onFuzzyBorder(transform.position))
            {
                if (border == null || border.mBorder == null)
                {
                    continue;
                }

                if (border.mBorder.tag == "VerticalBorder")
                {
                    pos = new Vector3(border.mStartPoint.x, gameObject.transform.position.y, 0);
                }
                else if (border.mBorder.tag == "HorizontalBorder")
                {
                    pos = new Vector3(gameObject.transform.position.x, border.mStartPoint.y, 0);
                }
            }
        }
        transform.position = pos;
    }
    void setOnBorderSameDirection()
    {
        List<Vector3> border_points = new List<Vector3>();
        foreach (Border border in mBorders)
        {
            if (border.onSmallFuzzyBorder(transform.position))
            {
                if(Direction.isVertical(mCurrentDirection) && border.isVertical())
                {
                    Vector3 point = new Vector3(border.mStartPoint.x, gameObject.transform.position.y, 0);
                    border_points.Add(point);
                }
                else if (Direction.isHorizontal(mCurrentDirection) && border.isHorizontal())
                {
                    Vector3 point = new Vector3(gameObject.transform.position.x, border.mStartPoint.y, 0);
                    border_points.Add(point);
                }
            }
        }

        if(border_points.Count == 0)
        {
            return;
        }

        Vector3 closest_point = border_points[0];
        foreach (Vector3 border_point in border_points)
        {
            if(Vector3.Distance(transform.position, border_point) < Vector3.Distance(transform.position, closest_point))
            {
                closest_point = border_point;
            }

        }

        transform.position = closest_point;
    }
    Vector3 getOnBorder(Vector3 old_pos)
    {
        Vector3 pos = new Vector3();
        foreach (Border border in mBorders)
        {
            if (border.onFuzzyBorder(old_pos))
            {
                if (border == null || border.mBorder == null)
                {
                    continue;
                }

                if (border.mBorder.tag == "VerticalBorder")
                {
                    pos = new Vector3(border.mStartPoint.x, gameObject.transform.position.y, 0);
                }
                else if (border.mBorder.tag == "HorizontalBorder")
                {
                    pos = new Vector3(gameObject.transform.position.x, border.mStartPoint.y, 0);
                }
            }
        }
        return pos;
    }
    Vector3 getOnBorderNotSameDirection(Vector3 old_pos)
    { 
        List<Vector3> border_points = new List<Vector3>();
        foreach (Border border in mBorders)
        {
            if (border.onSmallFuzzyBorder(transform.position))
            {
                if (Direction.isVertical(mCurrentDirection) && border.isHorizontal())
                {
                    Vector3 point = new Vector3(gameObject.transform.position.x, border.mStartPoint.y, 0);
                    border_points.Add(point);
                }
                else if (Direction.isHorizontal(mCurrentDirection) && border.isVertical())
                {
                    Vector3 point = new Vector3(border.mStartPoint.x, gameObject.transform.position.y, 0);
                    border_points.Add(point);
                }
            }
        }

        if (border_points.Count == 0)
        {
            return old_pos;
        }

        Vector3 closest_point = border_points[0];
        foreach (Vector3 border_point in border_points)
        {
            if (Vector3.Distance(transform.position, border_point) < Vector3.Distance(transform.position, closest_point))
            {
                closest_point = border_point;
            }
        }

        return closest_point;
    }

    Vector3 getPositionInBorder(Vector3 position)
    {
        if (position.x < mMinBorderPos.x)
            return new Vector3(mMinBorderPos.x, position.y, position.z);
        if (position.x > mMaxBorderPos.x)
            return new Vector3(mMaxBorderPos.x, position.y, position.z);
        if (position.y < mMinBorderPos.y)
            return new Vector3(position.x, mMinBorderPos.y, position.z);
        if (position.y > mMaxBorderPos.y)
            return new Vector3(position.x, mMaxBorderPos.y, position.z);
        return position;
    }

    void moveBall()
    {
        foreach(Border border in mBorders)
        {
            if (border.onFuzzyBorder(transform.position))
            {
                setLastPosition(transform.position);
            }
        }
        Vector3 new_pos = transform.position + mDirections[mCurrentDirection] * mSpeed * Time.deltaTime;
        transform.position = getPositionInBorder(new_pos);
        mDirectionUpdated = false;
    }

    private Vector3 getLastPosition()
    {
        return mLastPosition_go.transform.position;
    }
    private void setLastPosition(Vector3 pos)
    {
        mLastPosition_go.transform.position = pos;
    }
}
