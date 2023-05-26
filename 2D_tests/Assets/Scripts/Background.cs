using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Background
{
    public GameObject mBackground;
    private string mId;
    public Vector3 mMinBorderPos;
    public Vector3 mMaxBorderPos;
    public List<GameObject> mEnemyList;
    public GameObject mCharacter;
    public List<Background> mConnectedBackground;

    public Background(Vector3 min_border_pos, Vector3 max_border_pos, string _id)
    {
        mEnemyList = new List<GameObject>();
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
        //renderer.material = (Material)Resources.Load("Materials/BackgroundTransparent", typeof(Material));
        renderer.material.color = renderer.color;
        renderer.sprite = Resources.Load<Sprite>("Sprites/Square");

        renderer.sortingLayerName = "Background";
        mConnectedBackground = new List<Background>();
    }
    public void Clone(Background target)
    {
        target.mBackground = mBackground;
        target.mId = mId;
        target.mMinBorderPos = mMinBorderPos;
        target.mMaxBorderPos = mMaxBorderPos;
        target.mEnemyList = mEnemyList;
        target.mCharacter = mCharacter;
        target.mConnectedBackground = mConnectedBackground;
}
    public void changeBackgroundColor()
    {
        SpriteRenderer render = mBackground.GetComponent<SpriteRenderer>();
        if (hasEnemies())
        {
            render.material = new Material(Shader.Find("Sprites/Default"));
            render.color = new Color(0f, 0f, 0f, 0f);
            //render.color = new Color(0f, 1f, 0f);
        }
        else
        {
            render.material = (Material)Resources.Load("Shaders/CoverBackground", typeof(Material));
            render.color = new Color(1f,  1f, 1f, 1f);
            //render.color = new Color(0f, 1f, 1f);
        }
        render.material.color = render.color;
    }

    public bool contains(Vector3 pos)
    {
        return pos.x > mMinBorderPos.x && pos.x < mMaxBorderPos.x && pos.y > mMinBorderPos.y && pos.y < mMaxBorderPos.y;
    }
    public bool containsEquals(Vector3 pos)
    {
        return pos.x >= mMinBorderPos.x && pos.x <= mMaxBorderPos.x && pos.y >= mMinBorderPos.y && pos.y <= mMaxBorderPos.y;
    }
    public bool contains(GameObject go)
    {
        return contains(go.transform.position);
    }

    private bool fuzzyCompare(float val_1, float val_2)
    {
        return val_1 > val_2 - Utils.EPSILON() && val_1 < val_2 + Utils.EPSILON();
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
    public List<GameObject> getEnemies()
    {
        return mEnemyList;
    }
    public void addConnectedEnemy()
    {
        foreach(Background bg in mConnectedBackground)
        {
            foreach(GameObject enemy in bg.mEnemyList)
            {
                addEnemy(enemy);
            }
        }
        
    }
    public bool addEnemy(GameObject enemy)
    {
        if (!mEnemyList.Contains(enemy))
        {
            mEnemyList.Add(enemy);
            return true;
        }
        return false;
    }
    public bool hasEnemies()
    {
        return mEnemyList.Count != 0;
    }
    public void destroy()
    {
        foreach (Background connected_bg in mConnectedBackground)
        {
            connected_bg.mConnectedBackground.Remove(this);
        }
        GameObject.DestroyImmediate(mBackground);
    }

    public List<Background> split(Vector3 start_point, Vector3 end_point)
    {
        Background bg_1 = null;
        Background bg_2 = null;
        if (start_point.x == end_point.x)
        {
            bg_1 = new Background(mMinBorderPos, (fuzzyCompare(start_point.y, mMinBorderPos.y)) ? end_point : start_point, mId + "_1");
            bg_2 = new Background((fuzzyCompare(start_point.y, mMaxBorderPos.y)) ? end_point : start_point, mMaxBorderPos, mId + "_2");
        }
        else if (start_point.y == end_point.y)
        {
            bg_1 = new Background(mMinBorderPos, (fuzzyCompare(start_point.x, mMinBorderPos.x)) ? end_point : start_point, mId + "_1");
            bg_2 = new Background((fuzzyCompare(start_point.x, mMaxBorderPos.x)) ? end_point : start_point, mMaxBorderPos, mId + "_2");
        }

        if (bg_1 == null || bg_2 == null)
        {
            Debug.Log("Issue to split Background !");
            return null;
        }

        //foreach (GameObject enemy in getEnemies())
        //{
        //    if (bg_1.contains(enemy))
        //    {
        //        bg_1.addEnemy(enemy);
        //    }
        //    else if (bg_2.contains(enemy))
        //    {
        //        bg_2.addEnemy(enemy);
        //    }
        //    else
        //    {
        //        Debug.Log("Issue, enemy not set in new background");
        //    }
        //}

        //bg_1.changeBackgroundColor();
        //bg_2.changeBackgroundColor();

        List<Background> backgrounds = new List<Background>();

        backgrounds.Add(bg_1);
        backgrounds.Add(bg_2);

        destroy();
        return backgrounds;
    }

    public Vector3 getPointFromBackground(Vector3 start_point, Vector3 end_point)
    {
        Vector3 direction = end_point - start_point;
        direction = direction.normalized;

        if (direction == Vector3.up)
        {
            return new Vector3(start_point.x, mMaxBorderPos.y);
        }
        else if (direction == Vector3.right)
        {
            return new Vector3(mMaxBorderPos.x, start_point.y);
        }
        else if (direction == Vector3.down)
        {
            return new Vector3(start_point.x, mMinBorderPos.y);
        }
        else if (direction == Vector3.left)
        {
            return new Vector3(mMinBorderPos.x, start_point.y);
        }

        return Vector3.zero;
    }

    public Vector3 getCenterPoint()
    {
        return (mMinBorderPos + mMaxBorderPos) / 2f;
    }
    public bool addConnection(Background bg)
    {
        if (!mConnectedBackground.Contains(bg) && this != bg)
        {
            mConnectedBackground.Add(bg);
            return true;
        }
        return false;
    }

}
