using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background
{
    public GameObject mBackground;
    private Vector3 mMinBorderPos;
    private Vector3 mMaxBorderPos;
    private List<GameObject> mEnnemyList;

    public Background(GameObject go)
    {
        mEnnemyList = new List<GameObject>();
        mBackground = GameObject.Instantiate(go);

        mBackground.transform.parent = GameObject.Find("Backgrounds").transform;
    }
    public Background(GameObject go, Vector3 min_border_pos, Vector3 max_border_pos)
    {
        mEnnemyList = new List<GameObject>();
        mBackground = GameObject.Instantiate(go);
        mMinBorderPos = min_border_pos;
        mMaxBorderPos = max_border_pos;
        mBackground.transform.position = (max_border_pos + min_border_pos)/2;
        float scale_x = max_border_pos.x - min_border_pos.x;
        float scale_y = max_border_pos.y - min_border_pos.y;
        mBackground.transform.localScale = new Vector3(scale_x, scale_y, 0);
        mBackground.transform.parent = GameObject.Find("Backgrounds").transform;
    }
    public void setBorder(Vector3 min_border_pos, Vector3 max_border_pos)
    {
        mMinBorderPos = min_border_pos;
        mMaxBorderPos = max_border_pos;
    }
}

public class CharacterMoveOthogonal : MonoBehaviour
{
    public float mSpeed;

    enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }
    private Dictionary<Direction, Vector3> mDirections;
    private Direction mCurrentDirection;


    public GameObject mBackground;
    private List<Background> mBackgrounds;

    public GameObject mBorder;
    private List<GameObject> mBorders;
    private Vector3 mMinBorderPos;
    private Vector3 mMaxBorderPos;

    public GameObject mTrail;
    private GameObject mCurrentTrail;
    private Vector3 mLastPosition;

    // Start is called before the first frame update
    void Start()
    {
        mCurrentDirection = Direction.Right;

        mDirections = new Dictionary<Direction, Vector3>();
        mDirections.Add(Direction.Left, Vector3.left);
        mDirections.Add(Direction.Up, Vector3.up);
        mDirections.Add(Direction.Right, Vector3.right);
        mDirections.Add(Direction.Down, Vector3.down);

        mBorders = new List<GameObject>();
        mMinBorderPos = new Vector3(0, 0, 0);
        mMaxBorderPos = new Vector3(0, 0, 0);
        for (int i = 0; i < mBorder.transform.childCount; ++i)
        {
            GameObject border = mBorder.transform.GetChild(i).gameObject;
            mBorders.Add(border);
            Collider2D collider = border.GetComponent<Collider2D>();
            Vector3 center = collider.bounds.center;
            if (center.x < mMinBorderPos.x) mMinBorderPos.x = center.x;
            if (center.x > mMaxBorderPos.x) mMaxBorderPos.x = center.x;
            if (center.y < mMinBorderPos.y) mMinBorderPos.y = center.y;
            if (center.y > mMaxBorderPos.y) mMaxBorderPos.y = center.y;
        }
        transform.position = new Vector3(mMinBorderPos.x, mMinBorderPos.y, 0);

        mBackgrounds = new List<Background>();
        mBackgrounds.Add(new Background(mBackground, mMinBorderPos, mMaxBorderPos));
    }

    // Update is called once per frame
    void Update()
    {
        if (onBorder())
        {
            updateDirection();
            if(mCurrentTrail)
            {
                splitBackground();
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
    }

    void createLine()
    {
        mCurrentTrail = GameObject.Instantiate(mTrail);
        mLastPosition = transform.position;
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
        Destroy(mCurrentTrail);
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
        
        //mLastPosition;
        //transform.position;
    }

    void updateDirection()
    {
        if ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)) && mCurrentDirection != Direction.Right)
        {
            mCurrentDirection = Direction.Left;
        }
        else if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow)) && mCurrentDirection != Direction.Down)
        {
            mCurrentDirection = Direction.Up;
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && mCurrentDirection != Direction.Left)
        {
            mCurrentDirection = Direction.Right;
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && mCurrentDirection != Direction.Up)
        {
            mCurrentDirection = Direction.Down;
        }
    }

    bool onBorder()
    {
        if (transform.position.x == mMinBorderPos.x || transform.position.x == mMaxBorderPos.x)
            return true;
        if (transform.position.y == mMinBorderPos.y || transform.position.y == mMaxBorderPos.y)
            return true;
        return false;
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
        Vector3 new_pos = transform.position + mDirections[mCurrentDirection] * mSpeed * Time.deltaTime;
        transform.position = getPositionInBorder(new_pos);
    }


}
