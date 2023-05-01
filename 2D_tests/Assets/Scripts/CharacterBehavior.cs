using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class CharacterBehavior : MonoBehaviour
{
    public float mSpeed;

    private Dictionary<Direction.direction, Vector3> mDirections;
    private Direction.direction mCurrentDirection;

    public List<Background> mBackgrounds;

    public List<Border> mBorders;
    private Vector3 mMinBorderPos;
    private Vector3 mMaxBorderPos;

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

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinBorderPos = new Vector3(-width, -height, 0);
        mMaxBorderPos = new Vector3(width, height, 0);

        transform.position = new Vector3(mMinBorderPos.x, mMinBorderPos.y, 0);
        mBorders = new List<Border>();
        makeBorders();

        mBackgrounds = new List<Background>();
        mBackgrounds.Add(new Background(mMinBorderPos, mMaxBorderPos, 0));

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
        mTrailGO.name = "Trail";
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
        bool direction_updated = updateDirection();

        bool should_create_line = false;
        should_create_line = isInBackground();
        

        if (onBorder() && !should_create_line)
        {
            if(mTrailPoints.Count > 0)
            {
                setOnBorder();
            }
            deleteLine();
        }
        else
        {
            if (direction_updated)
            {
                addLine();
            }
        }
        moveBall();
    }

    private bool isInBackground()
    {
        Vector3 current_pos = transform.position;
        Vector3 next_pos = current_pos + Direction.directions[mCurrentDirection] * transform.localScale.x;
        Background next_bg = GetBackground(next_pos);
        if(next_bg == null)
        {
            Debug.Log("Next move cannot find BG");
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
        foreach (GameObject trail in mTrails)
        {
            Destroy(trail);
        }
        mTrails.Clear();
    }

    bool updateDirection()
    {
        if ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)) && mCurrentDirection != Direction.Left /* && mCurrentDirection != Direction.Right*/)
        {
            mCurrentDirection = Direction.Left;
            Debug.Log("Left");
            return true;
        }
        else if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow) && mCurrentDirection != Direction.Up)/* && mCurrentDirection != Direction.Down*/)
        {
            mCurrentDirection = Direction.Up;
            Debug.Log("Up");
            return true;
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && mCurrentDirection != Direction.Right)/* && mCurrentDirection != Direction.Left*/)
        {
            mCurrentDirection = Direction.Right;
            Debug.Log("Right");
            return true;
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && mCurrentDirection != Direction.Down)/* && mCurrentDirection != Direction.Up*/)
        {
            mCurrentDirection = Direction.Down;
            Debug.Log("Down");
            return true;
        }
        return false;
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
            Debug.Log("Current BG not found!");
            return null;
        }
        return current_bg;
    }

    void splitBackground()
    {
        Background current_bg = GetBackground((getLastPosition() + transform.position)/2f);
        if (current_bg == null)
        {
            return;
        }
        if (!current_bg.hasEnemies())
        {
            Debug.Log("No enemy, no split intended");
            return;
        }

        List<Background> background_splitten = current_bg.split(getLastPosition(), transform.position);
        if (background_splitten == null || background_splitten.Count != 2)
        {
            return;
        }
        Background bg_1 = background_splitten[0];
        Background bg_2 = background_splitten[1];

        mBackgrounds.Remove(current_bg);
        mBackgrounds.Add(bg_1);
        mBackgrounds.Add(bg_2);
        if(!bg_1.hasEnemies())
        {
            Score.Instance.mCurrentScore += bg_1.getArea();
        }
        if (!bg_2.hasEnemies())
        {
            Score.Instance.mCurrentScore += bg_2.getArea();
        }
    }
    
    bool onBorder()
    {
        foreach (Border border in mBorders)
        {
            if(border.onFuzzyBorder(transform.position))
            {
                return true;
            }
        }
        return false;
    }

    void setOnBorder()
    {
        Vector3 pos = new Vector3();
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
