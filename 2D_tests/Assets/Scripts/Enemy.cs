using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Vector3 speed;
    protected Vector3 mMinPos;
    protected Vector3 mMaxPos;
    protected bool mHasCollideVertical;
    protected bool mHasCollideHorizontal;
    public string mSprite = "";
    public string mMaterial = "";
    public Rigidbody2D mRigidBody;
    protected void Awake()
    {
        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinPos = new Vector3(-width + Utils.EPSILON() * 2f, -height + Utils.EPSILON() * 2f, 0);
        mMaxPos = new Vector3(width - Utils.EPSILON() * 2f, height - Utils.EPSILON() * 2f, 0);
        mHasCollideVertical = false;
        mHasCollideHorizontal = false;

        // Add Circle Collider 2D
        gameObject.AddComponent<CircleCollider2D>();
        // Add Rigidbody 2D
        mRigidBody = gameObject.AddComponent<Rigidbody2D>();
        mRigidBody.gravityScale = 0;
        speed = Vector3.zero;

    }
    protected void Start()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
    }
    protected void FixedUpdate()
    {
        if (Utils.GAME_STOPPED) return;

        mRigidBody.MovePosition(gameObject.transform.position + speed * Time.fixedDeltaTime);
        //gameObject.transform.Translate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, 0);
    }

    protected IEnumerator waiterColliderVertical()
    {
        mHasCollideVertical = true;
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        mHasCollideVertical = false;
    }
    protected IEnumerator waiterColliderHorizontal()
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

    public bool checkPositionIsValid(Vector2 pos)
    {
        float scale = transform.localScale.x;

        return pos.x > mMinPos.x + scale
            && pos.x < mMaxPos.x - scale
            && pos.y > mMinPos.x + scale
            && pos.y < mMaxPos.y - scale;
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
            setDefaultPosition();
        }
    }
    public void setPosition(float x, float y)
    {
        Vector2 pos = new Vector2(x, y);
        setPosition(pos);
    }

}
