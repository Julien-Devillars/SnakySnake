using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    public float mSpeed;

    private Dictionary<Direction.direction, Vector3> mDirections;
    private Direction.direction mCurrentDirection;

    public List<Background> mBackgrounds;

    public List<Border> mBorders;
    private Vector3 mMinBorderPos;
    private Vector3 mMaxBorderPos;

    private List<GameObject> mEnnemies;

    private GameObject mCurrentTrail;
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

        mEnnemies = new List<GameObject>();
        GameObject ennemies_go = GameObject.Find("Ennemies"); 
        for (int i = 0; i < ennemies_go.transform.childCount; ++i)
        {
            GameObject ennemy = ennemies_go.transform.GetChild(i).gameObject;
            mEnnemies.Add(ennemy);
            mBackgrounds[0].addEnnemy(ennemy);
        }
        mBackgrounds[0].changeBackgroundColor();

        mLastPosition_go = new GameObject();
        mLastPosition_go.name = "start_point";
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
        if (onBorder())
        {
            updateDirection();
            if(mCurrentTrail)
            {
                setOnBorder();
            }
            deleteLine();
        }
        else
        {
            Background current_bg = GetBackground(transform.position);
            if(current_bg != null && current_bg.hasEnnemies())
            {
                if (!mCurrentTrail)
                {
                    createLine();
                }
            }
            else
            {
                updateDirection();
            }
        }

        moveBall();
    }

    void createLine()
    {
        mCurrentTrail = new GameObject();
        mCurrentTrail.AddComponent<Trail>();
    }
    void deleteLine()
    {
        if (mCurrentTrail == null)
            return;

        LineRenderer lineRenderer = mCurrentTrail.GetComponent<LineRenderer>();
        Vector3 point_middle_line = (lineRenderer.GetPosition(0) + lineRenderer.GetPosition(1))/2;
        Background bg = GetBackground(point_middle_line);
        if(bg.hasEnnemies())
        {
            Border line_to_border = new Border(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
            addBorder(line_to_border);
            splitBackground();
        }
        Destroy(mCurrentTrail);
        mCurrentTrail = null;
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

    Background GetBackground(Vector3 pos)
    {
        Background current_bg = null;
        foreach (Background bg in mBackgrounds)
        {
            if (bg.contains((getLastPosition() + transform.position) / 2))
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
        Background current_bg = GetBackground(getLastPosition() + transform.position);
        if (current_bg == null)
        {
            return;
        }
        if (!current_bg.hasEnnemies())
        {
            Debug.Log("No ennemy, no split intended");
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
        if(!bg_1.hasEnnemies())
        {
            Score.Instance.mCurrentScore += bg_1.getArea();
        }
        if (!bg_2.hasEnnemies())
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
