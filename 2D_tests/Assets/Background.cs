using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Background
{
    public GameObject mBackground;
    private int mId;
    public Vector3 mMinBorderPos;
    public Vector3 mMaxBorderPos;
    private List<GameObject> mEnnemyList;
    public List<Border> mBorders;
    float mEpsilon;
    public GameObject mCharacter;

    public Background(GameObject go)
    {
        mEnnemyList = new List<GameObject>();
        mBackground = GameObject.Instantiate(go);
        mId = 0;

        mBackground.transform.parent = GameObject.Find("Backgrounds").transform;
    }
    public Background(GameObject go, Vector3 min_border_pos, Vector3 max_border_pos, int _id)
    {
        mEnnemyList = new List<GameObject>();
        mBackground = GameObject.Instantiate(go);
        mId = _id;
        mMinBorderPos = min_border_pos;
        mMaxBorderPos = max_border_pos;
        mBackground.transform.position = (max_border_pos + min_border_pos) / 2;
        float scale_x = max_border_pos.x - min_border_pos.x;
        float scale_y = max_border_pos.y - min_border_pos.y;
        mBackground.transform.localScale = new Vector3(scale_x, scale_y, 1);
        mBackground.transform.parent = GameObject.Find("Backgrounds").transform;
        //mBackground.transform.rotation = Quaternion.Euler(90, 0, 0);
        SpriteRenderer render = mBackground.GetComponent<SpriteRenderer>();
        render.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        mBackground.name = "Background" + "_" + mId;

        render.material = new Material(Shader.Find("Sprites/Default"));
        render.material.color = render.color;

        GameObject ball = GameObject.Find("Ball");
        mEpsilon = ball.transform.localScale.x/2f;

        //mBorders = new List<Border>();
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

    public void setBorder(Vector3 min_border_pos, Vector3 max_border_pos)
    {
        mMinBorderPos = min_border_pos;
        mMaxBorderPos = max_border_pos;
    }
    public bool Fuzzycontains(Vector3 pos, float epsilon = -1f)
    {
        if(epsilon == -1f)
        {
            epsilon = mEpsilon;
        }
        return pos.x > mMinBorderPos.x - epsilon && pos.x < mMaxBorderPos.x + epsilon && pos.y > mMinBorderPos.y - epsilon && pos.y < mMaxBorderPos.y + epsilon;
    }
    public bool contains(Vector3 pos)
    {
        return pos.x > mMinBorderPos.x && pos.x < mMaxBorderPos.x && pos.y > mMinBorderPos.y && pos.y < mMaxBorderPos.y;
    }
    public bool contains(GameObject go)
    {
        return contains(go.transform.position);
    }
    public bool containsOrOnBorder(Vector3 pos)
    {
        return pos.x >= mMinBorderPos.x && pos.x <= mMaxBorderPos.x && pos.y >= mMinBorderPos.y && pos.y <= mMaxBorderPos.y;
    }
    public bool containsOrOnBorder(GameObject go)
    {
        return containsOrOnBorder(go.transform.position);
    }
    //public bool onFuzzyContainsOrOnBorder(Vector3 pos)
    //{
    //    return pos.x >= mMinBorderPos.x - mEpsilon && pos.x <= mMinBorderPos.x + mEpsilon
    //        && pos.x >= mMaxBorderPos.x - mEpsilon && pos.x <= mMaxBorderPos.x + mEpsilon 
    //        && pos.y >= mMinBorderPos.y - mEpsilon && pos.y <= mMinBorderPos.y + mEpsilon 
    //        && pos.y >= mMaxBorderPos.y - mEpsilon && pos.y <= mMaxBorderPos.y + mEpsilon;
    //}
    //public bool onFuzzyContainsOrOnBorder(GameObject go)
    //{
    //    return onFuzzyContainsOrOnBorder(go.transform.position);
    //}
    private bool fuzzyCompare(float val_1, float val_2, float epsilon)
    {
        return val_1 > val_2 - epsilon && val_1 < val_2 + epsilon;
    }
    public bool onFuzzyBorder(Vector3 pos)
    {
        return fuzzyCompare(pos.x, mMinBorderPos.x, mEpsilon) 
            || fuzzyCompare(pos.x, mMaxBorderPos.x, mEpsilon) 
            || fuzzyCompare(pos.y, mMinBorderPos.y, mEpsilon) 
            || fuzzyCompare(pos.y, mMaxBorderPos.y, mEpsilon);
    }
    public Border getFuzzyBorder(Vector3 pos)
    {
        //if (fuzzyCompare(pos.y, mMaxBorderPos.y, mEpsilon) && pos.x > mMinBorderPos.x && pos.x < mMaxBorderPos.x)
        //    return (mBorders.Count > 0) ? mBorders[0] : null;
        //if (fuzzyCompare(pos.x, mMaxBorderPos.x, mEpsilon) && pos.y > mMinBorderPos.y && pos.y < mMaxBorderPos.y)
        //    return (mBorders.Count > 1) ? mBorders[1] : null;
        //if (fuzzyCompare(pos.y, mMinBorderPos.y, mEpsilon) && pos.x > mMinBorderPos.x && pos.x < mMaxBorderPos.x)
        //    return (mBorders.Count > 2) ? mBorders[2] : null;
        //if (fuzzyCompare(pos.x, mMinBorderPos.x, mEpsilon) && pos.y > mMinBorderPos.y && pos.y < mMaxBorderPos.y)
        //    return (mBorders.Count > 3) ? mBorders[3] : null;
        return null;
    }
    public bool onBorder(Vector3 pos)
    {
        return pos.x == mMinBorderPos.x
            || pos.x == mMaxBorderPos.x
            || pos.y == mMinBorderPos.y
            || pos.y == mMaxBorderPos.y;
    }
    public Border getBorder(Vector3 pos)
    {
        if (pos.y == mMinBorderPos.y)
            return mBorders[0];
        if (pos.x == mMaxBorderPos.x)
            return mBorders[1];
        if (pos.y == mMaxBorderPos.y)
            return mBorders[2];
        if (pos.x == mMinBorderPos.x)
            return mBorders[4];
        return null;
    }
    public float getArea()
    {
        return (mMaxBorderPos.x - mMinBorderPos.x) * (mMaxBorderPos.y - mMinBorderPos.y);
    }
    public List<GameObject> getEnnemies()
    {
        return mEnnemyList;
    }
    public void addEnnemy(GameObject ennemy)
    {
        mEnnemyList.Add(ennemy);
    }
    public bool hasEnnemies()
    {
        return mEnnemyList.Count != 0;
    }
    public void removeBorder(Border border)
    {
        mBorders.Remove(border);
    }
    public void destroy()
    {
        foreach(Border border in mBorders)
        {
            border.Destroy();
            if(border.mDuplicateBorder != null)
            {
                Border duplicate_border = border.mDuplicateBorder;
                if(duplicate_border.mDuplicateBorder == null)
                {
                    continue;
                }
                //duplicate_border.mBackground.removeBorder(duplicate_border);
                duplicate_border.Destroy();
            }
        }
        GameObject.DestroyImmediate(mBackground);
    }

    public List<Background> split(Vector3 start_point, Vector3 end_point)
    {
        Background bg_1 = null;
        Background bg_2 = null;
        if (start_point.x == end_point.x)
        {
            bg_1 = new Background(mBackground, mMinBorderPos, (start_point.y == mMinBorderPos.y) ? end_point : start_point, mId + 1);
            bg_2 = new Background(mBackground, (start_point.y == mMaxBorderPos.y) ? end_point : start_point, mMaxBorderPos, mId + 2);
        }
        else if (start_point.y == end_point.y)
        {
            bg_1 = new Background(mBackground, mMinBorderPos, (start_point.x == mMinBorderPos.x) ? end_point : start_point, mId + 1);
            bg_2 = new Background(mBackground, (start_point.x == mMaxBorderPos.x) ? end_point : start_point, mMaxBorderPos, mId + 2);
        }

        if (bg_1 == null || bg_2 == null)
        {
            Debug.Log("Issue to split Background !");
            return null;
        }

        foreach (GameObject ennemy in getEnnemies())
        {
            if (bg_1.contains(ennemy))
            {
                bg_1.addEnnemy(ennemy);
            }
            else if (bg_2.contains(ennemy))
            {
                bg_2.addEnnemy(ennemy);
            }
            else
            {
                Debug.Log("Issue, ennemy not set in new background");
            }
        }

        List<Background> backgrounds = new List<Background>();

        backgrounds.Add(bg_1);
        backgrounds.Add(bg_2);

        destroy();
        handleDuplicateBorder(bg_1, bg_2);
        //hideCommonBorder(bg_1, bg_2);
        return backgrounds;
    }

    void handleDuplicateBorder(Background bg_1, Background bg_2)
    {
        foreach (Border border_bg_1 in bg_1.mBorders)
        {
            foreach (Border border_bg_2 in bg_2.mBorders)
            {
                if (isSameBorder(border_bg_1, border_bg_2))
                {
                    border_bg_1.mDuplicateBorder = border_bg_2;
                    border_bg_2.mDuplicateBorder = border_bg_1;
                    
                    border_bg_1.hideColliderBorder();
                }
            }
        }
    }
    public bool isSameBorder(Border border_1, Border border_2)
    {
        Vector3 line_start_point_1 = border_1.mStartPoint;
        Vector3 line_start_point_2 = border_2.mStartPoint;
        Vector3 line_end_point_1 = border_1.mEndPoint;
        Vector3 line_end_point_2 = border_2.mEndPoint;
        return (line_start_point_1 == line_start_point_2 && line_end_point_1 == line_end_point_2) || (line_start_point_1 == line_end_point_2 && line_start_point_2 == line_end_point_1);
    }

    void hideCommonBorder(Background bg_1, Background bg_2)
    {
        foreach (Border border_bg_1 in bg_1.mBorders)
        {
            if (border_bg_1.checkColliderAlreadyExist())
            {
                border_bg_1.hideColliderBorder();
            }
        }
        foreach (Border border_bg_2 in bg_2.mBorders)
        {
            if (border_bg_2.checkColliderAlreadyExist())
            {
                border_bg_2.hideColliderBorder();
            }
        }
    }

    //public Border findBorderWithGameobject(Background bg)
    //{
    //    foreach (Border border in bg.mBorders)
    //    {
    //        if (border.mBorder == go)
    //        {
    //            return border;
    //        }
    //    }
    //    return null;
    //}
}
