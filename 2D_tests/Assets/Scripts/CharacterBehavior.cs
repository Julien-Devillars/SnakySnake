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
    public Direction.direction mPreviousDirection;
    private bool mDirectionUpdated;
    private bool mCanMove;
    private bool mCanAddLine;

    public List<GameObject> mBackgroundGameObjects;
    public List<Background> mBackgrounds;

    public List<GameObject> mBorderGameObjects;
    public List<Border> mBorders;
    [HideInInspector] public Vector3 mMinBorderPos;
    [HideInInspector] public Vector3 mMaxBorderPos;

    private List<GameObject> mEnemies;

    private List<GameObject> mTrailPoints;
    private GameObject mTrailPointsGO;
    public List<GameObject> mTrails;
    private GameObject mTrailGO;
    private GameObject mLastPosition_go;
    private Border mLastBorder;
    private Border mLastBorderWhichCreateTrail;

    // Start is called before the first frame update
    void Start()
    {
        mDirections = Direction.directions;
        mCurrentDirection = Direction.direction.None;
        mPreviousDirection = Direction.direction.None;
        mCanMove = true;
        mCanAddLine = true;

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinBorderPos = new Vector3(-width, -height, 0);
        mMaxBorderPos = new Vector3(width, height, 0);

        transform.position = new Vector3(mMinBorderPos.x, mMinBorderPos.y, 0);
        mBorders = new List<Border>();
        mBorderGameObjects = new List<GameObject>();
        makeBorders();

        mBackgrounds = new List<Background>();
        mBackgroundGameObjects = new List<GameObject>();

        GameObject background_go = new GameObject();
        mBackgroundGameObjects.Add(background_go);

        Background background = background_go.AddComponent<Background>();
        background.init(mMinBorderPos, mMaxBorderPos, "0");

        mBackgrounds.Add(background);

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

    public void addBorder(Border border)
    {
        border.setName("Border_" + mBorders.Count);
        mBorderGameObjects.Add(border.gameObject);
        mBorders.Add(border);
    }

    private void makeBorders()
    {
        Vector3 top_left = new Vector3(mMinBorderPos.x, mMaxBorderPos.y, mMinBorderPos.z);
        Vector3 top_right = new Vector3(mMaxBorderPos.x, mMaxBorderPos.y, mMinBorderPos.z);
        Vector3 bot_left = new Vector3(mMinBorderPos.x, mMinBorderPos.y, mMinBorderPos.z);
        Vector3 bot_right = new Vector3(mMaxBorderPos.x, mMinBorderPos.y, mMinBorderPos.z);

        Border top = Border.create(top_left, top_right, false);
        Border right = Border.create(top_right, bot_right, false);
        Border bot = Border.create(bot_right, bot_left, false);
        Border left = Border.create(bot_left, top_left, false);

        mLastBorder = bot;

        addBorder(top);
        addBorder(right);
        addBorder(bot);
        addBorder(left);
    }

    // Update is called once per frame
    void Update()
    {
        if (Utils.GAME_STOPPED) return;
        Background current_bg = GetBackground(transform.position);
        Border current_border = GetBorder(transform.position);

        Direction.direction new_direction = getInputDirection();
        updateDirection(new_direction);
        if(mTrailPoints.Count == 0)
        {
            setOnBorderSameDirection();
        }

        Background next_bg = getNextBackground();
        Border next_border = GetBorder(getNextPosition());
        Vector3 next_pos = getNextPosition();

        if (current_bg == null && next_bg == null && current_border != null && current_border != next_border && next_border == null)
        {
            if(isInScreen(next_pos))
            {
                setOnBorderOppositeDirection();
                addLine();
            }
        }
        else if ((!isInScreen(next_pos) && !mDirectionUpdated) || (next_bg != null && next_bg != current_bg && !next_bg.hasEnemies()))//onBorder() && !should_create_line)
        {
            if(mTrails.Count > 0)
            {
                setOnBorderOppositeDirection(false);
            }
            deleteLine();
        }
        else if (mTrails.Count > 0 && lastTrailIntersectBorder())
        {
            setOnBorderOppositeDirection();
            deleteLine();
            Background new_next_bg = getNextBackground(); // We update the position
            if (new_next_bg != null && new_next_bg.hasEnemies())/* && GetBorder(next_pos) == null*/
            {
                addLine();
            }
        }
        else
        {
            if (mDirectionUpdated && (current_bg == null || current_bg.hasEnemies()) && next_border == null) // Leave border to create trail inside BG
            {
                addLine();
            }
            else if (next_bg != null && next_bg != current_bg && next_bg.hasEnemies() && (current_bg  == null || current_bg != null && !current_bg.hasEnemies()))
            {
                setOnBorderOppositeDirection();
                addLine();
            }
        }
        moveBall();

        // Delete line if crossing a border.
        if(mTrails.Count > 0)
        {
            GameObject trail_go = mTrails[mTrails.Count - 1];
            LineRenderer trail = trail_go.GetComponent<LineRenderer>();
            if(trail != null)
            {
                Vector3 trail_start_point = trail.GetPosition(0);
                for (int i = 0; i < mBorders.Count; ++i) // Use for instead of foreach to avoid exception due to mBorders updates when deleting line
                {
                    Border border = mBorders[i];

                    //if ( !(border.mStartPoint.x == trail_start_point.x && border.mEndPoint.x == trail_start_point.x || border.mStartPoint.y == trail_start_point.y && border.mEndPoint.y == trail_start_point.y)
                    //    && Utils.intersect(trail.GetPosition(0), trail.GetPosition(1), border.mStartPoint, border.mEndPoint))
                    //{
                    if (!border.contains(trail.GetPosition(0))
                        && Utils.intersect(trail.GetPosition(0), trail.GetPosition(1), border.mStartPoint, border.mEndPoint))
                    {
                        Debug.Log("Stop");
                        deleteLine();
                        addLine();
                        break;
                    }
                }
            }
        }

        countScore();
    }
    bool isInScreen(Vector3 point)
    {
        return point.x > mMinBorderPos.x && point.x < mMaxBorderPos.x && point.y > mMinBorderPos.y && point.y < mMaxBorderPos.y;
    }
    private Vector3 getNextPosition()
    {
        return transform.position + Direction.directions[mCurrentDirection] * transform.localScale.x;
    }
    private Vector3 getPreviousPosition()
    {
        return transform.position - Direction.directions[mCurrentDirection] * transform.localScale.x;
    }
    private Vector3 getNextPositionWithPreviousDirection()
    {
        return transform.position + Direction.directions[mCurrentDirection] * transform.localScale.x;
    }
    private Vector3 getPreviousPositionWithPreviousDirection()
    {
        return transform.position - Direction.directions[mPreviousDirection] * transform.localScale.x;
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

        mLastBorderWhichCreateTrail = mLastBorder;
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

    void fixLastTrailIfNeeded()
    {

        Vector3 last_point = transform.position;
        Vector3 before_last_point = mTrailPoints[mTrailPoints.Count - 1].transform.position;
        if (last_point.x == before_last_point.x || last_point.y == before_last_point.y) return; // OK

        Vector3 new_point = new Vector3();
        if (Direction.isVertical(mCurrentDirection))
        {
            new_point = new Vector3(last_point.x, before_last_point.y);
        }
        if (Direction.isHorizontal(mCurrentDirection))
        {
            new_point = new Vector3(before_last_point.x, last_point.y);
        }
        GameObject new_in_between_trail_point = new GameObject();
        new_in_between_trail_point.gameObject.transform.parent = mTrailPointsGO.gameObject.transform;
        new_in_between_trail_point.name = "Trail Points " + mTrailPoints.Count.ToString();
        new_in_between_trail_point.transform.position = new_point;
        mTrailPoints.Add(new_in_between_trail_point);

        GameObject new_in_between_trail = new GameObject();
        new_in_between_trail.gameObject.transform.parent = mTrailGO.gameObject.transform;
        new_in_between_trail.name = "Trail " + mTrails.Count.ToString();
        Trail trail = new_in_between_trail.AddComponent<Trail>();
        mTrails.Add(new_in_between_trail);
        trail.forceUpdateTrailPoint();
        mTrails[mTrails.Count - 2].GetComponent<Trail>().forceUpdateTrailPoint();
        Debug.Log("split trail");
    }
void deleteLine()
    {
        if (mTrailPoints.Count == 0)
            return;

        fixLastTrailIfNeeded();

        foreach (GameObject trail_point in mTrailPoints)
        {
            Destroy(trail_point);
        }
        mTrailPoints.Clear();

        List<Background> deleted_bgs = new List<Background>();
        foreach (GameObject trail in mTrails)
        {
            Trail trail_script = trail.GetComponent<Trail>();
            if(!trail_script.mHasBeenUpdated)
            {
                trail_script.forceUpdateTrailPoint();
            }
            // Transform line to border
            LineRenderer lineRenderer = trail.GetComponent<LineRenderer>();
            //Utils.fixGivenPointsIfNeeded(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
            if (trail == mTrails[mTrails.Count - 1])
            {
                Vector3 pos_on_border = getOnClosestBorder(lineRenderer.GetPosition(1));
                lineRenderer.SetPosition(1, pos_on_border);
            }
            Border line_to_border = Border.create(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
            if(line_to_border.mStartPoint == Vector3.zero || line_to_border.mEndPoint == Vector3.zero)
            {
                Debug.Log("TOTO");
            }
            line_to_border.mNewBorderOnDelete = true;
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
                splitBackground(bg, bg_start_point, bg_end_point);
                deleted_bgs.Add(bg);
            }

            Destroy(trail);
        }
        mTrails.Clear();

        foreach(Border border in mBorders)
        {
            border.mHasToUpdateBorder = true;
            border.mNewBorderOnDelete = false;
        }

        // Clean backgrounds information (connection & enemies) before assigned it again
        foreach (Background bg in mBackgrounds)
        {
            bg.mEnemyList.Clear();
        }

        foreach (Background bg_1 in mBackgrounds)
        {
            bg_1.mConnectedBackground.Clear();
            foreach (GameObject ennemy_to_reassign in mEnemies)
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
        mLastBorderWhichCreateTrail = null;
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
            mPreviousDirection = mCurrentDirection;
            mCurrentDirection = Direction.Left;
            GetComponent<SpriteRenderer>().flipX = true;
            return true;
        }
        else if ((new_direction == Direction.Up && mCurrentDirection != Direction.Up) && (can_move_backward || mCurrentDirection != Direction.Down))
        {
            StartCoroutine(waiter());
            mPreviousDirection = mCurrentDirection;
            mCurrentDirection = Direction.Up;
            return true;
        }
        else if ((new_direction == Direction.Right && mCurrentDirection != Direction.Right) && (can_move_backward || mCurrentDirection != Direction.Left))
        {
            StartCoroutine(waiter());
            mPreviousDirection = mCurrentDirection;
            mCurrentDirection = Direction.Right;
            GetComponent<SpriteRenderer>().flipX = false;
            return true;
        }
        else if ((new_direction == Direction.Down && mCurrentDirection != Direction.Down) && (can_move_backward || mCurrentDirection != Direction.Up))
        {
            StartCoroutine(waiter());
            mPreviousDirection = mCurrentDirection;
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
        foreach (Background bg in mBackgrounds)
        {
            if (bg.contains(pos))
            {
                return bg;
            }
        }
        return null;
    }
    Border GetBorder(Vector3 pos)
    {
        foreach (Border border in mBorders)
        {
            if (border.contains(pos))
            {
                return border;
            }
        }
        return null;
    }

    void splitBackground(Background current_bg, Vector3 start_point, Vector3 end_point)
    {
        List<GameObject> background_splitten = current_bg.split(start_point, end_point);

        if (background_splitten == null || background_splitten.Count != 2) return;
        
        GameObject bg_1 = background_splitten[0];
        GameObject bg_2 = background_splitten[1];

        Background background_1 = bg_1.GetComponent<Background>();
        Background background_2 = bg_2.GetComponent<Background>();

        mBackgroundGameObjects.Add(bg_1);
        mBackgroundGameObjects.Add(bg_2);

        mBackgrounds.Add(background_1);
        mBackgrounds.Add(background_2);
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

    Vector3 getClosestPoint(Dictionary<Border, Vector3> border_points)
    {
        Vector3 closest_point = mMaxBorderPos*2f;
        foreach (Border border_key in border_points.Keys)
        {
            Vector3 border_point = border_points[border_key];
            if (Vector3.Distance(transform.position, border_point) < Vector3.Distance(transform.position, closest_point))
            {
                closest_point = border_point;
                mLastBorder = border_key;
            }

        }
        return closest_point;
    }

    void setOnBorderSameDirection()
    {
        Dictionary<Border, Vector3> border_points = new Dictionary<Border, Vector3>();
        foreach (Border border in mBorders)
        {
            if(border == mLastBorderWhichCreateTrail && mTrailPoints.Count < 2)
            {
                continue;
            }

            if (border.onSmallFuzzyBorder(transform.position))
            {
                if(Direction.isVertical(mCurrentDirection) && border.isVertical())
                {
                    Vector3 point = new Vector3(border.mStartPoint.x, gameObject.transform.position.y, 0);
                    border_points.Add(border, point);
                }
                else if (Direction.isHorizontal(mCurrentDirection) && border.isHorizontal())
                {
                    Vector3 point = new Vector3(gameObject.transform.position.x, border.mStartPoint.y, 0);
                    border_points.Add(border, point);
                }
            }
        }

        if(border_points.Count == 0)
        {
            return;
        }


        transform.position = getClosestPoint(border_points);
    }

    void setOnBorderOppositeDirection(bool check_previous_direction = true)
    {
        Dictionary<Border, Vector3> border_points = new Dictionary<Border, Vector3>();
        foreach (Border border in mBorders)
        {
            if (border == mLastBorderWhichCreateTrail && mTrailPoints.Count < 2) continue;
            
            if (border.onSmallFuzzyBorder(transform.position))
            {
                if (border.isHorizontal() && (Direction.isVertical(mCurrentDirection) || (check_previous_direction && Direction.isVertical(mPreviousDirection))))
                {
                    Vector3 point = new Vector3(gameObject.transform.position.x, border.mStartPoint.y, 0);
                    if (point == transform.position) continue;
                    border_points.Add(border, point);
                }
                else if (border.isVertical() && (Direction.isHorizontal(mCurrentDirection) || (check_previous_direction && Direction.isHorizontal(mPreviousDirection))))
                {
                    Vector3 point = new Vector3(border.mStartPoint.x, gameObject.transform.position.y, 0);
                    if (point == transform.position) continue;
                    border_points.Add(border, point);
                }
            }
        }

        if (border_points.Count == 0)
        {
            return;
        }

        transform.position = getClosestPoint(border_points);
    }

    Vector3 getOnClosestBorder(Vector3 old_pos, List<Border> _borders)
    {
        List<Border> borders = new List<Border>();
        foreach (Border border in _borders)
        {
            //if (border == mLastBorder && mTrailPoints.Count < 2)
            //{
            //    continue;
            //}
            if (border.onSmallFuzzyBorder(transform.position)&& !border.mNewBorderOnDelete)
            {
                borders.Add(border);
            }
        }

        if (borders.Count == 0)
        {
            Debug.Log("ASSERT : Borders should not be empty.");
            return old_pos;
        }

        Vector3 closest_point = Vector3.positiveInfinity;

        foreach (Border border in borders)
        {
            if (border.isHorizontal() && (Direction.isVertical(mCurrentDirection) || Direction.isVertical(mPreviousDirection)))
            {
                Vector3 point_on_border = new Vector3(gameObject.transform.position.x, border.mStartPoint.y, 0);
                if (border.contains(point_on_border) && Vector3.Distance(transform.position, point_on_border) < Vector3.Distance(transform.position, closest_point))
                {
                    closest_point = point_on_border;
                    //mLastBorder = border;
                }
            }
            else if (border.isVertical() && (Direction.isHorizontal(mCurrentDirection) || Direction.isHorizontal(mPreviousDirection)))
            {
                Vector3 point_on_border = new Vector3(border.mStartPoint.x, gameObject.transform.position.y, 0);
                if (border.contains(point_on_border) && Vector3.Distance(transform.position, point_on_border) < Vector3.Distance(transform.position, closest_point))
                {
                    closest_point = point_on_border;
                    //mLastBorder = border;
                }
            }

        }

        return closest_point;
    }
    Vector3 getOnClosestBorder(Vector3 old_pos)
    {
        return getOnClosestBorder(old_pos, mBorders);
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
    private bool intersectBorderWithPreviousDirection()
    {
        Vector3 previous_pos = getPreviousPositionWithPreviousDirection();
        Vector3 next_pos = getNextPositionWithPreviousDirection();
        foreach(Border border in mBorders)
        {
            if(Utils.intersect(previous_pos, next_pos, border.mStartPoint, border.mEndPoint))
            {
                return true;
            }
        }
        return false;
    }
    private bool lastTrailIntersectBorder()
    {
        GameObject trail_go = mTrails[mTrails.Count - 1];
        LineRenderer line_renderer = trail_go.GetComponent<LineRenderer>();

        foreach (Border border in mBorders)
        {
            if(border.contains(line_renderer.GetPosition(0)))
            {
                continue;
            }

            if (Utils.intersect(line_renderer.GetPosition(0), line_renderer.GetPosition(1), border.mStartPoint, border.mEndPoint))
            {
                return true;
            }
        }
        return false;
    }
}
