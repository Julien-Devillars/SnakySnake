using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveOthogonal : MonoBehaviour
{
    public float mSpeed;

    enum Direction
    {
        Left,
        Up,
        Right,
        Down,
        None
    }
    private Dictionary<Direction, Vector3> mDirections;
    private Direction mCurrentDirection;
    private Border mCurrentBorder;


    public GameObject mBackground;
    public List<Background> mBackgrounds;

    public List<Border> mBorders;
    public GameObject mBorder;
    private Vector3 mMinBorderPos;
    private Vector3 mMaxBorderPos;


    public GameObject mEnnemy;
    private List<GameObject> mEnnemies;

    public GameObject mTrail;
    private GameObject mCurrentTrail;
    private Vector3 mLastPosition;

    // Start is called before the first frame update
    void Start()
    {
        mCurrentDirection = Direction.None;

        mDirections = new Dictionary<Direction, Vector3>();
        mDirections.Add(Direction.Left, Vector3.left);
        mDirections.Add(Direction.Up, Vector3.up);
        mDirections.Add(Direction.Right, Vector3.right);
        mDirections.Add(Direction.Down, Vector3.down);
        mDirections.Add(Direction.None, Vector3.zero);

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinBorderPos = new Vector3(-width, -height, 0);
        mMaxBorderPos = new Vector3(width, height, 0);

        transform.position = new Vector3(mMinBorderPos.x, mMinBorderPos.y, 0);

        //mBackgrounds = new List<Background>();
        //mBackgrounds.Add(new Background(mBackground, mMinBorderPos, mMaxBorderPos, 0));
        //mCurrentBorder = mBackgrounds[0].getFuzzyBorder(transform.position);

        //mEnnemies = new List<GameObject>();
        //for (int i = 0; i < mEnnemy.transform.childCount; ++i)
        //{
        //    GameObject ennemy = mEnnemy.transform.GetChild(i).gameObject;
        //    mEnnemies.Add(ennemy);
        //    mBackgrounds[0].addEnnemy(ennemy);
        //}
        mBorders = new List<Border>();
        makeBorders();
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

        string bg_number = mBackground.name.Replace("Background_", "");
        top.setName("Border_" + bg_number + "_top");
        right.setName("Border_" + bg_number + "_right");
        bot.setName("Border_" + bg_number + "_bot");
        left.setName("Border_" + bg_number + "_left");

        mBorders.Add(top);
        mBorders.Add(right);
        mBorders.Add(bot);
        mBorders.Add(left);
    }

    // Update is called once per frame
    void Update()
    {
        if (onBorder())
        {
            updateDirection();
            if(mCurrentTrail)
            {
                setOnBorder();
                //splitBackground();
            }
            deleteLine();
        }
        else
        {
            if (!mCurrentTrail)
            {
                createLine();
            }
            updateLine();
        }

        moveBall();
        //Debug.Log(transform.position);
    }

    void createLine()
    {
        mCurrentTrail = GameObject.Instantiate(mTrail);
        //For creating line renderer object
        LineRenderer lineRenderer = mCurrentTrail.GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = transform.localScale.x;
        lineRenderer.endWidth = transform.localScale.x;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
    }
    void deleteLine()
    {
        if (mCurrentTrail == null)
            return;

        LineRenderer lineRenderer = mCurrentTrail.GetComponent<LineRenderer>();
        Border line_to_border = new Border(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
        mBorders.Add(line_to_border);
        Destroy(mCurrentTrail);
        //lineRenderer.startColor = Color.white;
        //lineRenderer.endColor = Color.white;
        mCurrentTrail = null;
    }
    void updateLine()
    {
        LineRenderer lineRenderer = mCurrentTrail.GetComponent<LineRenderer>();
        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.SetPosition(0, mLastPosition); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, transform.position); //x,y and z position of the end point of the line
    }

    void splitBackground()
    {
        Background current_bg = null;
        foreach(Background bg in mBackgrounds)
        {
            if (bg.contains((mLastPosition + transform.position)/2))
            {
                current_bg = bg;
            }
        }
        if(current_bg == null)
        {
            Debug.Log("Current BG not found!");
            return;
        }
        if(!current_bg.hasEnnemies())
        {
            Debug.Log("No ennemy, no split intended");
            return;
        }

        List<Background> background_splitten = current_bg.split(mLastPosition, transform.position);
        if(background_splitten == null || background_splitten.Count != 2)
        {
            return;
        }
        Background bg_1 = background_splitten[0];
        Background bg_2 = background_splitten[1];

        mBackgrounds.Remove(current_bg);
        mBackgrounds.Add(bg_1);
        mBackgrounds.Add(bg_2);
    }

    void updateDirection()
    {
        if ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))/* && mCurrentDirection != Direction.Right*/)
        {
            mCurrentDirection = Direction.Left;
         }
        else if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))/* && mCurrentDirection != Direction.Down*/)
        {
            mCurrentDirection = Direction.Up;
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))/* && mCurrentDirection != Direction.Left*/)
        {
            mCurrentDirection = Direction.Right;
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))/* && mCurrentDirection != Direction.Up*/)
        {
            mCurrentDirection = Direction.Down;
        }
    }

    bool movingVertical()
    {
        return mCurrentDirection == Direction.Up || mCurrentDirection == Direction.Down;
    }
    bool movingHorizontal()
    {
        return mCurrentDirection == Direction.Left || mCurrentDirection == Direction.Right;
    }

    //bool onBorder()
    //{
    //    if (mCurrentBorder.mBorder.tag == "VerticalBorder" && movingVertical())
    //    {
    //
    //    }
    //    else if(mCurrentBorder.mBorder.tag == "VerticalBorder" && movingVertical())
    //    {
    //
    //    }
    //    return true;
    //}
    
    bool onBorder()
    {
        foreach (Border border in mBorders)
        {
            if(border.onFuzzyBorder(transform.position))
            {
                if (mCurrentTrail)
                {
                    if (border.mBorder.tag == "VerticalBorder")
                    {
                        mCurrentDirection = Direction.None;
                    }
                    else if (border.mBorder.tag == "HorizontalBorder")
                    {
                        mCurrentDirection = Direction.None;
                    }
                }
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
                    Debug.Log("VerticalBorder : " + border.mBorder.name + " : " + pos);
                }
                else if (border.mBorder.tag == "HorizontalBorder")
                {
                    pos = new Vector3(gameObject.transform.position.x, border.mStartPoint.y, 0);
                    Debug.Log("HorizontalBorder : " + border.mBorder.name + " : " + pos);
                }
            }
        }
        transform.position = pos;
    }

    Vector3 getFuzzyPositionInBorder(Vector3 position)
    {
        foreach (Background bg in mBackgrounds)
        {
            if (bg.onFuzzyBorder(transform.position) && !bg.onBorder(transform.position))
            {
                setOnBorder();
            }
            else if (bg.onFuzzyBorder(transform.position))
            {
                Border border = bg.getFuzzyBorder(transform.position);
                if (border == null || border.mBorder == null)
                {
                    continue;
                }

                if (border.mBorder.tag == "VerticalBorder" && (mCurrentDirection == Direction.Up || mCurrentDirection == Direction.Down))
                {
                    return new Vector3(border.mStartPoint.x, gameObject.transform.position.y, 0);
                }
                else if (border.mBorder.tag == "HorizontalBorder" && (mCurrentDirection == Direction.Left || mCurrentDirection == Direction.Right))
                {
                    return new Vector3(gameObject.transform.position.x, border.mStartPoint.y, 0);
                }
            }
        }
        return position;
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
                mLastPosition = transform.position;
            }
        }
        Vector3 new_pos = transform.position + mDirections[mCurrentDirection] * mSpeed * Time.deltaTime;
        transform.position = getPositionInBorder(new_pos);
        //transform.position = getFuzzyPositionInBorder(new_pos);
    }

}
