using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Vector2 speed;
    protected Vector3 mMinPos;
    protected Vector3 mMaxPos;
    protected Vector3 mFullMinPos;
    protected Vector3 mFullMaxPos;
    protected bool mHasCollideVertical;
    protected bool mHasCollideHorizontal;
    public EnemyType type;
    public bool mCanCrossBorder = false;
    protected void Awake()
    {
        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinPos = new Vector3(-width + Utils.EPSILON() * 2f, -height + Utils.EPSILON() * 2f, 0);
        mMaxPos = new Vector3(width - Utils.EPSILON() * 2f, height - Utils.EPSILON() * 2f, 0);
        mFullMinPos = new Vector3(-width, -height, 0);
        mFullMaxPos = new Vector3(width, height, 0);
        mHasCollideVertical = false;
        mHasCollideHorizontal = false;
    }
    protected void Start()
    {
        Physics2D.IgnoreLayerCollision(7, 7, true);
        type = EnemyType.Basic;
    }
    protected void FixedUpdate()
    {
        if (Utils.GAME_STOPPED) return;
        if (GameControler.status == GameControler.GameStatus.Waiting) return;
        gameObject.transform.Translate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, 0);
    }

    protected void Lose()
    {
        CameraHandler.mTargetPosition = transform.position;
        GameControler.status = GameControler.GameStatus.Lose;
        if (Utils.HAS_LOSE)
        {
            if (GameControler.type == GameControler.GameType.Infinity)
            {
                if (InfinityControler.mCurrentLevel > 1)
                {
                    ES3.Save("Infinity_HighScore_" + InfinityControler.mDifficulty.ToString(), InfinityControler.mCurrentLevel);

                    InfinityControler.mCurrentLevel = 1;
                }
                SceneManager.LoadScene("InfinityLevel");

            }


            if (GameControler.type == GameControler.GameType.Play)
            {
                //ES3.Save<int>($"Play_Level_{GameControler.currentLevel}_HighScore", (int)Score.Instance.mCurrentPercentScore);
                //SceneManager.LoadScene("PlayLevel");
                CameraHandler.mTargetPosition = transform.position;
                GameControler.status = GameControler.GameStatus.Lose;
            }
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Border"))
        {
            if (!mHasCollideVertical && collision.gameObject.tag == "VerticalBorder")
            {
                StartCoroutine(waiterColliderVertical());
                speed.x = -speed.x;
            }
            if (!mHasCollideHorizontal && collision.gameObject.tag == "HorizontalBorder")
            {
                StartCoroutine(waiterColliderHorizontal());
                speed.y = -speed.y;
            }
        }
        if (collision.gameObject.tag.Contains("Trail"))
        {
            Lose();
        }
    }
    IEnumerator waiterColliderVertical()
    {
        mHasCollideVertical = true;
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        mHasCollideVertical = false;
    }
    IEnumerator waiterColliderHorizontal()
    {
        mHasCollideHorizontal = true;
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        mHasCollideHorizontal = false;
    }

    public void setRandomDirection()
    {
        speed.x = Random.Range(-10f, 10f);
        speed.y = Random.Range(-10f, 10f);
    }
    public void setDirection(Vector2 direction)
    {
        speed = direction;
    }
    public void setScale(float scale)
    {
        transform.localScale = new Vector3(scale / 2f, scale / 2f, transform.localScale.z);

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinPos = new Vector3(-width + scale * 2f, -height + scale * 2f, 0);
        mMaxPos = new Vector3(width - scale * 2f, height - scale * 2f, 0);
    }

    public bool checkPositionIsValid(Vector2 pos)
    {
        float scale = transform.localScale.x;

        return pos.x > mFullMinPos.x + scale
            && pos.x < mFullMaxPos.x - scale
            && pos.y > mFullMinPos.x + scale
            && pos.y < mFullMaxPos.y - scale;
    }

    public void setDefaultPosition()
    {
        float x = (mMinPos.x + mMaxPos.x) / 2f;
        float y = (mMinPos.y + mMaxPos.y) / 2f;
        transform.position = new Vector3(x, y, 0);
    }
    public void setRandomPosition()
    {
        float x = Random.Range(mMinPos.x, mMaxPos.x);
        float y = Random.Range(mMinPos.y, mMaxPos.y);
        Vector2 pos = new Vector2(x, y);

        while (!checkPositionIsValid(pos))
        {
            x = Random.Range(mMinPos.x, mMaxPos.x);
            y = Random.Range(mMinPos.y, mMaxPos.y);
            pos = new Vector2(x, y);
        }

        transform.position = pos;
    }

    public void setPosition(Vector2 pos)
    {
        if (checkPositionIsValid(pos))
        {
            transform.position = pos;
        }
        else
        {
            Debug.Log($"Ennemy position not valid : {pos}");
            setDefaultPosition();
        }
    }
    public void setRelativePosition(Vector2 relative_pos)
    {
        float x = mFullMinPos.x * (1 - relative_pos.x) + mFullMaxPos.x * relative_pos.x;
        float y = mFullMinPos.y * (1 - relative_pos.y) + mFullMaxPos.y * relative_pos.y;
        Vector2 absolute_pos = new Vector2(x, y);
        setPosition(absolute_pos);
    }
    public void setPosition(float x, float y)
    {
        Vector2 pos = new Vector2(x, y);
        setPosition(pos);
    }

}
