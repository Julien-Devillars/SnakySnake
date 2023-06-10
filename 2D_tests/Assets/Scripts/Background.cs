using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Background : MonoBehaviour
{
    private string mId;
    public Vector3 mMinBorderPos;
    public Vector3 mMaxBorderPos;
    public List<GameObject> mEnemyList;
    public List<Background> mConnectedBackground;

    public void Awake()
    {
        mEnemyList = new List<GameObject>();

        transform.parent = GameObject.Find("Backgrounds").transform;

        SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
        renderer.color = new Color(1f, 1f, 1f);
        renderer.material = new Material(Shader.Find("Sprites/Default"));

        renderer.material.color = renderer.color;
        renderer.sprite = Resources.Load<Sprite>("Sprites/Square");
        renderer.enabled = false;

        renderer.sortingLayerName = "Background";
        mConnectedBackground = new List<Background>();
    }

    public void Update()
    {
        fuseBackgroundIfNeeded();
    }
    public void init(Vector3 min_border_pos, Vector3 max_border_pos, string _id)
    {
        mId = _id;
        mMinBorderPos = min_border_pos;
        mMaxBorderPos = max_border_pos;
        transform.position = (max_border_pos + min_border_pos) / 2;
        float scale_x = max_border_pos.x - min_border_pos.x;
        float scale_y = max_border_pos.y - min_border_pos.y;
        transform.localScale = new Vector3(scale_x, scale_y, 1);
        gameObject.name = "Background" + "_" + mId;
    }
    public void initWithXScaleDivideBy2(Vector3 min_border_pos, Vector3 max_border_pos, string _id)
    {
        mId = _id;
        mMinBorderPos = min_border_pos;
        mMaxBorderPos = max_border_pos;
        transform.position = (max_border_pos + min_border_pos) / 2;
        float scale_x = max_border_pos.x - min_border_pos.x;
        float scale_y = max_border_pos.y - min_border_pos.y;
        transform.localScale = new Vector3(scale_x/2f, scale_y, 1);
        gameObject.name = "Background" + "_" + mId;
    }
    public void update()
    {
        transform.position = (mMaxBorderPos + mMinBorderPos) / 2;
        float scale_x = mMaxBorderPos.x - mMinBorderPos.x;
        float scale_y = mMaxBorderPos.y - mMinBorderPos.y;
        transform.localScale = new Vector3(scale_x/2f, scale_y, 1);
    }

    public void Clone(Background target)
    {
        target.mId = mId;
        target.mMinBorderPos = mMinBorderPos;
        target.mMaxBorderPos = mMaxBorderPos;
        target.mEnemyList = mEnemyList;
        target.mConnectedBackground = mConnectedBackground;
    }
    public void fuseBackgroundIfNeeded()
    {
        List<Background> backgrounds = Utils.getBackgrounds();
        Background background_found = null;
        if (hasEnemies()) return;

        foreach(Background background in backgrounds)
        {
            if (background == this) continue;
            if (string.Compare(background.gameObject.name, gameObject.name) != -1) continue;
            if (background.hasEnemies()) continue;

            if (mMaxBorderPos.x == background.mMinBorderPos.x && mMinBorderPos.y == background.mMinBorderPos.y && mMaxBorderPos.y == background.mMaxBorderPos.y)
            {
                mMaxBorderPos.x = background.mMaxBorderPos.x;
                background_found = background;
                break;
            }
            else if (mMinBorderPos.x == background.mMaxBorderPos.x && mMinBorderPos.y == background.mMinBorderPos.y && mMaxBorderPos.y == background.mMaxBorderPos.y)
            {
                mMinBorderPos.x = background.mMinBorderPos.x;
                background_found = background;
                break;
            }
            else if (mMaxBorderPos.y == background.mMinBorderPos.y && mMinBorderPos.x == background.mMinBorderPos.x && mMaxBorderPos.x == background.mMaxBorderPos.x)
            {
                mMaxBorderPos.y = background.mMaxBorderPos.y;
                background_found = background;
                break;
            }
            else if (mMinBorderPos.y == background.mMaxBorderPos.y && mMinBorderPos.x == background.mMinBorderPos.x && mMaxBorderPos.x == background.mMaxBorderPos.x)
            {
                mMinBorderPos.y = background.mMinBorderPos.y;
                background_found = background;
                break;
            }
        }
        if(background_found != null)
        {
            background_found.destroy();
            update();
            
        }
    }
    public void changeBackgroundColor()
    {
        SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
        if (hasEnemies())
        {
            render.material = new Material(Shader.Find("Sprites/Default"));
            render.color = new Color(0f, 0f, 0f, 0f);
            //render.color = new Color(0f, 1f, 0f);
        }
        else
        {
            if(Utils.SHADER_ON)
            {
                render.material = (Material)Resources.Load("Shaders/CoverBackground", typeof(Material));
                render.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                render.material = new Material(Shader.Find("Sprites/Default"));
                render.color = new Color(1f, 1f, 1f, 0.7f);
            }
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

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        character.mBackgrounds.Remove(this);
        character.mBackgroundGameObjects.Remove(gameObject);

        GameObject.DestroyImmediate(gameObject);
    }

    public List<GameObject> split(Vector3 start_point, Vector3 end_point)
    {
        GameObject bg_1 = null;
        GameObject bg_2 = null;
        if (start_point.x == end_point.x)
        {
            bg_1 = new GameObject();
            Background background_1 = bg_1.AddComponent<Background>();
            background_1.initWithXScaleDivideBy2(mMinBorderPos, (fuzzyCompare(start_point.y, mMinBorderPos.y)) ? end_point : start_point, mId + "_1");

            bg_2 = new GameObject();
            Background background_2 = bg_2.AddComponent<Background>();
            background_2.initWithXScaleDivideBy2((fuzzyCompare(start_point.y, mMaxBorderPos.y)) ? end_point : start_point, mMaxBorderPos, mId + "_2");
        }
        else if (start_point.y == end_point.y)
        {
            bg_1 = new GameObject();
            Background background_1 = bg_1.AddComponent<Background>();
            background_1.initWithXScaleDivideBy2(mMinBorderPos, (fuzzyCompare(start_point.x, mMinBorderPos.x)) ? end_point : start_point, mId + "_1");

            bg_2 = new GameObject();
            Background background_2 = bg_2.AddComponent<Background>();
            background_2.initWithXScaleDivideBy2((fuzzyCompare(start_point.x, mMaxBorderPos.x)) ? end_point : start_point, mMaxBorderPos, mId + "_2");
        }

        if (bg_1 == null || bg_2 == null)
        {
            Debug.Log("Issue to split Background !");
            return null;
        }

        List<GameObject> backgrounds = new List<GameObject>();

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
    public bool isHorizontalConnected(Background bg)
    {
        return mMinBorderPos.y == bg.mMaxBorderPos.y || mMaxBorderPos.y == bg.mMinBorderPos.y;
    }

    public bool isVerticalConnected(Background bg)
    {
        return mMinBorderPos.x == bg.mMaxBorderPos.x || mMaxBorderPos.x == bg.mMinBorderPos.x;
    }
}
