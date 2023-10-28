using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class CharacterBehavior : MonoBehaviour
{
    public float mSpeed;

    private Dictionary<Direction.direction, Vector3> mDirections;
    public Direction.direction mCurrentDirection;
    public Direction.direction mPreviousDirection;
    public Vector3 mPreviousPosition;
    private bool mDirectionUpdated;
    private bool mCanMove;
    private bool mCanAddLine;
    private bool mWaitOneFrame = false;
    private Direction.direction mSavedDirection = Direction.None;

    public List<GameObject> mBackgroundGameObjects;
    public List<Background> mBackgrounds;

    public List<GameObject> mBorderGameObjects;
    public List<Border> mBorders;
    [HideInInspector] public Vector3 mMinBorderPos;
    [HideInInspector] public Vector3 mMaxBorderPos;

    private List<GameObject> mEnemies;

    public List<GameObject> mTrailPoints;
    private GameObject mTrailPointsGO;
    public List<GameObject> mTrails;
    private GameObject mTrailGO;
    private GameObject mLastPosition_go;
    private Border mLastBorder;
    private Border mLastLastBorder;
    private Border mLastBorderWhichCreateTrail;

    private bool mFingerDown;
    Vector3 mStartPositionFingerDown;
    Vector3 mLastPositionFingerUp;
    private PlayerControl mPlayerControl;
    private Direction.direction mInputDirection;
    private bool mInputStop;
    private float mDeadStickZone = 0.95f;
    private bool mControllerStickIsActivated = false;
    private List<Direction.direction> mPressedDirection = new List<Direction.direction>();
    private void Awake()
    {
        mPlayerControl = new PlayerControl();
        mPressedDirection.Add(Direction.Stop);
        mPlayerControl.PlayerController.Left.performed += ctx =>
        {
            //mInputDirection = Direction.Left;
            mPressedDirection.Add(Direction.Left);
        };
        mPlayerControl.PlayerController.Left.canceled += ctx =>
        {
            mControllerStickIsActivated = false;
            mPressedDirection.Remove(Direction.Left);
            if (mCurrentDirection != Direction.Left) return;
            //mInputDirection = Direction.Stop;
        };
        mPlayerControl.PlayerController.Right.performed += ctx =>
        {
            //mInputDirection = Direction.Right;
            mPressedDirection.Add(Direction.Right);
        };
        mPlayerControl.PlayerController.Right.canceled += ctx =>
        {
            mControllerStickIsActivated = false;
            mPressedDirection.Remove(Direction.Right);
            if (mCurrentDirection != Direction.Right) return;
            //mInputDirection = Direction.Stop;
        };
        mPlayerControl.PlayerController.Up.performed += ctx =>
        {
            //mInputDirection = Direction.Up;
            mPressedDirection.Add(Direction.Up);
        };
        mPlayerControl.PlayerController.Up.canceled += ctx =>
        {
            mControllerStickIsActivated = false;
            mPressedDirection.Remove(Direction.Up);
            if (mCurrentDirection != Direction.Up) return;
            //mInputDirection = Direction.Stop;
        };
        mPlayerControl.PlayerController.Down.performed += ctx =>
        {
            //mInputDirection = Direction.Down;
            mPressedDirection.Add(Direction.Down);
        };
        mPlayerControl.PlayerController.Down.canceled += ctx =>
        {
            mControllerStickIsActivated = false;
            mPressedDirection.Remove(Direction.Down);
            if (mCurrentDirection != Direction.Down) return;
            //mInputDirection = Direction.Stop;
        };
        mPlayerControl.PlayerController.Stop.performed += ctx =>
        {
            Debug.Log("Stop played");
            //mInputDirection = Direction.Stop;
            mInputStop = true;
        };
        mPlayerControl.PlayerController.Stop.canceled += ctx =>
        {
            //mInputDirection = Direction.Stop;
            mInputStop = false;
        };
        mPlayerControl.PlayerController.XStick.performed += ctx =>
        {
            float val_x = mPlayerControl.PlayerController.XStick.ReadValue<float>();
            float val_y = mPlayerControl.PlayerController.YStick.ReadValue<float>();
            if (Mathf.Abs(val_x) < Mathf.Abs(val_y)) return;
            if (Mathf.Abs(val_y) < mDeadStickZone && Mathf.Abs(val_x) < mDeadStickZone && mControllerStickIsActivated)
            {
                //mInputDirection = Direction.Stop;
                mPressedDirection.Remove(Direction.Down);
                mPressedDirection.Remove(Direction.Up);
                return;
            }
            if (Mathf.Abs(val_x) < mDeadStickZone) return;
            if (mInputDirection != Direction.direction.None && mInputDirection != Direction.direction.Stop) return;

            Debug.Log($"X : {val_x}");
            if(val_x < 0 && !mPressedDirection.Contains(Direction.Left))
            {
                Debug.Log("Go Left");
                //mInputDirection = Direction.Left;
                clearPressedButton();
                mPressedDirection.Add(Direction.Left);
                mControllerStickIsActivated = true;
            }
            if (val_x > 0 && !mPressedDirection.Contains(Direction.Right))
            {
                Debug.Log("Go Right");
                //mInputDirection = Direction.Right;
                clearPressedButton();
                mPressedDirection.Add(Direction.Right);
                mControllerStickIsActivated = true;
            }
        };
        mPlayerControl.PlayerController.YStick.performed += ctx =>
        {
            float val_x = mPlayerControl.PlayerController.XStick.ReadValue<float>();
            float val_y = mPlayerControl.PlayerController.YStick.ReadValue<float>();
            if (Mathf.Abs(val_y) < Mathf.Abs(val_x)) return;
            if (Mathf.Abs(val_y) < mDeadStickZone && Mathf.Abs(val_x) < mDeadStickZone && mControllerStickIsActivated)
            {
                //mInputDirection = Direction.Stop;
                mPressedDirection.Remove(Direction.Down);
                mPressedDirection.Remove(Direction.Up);
                return;
            }
            if (Mathf.Abs(val_y) < mDeadStickZone) return;
            if (mInputDirection != Direction.direction.None) return;

            Debug.Log($"Y : {val_y}");
            if (val_y < 0 && !mPressedDirection.Contains(Direction.Down))
            {
                Debug.Log("Go Down");
                //mInputDirection = Direction.Down;
                clearPressedButton();
                mPressedDirection.Add(Direction.Down);
                mControllerStickIsActivated = true;
            }
            if (val_y > 0 && !mPressedDirection.Contains(Direction.Up))
            {
                Debug.Log("Go Up");
                //mInputDirection = Direction.Up;
                clearPressedButton();
                mPressedDirection.Add(Direction.Up);
                mControllerStickIsActivated = true;
            }
        };
        mPlayerControl.PlayerController.XStick.canceled += ctx =>
        {
            mPressedDirection.Remove(Direction.Left);
            mPressedDirection.Remove(Direction.Right);
        };
        mPlayerControl.PlayerController.YStick.canceled += ctx =>
        {
            mPressedDirection.Remove(Direction.Down);
            mPressedDirection.Remove(Direction.Up);
        };

    }
    private void OnEnable()
    {
        mPlayerControl.PlayerController.Enable();
    }

    private void OnDisable()
    {
        mPlayerControl.PlayerController.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        mDirections = Direction.directions;
        mCurrentDirection = Direction.direction.None;
        mPreviousDirection = Direction.direction.None;
        mPreviousPosition = new Vector3();
        mCanMove = true;
        mCanAddLine = true;
        mFingerDown = false;

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize - Utils.EPSILON();
        float height = cam.orthographicSize - Utils.EPSILON();

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

        gameObject.GetComponent<SpriteRenderer>().material = new Material(Resources.Load<Material>("Materials/CharacterMaterial"));
        addAdditionalBorder();
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
        mLastLastBorder = left;

        addBorder(top);
        addBorder(right);
        addBorder(bot);
        addBorder(left);
    }
    public void addAdditionalBorder()
    {
        Level current_level = Worlds.getLevel(GameControler.currentWorld, GameControler.currentLevel);
        foreach(AdditionalBorder additionalBorder in current_level.mAdditionalBorders)
        {
            for(int i = 0; i < additionalBorder.mFakeTrails.Count - 1; ++i)
            {
                Vector3 pos_1 = additionalBorder.mFakeTrails[i];
                Vector3 pos_2 = additionalBorder.mFakeTrails[i + 1];
                addFakeTrail(pos_1, pos_2);
            }
            deleteFakeTrail();
        }
        //addFakeTrail(new Vector3(0.25f, 0.00f), new Vector3(0.25f, 0.75f));
        //addFakeTrail(new Vector3(0.25f, 0.75f), new Vector3(0.75f, 0.75f));
        //addFakeTrail(new Vector3(0.75f, 0.75f), new Vector3(0.75f, 0f));
        //deleteFakeTrail();
        //addFakeTrail(new Vector3(0.75f, 0.25f), new Vector3(0.75f, 0.75f));
        //deleteFakeTrail();
        //addFakeTrail(new Vector3(0.25f, 0.25f), new Vector3(0.75f, 0.25f));
        //deleteFakeTrail();
        //addFakeTrail(new Vector3(0.25f, 0.75f), new Vector3(0.75f, 0.75f));
        //deleteFakeTrail();
    }

    // Update is called once per frame
    void Update()
    {
        if (Utils.GAME_STOPPED) return;
        if (GameControler.status == GameControler.GameStatus.Waiting) return;
        if (GameControler.status == GameControler.GameStatus.Lose) return;
        if (GameControler.status == GameControler.GameStatus.Win) return;

        Background current_bg = GetBackground(transform.position);
        Border current_border = GetBorder(transform.position);

        Direction.direction new_direction = getInputDirection();

        // When moving on the exact fram just after being set on a border, we move when we should stop, save the movement and apply it at next frame
        if(mSavedDirection != Direction.None)
        {
            new_direction = mSavedDirection;
            mSavedDirection = Direction.direction.None;
        }
        if(mWaitOneFrame)
        {
            mSavedDirection = new_direction;
            new_direction = Direction.direction.None;
            mWaitOneFrame = false;
        }
        Debug.Log("new_direction : " + new_direction);
        Debug.Log("current_direction before : " + new_direction);
        updateDirection(new_direction);
        Debug.Log("current_direction after: " + mCurrentDirection);
        if (mTrailPoints.Count == 0)
        {
            setOnBorderSameDirection();
        }

        Background next_bg = getNextBackground();
        Border next_border = GetBorder(getNextPosition());
        Vector3 next_pos = getNextPosition();

        List<Border> current_borders = GetBorders(transform.position);
        List<Border> next_borders = GetBorders(next_pos);
        //if (current_bg != null)
        //{
        //    Debug.Log("Current BG : " + current_bg.name);
        //}
        //if (next_bg != null)
        //{
        //    Debug.Log("Next BG : " + next_bg.name);
        //}
        //if (current_border != null)
        //{
        //    Debug.Log("Current Border : " + current_border.name);
        //}
        //if (next_border != null)
        //{
        //    Debug.Log("Next border : " + next_border.name);
        //}

        //if(mCurrentDirection != Direction.direction.None && current_border != null && !current_borders.Contains(mLastBorder) && !current_borders.Contains(mLastLastBorder))
        //{
        //    setOnBorderOppositeDirection();
        //}

        if(current_border == null && current_bg != null && current_bg.hasEnemies())
        {
            if(next_border == null && mTrails.Count == 0)
            {
                addStartLine();
            }
            if (mDirectionUpdated)
            {
                addLine();
            }
        }
        else
        {
            if(mCurrentDirection == mCollidingDirection && mCollidingBorder != null)
            {
                setOnBorderOppositeDirection(mCollidingBorder);
                mCollidingBorder = null;
                mCollidingDirection = Direction.direction.None;
            }
            if (mCurrentDirection == Direction.direction.None)
            {
                deleteLine();
            }
        }


        //if (current_bg == null && next_bg == null && current_border != null && current_border != next_border && next_border == null)
        //{
        //    Debug.Log("1");
        //    if(isInScreen(next_pos))
        //    {
        //        Debug.Log("1.1");
        //        setOnBorderOppositeDirection();
        //        if(new_direction != Direction.direction.None)
        //        {
        //            Debug.Log("1.2");
        //            addLine();
        //        }
        //    }
        //}
        //else if ((!isInScreen(next_pos) && !mDirectionUpdated) || (next_bg != null && next_bg != current_bg && !next_bg.hasEnemies()))//onBorder() && !should_create_line)
        //{
        //    Debug.Log("2");
        //    if (mTrails.Count > 0)
        //    {
        //        Debug.Log("2.1");
        //        setOnBorderOppositeDirection(false);
        //    }
        //    deleteLine();
        //}
        //else if (mTrails.Count > 0 && lastTrailIntersectBorder())
        //{
        //    Debug.Log("3");
        //    setOnBorderOppositeDirection();
        //    deleteLine();
        //    Background new_next_bg = getNextBackground(); // We update the position
        //    if (new_next_bg != null && new_next_bg.hasEnemies())/* && GetBorder(next_pos) == null*/
        //    {
        //        Debug.Log("3.1");
        //        addLine();
        //    }
        //}
        //else
        //{
        //    if (mDirectionUpdated && (current_bg == null || current_bg.hasEnemies()) && next_border == null) // Leave border to create trail inside BG
        //    {
        //        Debug.Log("4");
        //        addLine();
        //    }
        //    else if (next_bg != null && next_bg != current_bg && next_bg.hasEnemies() && (current_bg  == null || current_bg != null && !current_bg.hasEnemies()))
        //    {
        //        Debug.Log("5");
        //        setOnBorderOppositeDirection();
        //        //addLine();
        //    }
        //}
        moveBall();

        // Delete line if crossing a border.
        //if(mTrails.Count > 0)
        //{
        //    GameObject trail_go = mTrails[mTrails.Count - 1];
        //    LineRenderer trail = trail_go.GetComponent<LineRenderer>();
        //    if(trail != null)
        //    {
        //        Vector3 trail_start_point = trail.GetPosition(0);
        //        for (int i = 0; i < mBorders.Count; ++i) // Use for instead of foreach to avoid exception due to mBorders updates when deleting line
        //        {
        //            Border border = mBorders[i];
        //
        //            //if ( !(border.mStartPoint.x == trail_start_point.x && border.mEndPoint.x == trail_start_point.x || border.mStartPoint.y == trail_start_point.y && border.mEndPoint.y == trail_start_point.y)
        //            //    && Utils.intersect(trail.GetPosition(0), trail.GetPosition(1), border.mStartPoint, border.mEndPoint))
        //            //{
        //            if (!border.contains(trail.GetPosition(0))
        //                && Utils.intersect(trail.GetPosition(0), trail.GetPosition(1), border.mStartPoint, border.mEndPoint))
        //            {
        //                Debug.Log("Stop");
        //                deleteLine();
        //                addLine();
        //                break;
        //            }
        //        }
        //    }
        //}

        // Hide Score
        //countScore();
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

    void addTrail()
    {
        GameObject current_trail = new GameObject();
        current_trail.gameObject.transform.parent = mTrailGO.gameObject.transform;
        current_trail.name = "Trail " + mTrails.Count.ToString();
        current_trail.AddComponent<Trail>();
        mTrails.Add(current_trail);
        StartCoroutine(waiterAddLine());
    }
    void addFakeTrail(Vector3 pos_1, Vector3 pos_2)
    {
        GameObject current_trail = new GameObject();
        current_trail.gameObject.transform.parent = mTrailGO.gameObject.transform;
        FakeTrail fake_trail = current_trail.AddComponent<FakeTrail>();
        fake_trail.setRelativeStartPoint(pos_1);
        fake_trail.setRelativeEndPoint(pos_2);
        mTrails.Add(current_trail);
    }
    void addStartLine()
    {
        if (!mCanAddLine)
            return;

        mLastBorderWhichCreateTrail = mLastBorder;
        GameObject current_point = new GameObject();
        current_point.gameObject.transform.parent = mTrailPointsGO.gameObject.transform;
        current_point.name = "Trail Points " + mTrailPoints.Count.ToString();

        if(Direction.isVertical(mCurrentDirection))
        {
            if(mLastBorder.isHorizontal())
            {
                current_point.transform.position = new Vector3(transform.position.x, mLastBorder.mStartPoint.y, 0);
            }
            else
            {
                if(Vector3.Distance(transform.position, mLastBorder.mStartPoint) < Vector3.Distance(transform.position, mLastBorder.mEndPoint))
                {
                    current_point.transform.position = mLastBorder.mStartPoint;
                }
                else
                {
                    current_point.transform.position = mLastBorder.mEndPoint;
                }
            }
        }
        else if(Direction.isHorizontal(mCurrentDirection))
        {
            if (mLastBorder.isVertical())
            {
                current_point.transform.position = new Vector3(mLastBorder.mStartPoint.x, transform.position.y, 0);
            }
            else
            {
                if (Vector3.Distance(transform.position, mLastBorder.mStartPoint) < Vector3.Distance(transform.position, mLastBorder.mEndPoint))
                {
                    current_point.transform.position = mLastBorder.mStartPoint;
                }
                else
                {
                    current_point.transform.position = mLastBorder.mEndPoint;
                }
            }
        }

        mTrailPoints.Add(current_point);
        addTrail();
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
        addTrail();
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
        if (GameControler.status == GameControler.GameStatus.Lose) return;
        if (GameControler.status == GameControler.GameStatus.Win) return;
        Border.mOnDeleteLine = true;
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
            line_to_border.mNewBorderOnDelete = true;
            addBorder(line_to_border);

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
                splitBackground(bg, bg_start_point, bg_end_point, line_to_border);
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

                if (Utils.checkNoBorderBetwennBackground(bg_1, bg_2, mBorders))
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
        for(int i = 0; i < mBackgrounds.Count; ++i)
        {
            Background bg = mBackgrounds[i];
            bool fused = bg.fuseBackgroundIfNeeded();
            if(fused)
            {
                i = 0;
            }
        }
        mLastBorderWhichCreateTrail = null;
        // Draw bg
        Border.mOnDeleteLine = false;
    }

    void deleteFakeTrail()
    {
        List<Background> deleted_bgs = new List<Background>();
        foreach (GameObject trail in mTrails)
        {
            FakeTrail trail_script = trail.GetComponent<FakeTrail>();
            LineRenderer lineRenderer = trail.GetComponent<LineRenderer>();

            Border line_to_border = Border.create(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1), true);
            addBorder(line_to_border);

            List<Vector3> points = Utils.getIntermediatePointFromTrail(line_to_border);
            List<Background> bgs = new List<Background>();
            foreach (Vector3 point in points)
            {
                Background bg = GetBackground(point);
                if (bg != null && !bgs.Contains(bg))
                {
                    bgs.Add(bg);
                }
            }

            foreach (Background bg in bgs)
            {
                Vector3 start_point = lineRenderer.GetPosition(0);
                Vector3 middle_point = lineRenderer.GetPosition(1);

                Vector3 bg_start_point = bg.getPointFromBackground(start_point, middle_point);
                Vector3 bg_end_point = bg.getPointFromBackground(middle_point, start_point);
                splitBackground(bg, bg_start_point, bg_end_point, line_to_border);
                deleted_bgs.Add(bg);
            }

            Destroy(trail);
        }
        mTrails.Clear();

        foreach (Background bg in mBackgrounds)
        {
            bg.mEnemyList.Clear();
        }

        foreach (Background bg_1 in mBackgrounds)
        {
            bg_1.mConnectedBackground.Clear();
            foreach (GameObject ennemy_to_reassign in mEnemies)
            {
                if (bg_1.containsEquals(ennemy_to_reassign.transform.position))
                {
                    bg_1.addEnemy(ennemy_to_reassign);
                }
            }
            Vector3 center_1 = bg_1.getCenterPoint();
            foreach (Background bg_2 in mBackgrounds)
            {
                if (bg_1 == bg_2)
                    continue;

                if (Utils.checkNoBorderBetwennBackground(bg_1, bg_2, mBorders))
                {
                    bg_1.addConnection(bg_2);
                }
            }
        }

        // Propagate connection
        Utils.propagateBackgrounds(mBackgrounds);

        // Add ennemis related to propagate
        foreach (Background bg in mBackgrounds)
        {
            bg.addConnectedEnemy();
            bg.changeBackgroundColor();
        }
        // Draw bg
        Border.mOnDeleteLine = false;
    }

    Direction.direction getInputDirection()
    {
        mInputDirection = mPressedDirection[mPressedDirection.Count - 1];
        if (mInputDirection != Direction.direction.None)
        {
            Direction.direction tmp = mInputDirection;
            mInputDirection = Direction.direction.None;
            if(tmp == Direction.direction.Stop && mTrailPoints.Count == 0)
            {
                return tmp;
            }
            else if (tmp == Direction.direction.Stop && mTrailPoints.Count != 0)
            {
                return Direction.None;
            }
            else if(tmp != Direction.direction.Stop)
            {
                return tmp;
            }

        }

        if (MobileButtons.mButtonHasBeenPressed)
        {
            MobileButtons.mButtonHasBeenPressed = false;
            return MobileButtons.mButtonDirectionPressed;
        }

        //if(Input.touchCount > 0)
        //{
        //
        //    Touch touch = Input.GetTouch(0);
        //    Vector3 touch_position = Camera.main.ScreenToWorldPoint(touch.position);
        //    touch_position.z = 0f;
        //
        //    if (!mFingerDown)
        //    {
        //        mFingerDown = true;
        //        mStartPositionFingerDown = touch_position;
        //    }
        //    else
        //    {
        //        mLastPositionFingerUp = touch_position;
        //    }
        //}
        //else
        //{
        //    mFingerDown = false;
        //    Vector3 swap = mLastPositionFingerUp - mStartPositionFingerDown;
        //    if(swap == Vector3.zero) return Direction.None;
        //
        //    if (Mathf.Abs(swap.x) > Mathf.Abs(swap.y)) // Handle horizontal
        //    {
        //        if (swap.x < 0)
        //        {
        //            return Direction.Left;
        //        }
        //        else
        //        {
        //            return Direction.Right;
        //        }
        //    }
        //    else
        //    {
        //        if (swap.y < 0)
        //        {
        //            return Direction.Down;
        //        }
        //        else
        //        {
        //            return Direction.Up;
        //        }
        //    }
        //}

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
            mCurrentDirection =  Direction.Left;
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
            mCurrentDirection =  Direction.Right;
            GetComponent<SpriteRenderer>().flipX = false;
            return true;
        }
        else if ((new_direction == Direction.Down && mCurrentDirection != Direction.Down) && (can_move_backward || mCurrentDirection != Direction.Up))
        {
            StartCoroutine(waiter());
            mPreviousDirection = mCurrentDirection;
            mCurrentDirection =  Direction.Down;
            return true;
        }
        /*else if (new_direction == Direction.None)
        {
            //mPreviousDirection = mCurrentDirection;
            mCurrentDirection = Direction.None;
            return true;
        }*/        
        else if (new_direction == Direction.Stop && can_move_backward)
        {
            mPreviousDirection = mCurrentDirection;
            mCurrentDirection = Direction.Stop;
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
    IEnumerator waiterOnSetBorder()
    {
        mPressedDirection.Add(Direction.Stop);
        yield return new WaitForSeconds(0.06f);
        for(int i = mPressedDirection.Count - 1; i > 0; --i)
        {
            if (mPressedDirection[i] != Direction.Stop) continue;
            mPressedDirection.RemoveAt(i);
        }
    }

    Background GetBackground(Vector3 pos)
    {
        foreach (Background bg in mBackgrounds)
        {
            if (bg.containsEquals(pos))
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
    Border GetFuzzyBorder(Vector3 pos)
    {
        foreach (Border border in mBorders)
        {
            if (border.onSmallFuzzyBorder(pos))
            {
                return border;
            }
        }
        return null;
    }
    List<Border> GetBorders(Vector3 pos)
    {
        List<Border> borders = new List<Border>();
        foreach (Border border in mBorders)
        {
            if (border.onSmallFuzzyBorder(pos))
            {
                borders.Add(border);
            }
        }
        return borders;
    }
    Border GetBorderWithPreferredDirection(Vector3 pos, bool prefer_same_direction)
    {
        List<Border> borders = new List<Border>();
        foreach (Border border in mBorders)
        {
            if (border.onSmallFuzzyBorder(pos))
            {
                borders.Add(border);
                
            }
        }
        if (borders.Count == 0) return null;
        if (borders.Count == 1) return borders[0];
        
        List<Border> border_direction = new List<Border>();
        foreach (Border border in mBorders)
        {
            if(prefer_same_direction 
                && (border.isVertical() && Direction.isVertical(mCurrentDirection)
                || border.isHorizontal() && Direction.isHorizontal(mCurrentDirection)))
            {
                return border;
            }
            else if(!prefer_same_direction 
                && (border.isVertical() && Direction.isHorizontal(mCurrentDirection)
                || border.isHorizontal() && Direction.isVertical(mCurrentDirection)))
            {
                return border;
            }
        }
        return borders[0];
    }

    void splitBackground(Background current_bg, Vector3 start_point, Vector3 end_point, Border split_border)
    {
        List<GameObject> background_splitten = current_bg.split(start_point, end_point);

        if (background_splitten == null || background_splitten.Count != 2) return;
        
        GameObject bg_1 = background_splitten[0];
        if(bg_1.transform.localScale.x != 0 && bg_1.transform.localScale.y != 0)
        {
            Background background_1 = bg_1.GetComponent<Background>();
            mBackgroundGameObjects.Add(bg_1);
            mBackgrounds.Add(background_1);
            background_1.split_border = split_border;
        }

        GameObject bg_2 = background_splitten[1];
        if (bg_2.transform.localScale.x != 0 && bg_2.transform.localScale.y != 0)
        {
            Background background_2 = bg_2.GetComponent<Background>();
            mBackgroundGameObjects.Add(bg_2);
            mBackgrounds.Add(background_2);
            background_2.split_border = split_border;
        }
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
                mLastLastBorder = mLastBorder;
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
    bool setOnBorderOppositeDirection(Border border)
    {
        if (mCurrentDirection == Direction.None) return false;

        Dictionary<Border, Vector3> border_points = new Dictionary<Border, Vector3>();

        if (border.isHorizontal() && Direction.isVertical(mCurrentDirection))
        {
            if (border.isLeftToRight() && (transform.position.x < border.mStartPoint.x || border.mEndPoint.x < transform.position.x)) return false;
            if (!border.isLeftToRight() && (transform.position.x < border.mEndPoint.x || border.mStartPoint.x < transform.position.x)) return false;
            if (mCollidingBorder == null && mCurrentDirection == Direction.direction.Up && transform.position.y > border.mStartPoint.y) return false;
            if (mCollidingBorder == null && mCurrentDirection == Direction.direction.Down && transform.position.y < border.mStartPoint.y) return false;

            Vector3 point = new Vector3(gameObject.transform.position.x, border.mStartPoint.y, 0);
            if (point == transform.position) return false;
            border_points.Add(border, point);
        }
        else if (border.isVertical() && Direction.isHorizontal(mCurrentDirection))
        {
            if (border.isBottomToTop() && (transform.position.y < border.mStartPoint.y || border.mEndPoint.y < transform.position.y)) return false;
            if (!border.isBottomToTop() && (transform.position.y < border.mEndPoint.y || border.mStartPoint.y < transform.position.y)) return false;
            if (mCollidingBorder == null && mCurrentDirection == Direction.direction.Left && transform.position.x < border.mStartPoint.x) return false;
            if (mCollidingBorder == null && mCurrentDirection == Direction.direction.Right && transform.position.x > border.mStartPoint.x) return false;

            Vector3 point = new Vector3(border.mStartPoint.x, gameObject.transform.position.y, 0);
            if (point == transform.position) return false;
            border_points.Add(border, point);
        }
        

        if (border_points.Count == 0)
        {
            return false;
        }
        mCurrentDirection = Direction.direction.None;
        transform.position = getClosestPoint(border_points);
        mWaitOneFrame = true;
        StartCoroutine(waiterOnSetBorder());
        return true;
    }
    void setOnBorderOppositeDirection(bool check_previous_direction = false)
    {
        Dictionary<Border, Vector3> border_points = new Dictionary<Border, Vector3>();
        if (mCurrentDirection == Direction.None) return;

        foreach (Border border in mBorders)
        {
            //if ((border == mLastBorder) && mTrailPoints.Count < 2) continue;
            
            if (border.onSmallFuzzyBorder(transform.position))
            {
                if (border.isHorizontal() && (Direction.isVertical(mCurrentDirection) || (check_previous_direction && Direction.isVertical(mPreviousDirection))))
                {
                    if (border.isLeftToRight() && (transform.position.x < border.mStartPoint.x || border.mEndPoint.x < transform.position.x)) continue;
                    if (!border.isLeftToRight() && (transform.position.x < border.mEndPoint.x || border.mStartPoint.x < transform.position.x)) continue;
                    if (mCurrentDirection == Direction.direction.Up && transform.position.y > border.mStartPoint.y) continue;
                    if (mCurrentDirection == Direction.direction.Down && transform.position.y < border.mStartPoint.y) continue;

                    Vector3 point = new Vector3(gameObject.transform.position.x, border.mStartPoint.y, 0);
                    if (point == transform.position) continue;
                    border_points.Add(border, point);
                }
                else if (border.isVertical() && (Direction.isHorizontal(mCurrentDirection) || (check_previous_direction && Direction.isHorizontal(mPreviousDirection))))
                {
                    if (border.isBottomToTop() && (transform.position.y < border.mStartPoint.y || border.mEndPoint.y < transform.position.y)) continue;
                    if (!border.isBottomToTop() && (transform.position.y < border.mEndPoint.y || border.mStartPoint.y < transform.position.y)) continue;
                    if (mCurrentDirection == Direction.direction.Left && transform.position.x < border.mStartPoint.x) continue;
                    if (mCurrentDirection == Direction.direction.Right && transform.position.x > border.mStartPoint.x) continue;

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
        mCurrentDirection = Direction.direction.None;
        transform.position = getClosestPoint(border_points);
        mWaitOneFrame = true;
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
            if (border.isHorizontal() && (Direction.isVertical(mCurrentDirection) || Direction.isVertical(mPreviousDirection) || mCurrentDirection == Direction.direction.None))
            {
                Vector3 point_on_border = new Vector3(gameObject.transform.position.x, border.mStartPoint.y, 0);
                if (border.contains(point_on_border) && Vector3.Distance(transform.position, point_on_border) < Vector3.Distance(transform.position, closest_point))
                {
                    closest_point = point_on_border;
                    //mLastBorder = border;
                }
            }
            else if (border.isVertical() && (Direction.isHorizontal(mCurrentDirection) || Direction.isHorizontal(mPreviousDirection) || mCurrentDirection == Direction.direction.None))
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
        {
            mCurrentDirection = Direction.direction.None;
            return new Vector3(mMinBorderPos.x, position.y, position.z);
        }
        if (position.x > mMaxBorderPos.x)
        {
            mCurrentDirection = Direction.direction.None;
            return new Vector3(mMaxBorderPos.x, position.y, position.z);
        }
        if (position.y < mMinBorderPos.y)
        {
            mCurrentDirection = Direction.direction.None;
            return new Vector3(position.x, mMinBorderPos.y, position.z);
        }
        if (position.y > mMaxBorderPos.y)
        {
            mCurrentDirection = Direction.direction.None;
            return new Vector3(position.x, mMaxBorderPos.y, position.z);
        }
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
        float speed = mSpeed;
        if (mInputStop && mTrailPoints.Count == 0)
        {
            speed = mSpeed/6f;
        }
        Vector3 new_pos = transform.position + mDirections[mCurrentDirection] * speed * Time.deltaTime;
        mPreviousPosition = transform.position;
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
    private Border mCollidingBorder = null;
    private Direction.direction mCollidingDirection = Direction.direction.None;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if(collision.CompareTag("VerticalBorder") || collision.CompareTag("HorizontalBorder"))
        {
            Border border = collision.GetComponent<Border>();
            if (border.isHorizontal() && Direction.isVertical(mCurrentDirection) 
                || border.isVertical() && Direction.isHorizontal(mCurrentDirection))
            {
                setOnBorderOppositeDirection(border);
            }
            if (mCollidingBorder != null) return;
            if (border.isHorizontal() && Direction.isHorizontal(mCurrentDirection) && !border.onLine(transform.position))
            {
                mCollidingBorder = border;
                if (transform.position.y > border.y())
                {
                    mCollidingDirection = Direction.direction.Down;
                    Debug.Log("Down on " + border.name);
                }
                else
                {
                    mCollidingDirection = Direction.direction.Up;
                    Debug.Log("Up on " + border.name);
                }
            }
            if (border.isVertical() && Direction.isVertical(mCurrentDirection) && !border.onLine(transform.position))
            {
                mCollidingBorder = border;
                if (transform.position.x > border.x())
                {
                    mCollidingDirection = Direction.direction.Left;
                    Debug.Log("Left on " + border.name);
                }
                else
                {
                    mCollidingDirection = Direction.direction.Right;
                    Debug.Log("Right on " + border.name);
                }
            }
            //setOnBorderOppositeDirection();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("VerticalBorder") || collision.CompareTag("HorizontalBorder"))
        {
            Border border = collision.GetComponent<Border>();
            if (border == mCollidingBorder)
            {
                mCollidingBorder = null;
                mCollidingDirection = Direction.direction.None;
            }
        }
    }

    public void clearPressedButton()
    {
        mPressedDirection.Clear();
        mPressedDirection.Add(Direction.Stop);
    }

}
