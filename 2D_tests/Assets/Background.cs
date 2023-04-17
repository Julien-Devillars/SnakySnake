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
    //public List<Border> mBorders;
    float mEpsilon;
    public GameObject mCharacter;

    public Background(Vector3 min_border_pos, Vector3 max_border_pos, int _id)
    {
        mEnnemyList = new List<GameObject>();
        mBackground = new GameObject();

        mId = _id;
        mMinBorderPos = min_border_pos;
        mMaxBorderPos = max_border_pos;
        mBackground.transform.position = (max_border_pos + min_border_pos) / 2;
        float scale_x = max_border_pos.x - min_border_pos.x;
        float scale_y = max_border_pos.y - min_border_pos.y;
        mBackground.transform.localScale = new Vector3(scale_x, scale_y, 1);
        mBackground.transform.parent = GameObject.Find("Backgrounds").transform;

        SpriteRenderer renderer = mBackground.AddComponent<SpriteRenderer>();
        //renderer.sprite = Sprite.
        renderer.color = new Color(1f, 1f, 1f);

        mBackground.name = "Background" + "_" + mId;

        renderer.material = new Material(Shader.Find("Sprites/Default"));
        renderer.material.color = renderer.color;
        renderer.sprite = Resources.Load<Sprite>("Sprites/Square");

        renderer.sortingLayerName = "Background";

        GameObject ball = GameObject.Find("Ball");
        mEpsilon = ball.transform.localScale.x / 2f;
    }
    public void changeBackgroundColor()
    {
        SpriteRenderer render = mBackground.GetComponent<SpriteRenderer>();
        if (hasEnnemies())
        {
            render.color = new Color(0f, 1f, 0f);
        }
        else
        {
            render.color = new Color(0f, 1f, 1f);
        }
        render.material = new Material(Shader.Find("Sprites/Default"));
        render.material.color = render.color;
    }

    public bool contains(Vector3 pos)
    {
        return pos.x > mMinBorderPos.x && pos.x < mMaxBorderPos.x && pos.y > mMinBorderPos.y && pos.y < mMaxBorderPos.y;
    }
    public bool contains(GameObject go)
    {
        return contains(go.transform.position);
    }

    private bool fuzzyCompare(float val_1, float val_2)
    {
        return val_1 > val_2 - mEpsilon && val_1 < val_2 + mEpsilon;
    }
    public bool onBorder(Vector3 pos)
    {
        return pos.x == mMinBorderPos.x
            || pos.x == mMaxBorderPos.x
            || pos.y == mMinBorderPos.y
            || pos.y == mMaxBorderPos.y;
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
    public void destroy()
    {
        GameObject.DestroyImmediate(mBackground);
    }

    public List<Background> split(Vector3 start_point, Vector3 end_point)
    {
        Background bg_1 = null;
        Background bg_2 = null;
        if (start_point.x == end_point.x)
        {
            bg_1 = new Background(mMinBorderPos, (fuzzyCompare(start_point.y, mMinBorderPos.y)) ? end_point : start_point, mId + 1);
            bg_2 = new Background((fuzzyCompare(start_point.y, mMaxBorderPos.y)) ? end_point : start_point, mMaxBorderPos, mId + 2);
        }
        else if (start_point.y == end_point.y)
        {
            bg_1 = new Background(mMinBorderPos, (fuzzyCompare(start_point.x, mMinBorderPos.x)) ? end_point : start_point, mId + 1);
            bg_2 = new Background((fuzzyCompare(start_point.x, mMaxBorderPos.x)) ? end_point : start_point, mMaxBorderPos, mId + 2);
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

        bg_1.changeBackgroundColor();
        bg_2.changeBackgroundColor();

        List<Background> backgrounds = new List<Background>();

        backgrounds.Add(bg_1);
        backgrounds.Add(bg_2);

        destroy();
        return backgrounds;
    }
}
