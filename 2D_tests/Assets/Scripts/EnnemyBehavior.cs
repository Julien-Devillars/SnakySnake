using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemyBehavior : MonoBehaviour
{
    public Vector2 speed;
    private Vector3 mMinPos;
    private Vector3 mMaxPos;
    void Awake()
    {
        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinPos = new Vector3(-width + Utils.EPSILON * 2f, -height + Utils.EPSILON * 2f, 0);
        mMaxPos = new Vector3(width - Utils.EPSILON * 2f, height - Utils.EPSILON * 2f, 0);
    }

    private void FixedUpdate()
    {
        gameObject.transform.Translate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Border"))
        {
            if (collision.gameObject.tag == "VerticalBorder")
            {
                speed.x = -speed.x;
            }
            if (collision.gameObject.tag == "HorizontalBorder")
            {
                speed.y = -speed.y;
            }
        }
        if (collision.gameObject.tag.Contains("Trail"))
        {
            Debug.Log("Lose");
            SceneManager.LoadScene("Level_1");
        }
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
        return pos.x > mMinPos.x
            && pos.x < mMaxPos.x
            && pos.y > mMinPos.x
            && pos.y < mMaxPos.y;
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
        if(checkPositionIsValid(pos))
        {
            transform.position = pos;
        }
        else
        {
            setDefaultPosition();
        }
    }

}
