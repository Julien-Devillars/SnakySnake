using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDriller : Enemy
{

    CharacterBehavior character;
    Border last_border;
    Border last_last_border;
    bool mIsOnBorder = false;
    bool mIsOnTrail = false;
    public bool mReverseVertical = false;
    public bool mReverseHorizontal = false;
    private Vector2 mPreviousSpeed;
    private Quaternion mPreviousRotation;

    new private void Awake()
    {
        base.Awake();
        name = "Driller " + mCounter.ToString();
    }

    private void Start()
    {
        base.Start();
        type = EnemyType.Driller;
        mCanCrossBorder = true;

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinPos = new Vector3(-width, -height, 0);
        mMaxPos = new Vector3(width, height, 0);

        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Characters/EnemyDriller");
        sprite_renderer.material = Resources.Load<Material>("Materials/Enemies/EnemyDrillerMaterial");
        CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius -= collider.radius/3f;

        character = GameObject.Find(Utils.CHARACTER).GetComponent<CharacterBehavior>();
        last_border = null;
        last_last_border = null;
        mIsOnBorder = false;

        mPreviousSpeed = speed;
        mPreviousRotation = transform.rotation;
        mAudioSource.clip = Resources.Load<AudioClip>("Musics/UI & Menu/Misc/Data Calculation/Data Calculation Digits B");
    }

    private void FixedUpdate()
    {
        if (Utils.GAME_STOPPED) return;
        if (GameControler.status == GameControler.GameStatus.Lose) return;

        if(speed == Vector2.zero)
        {
            speed = mPreviousSpeed;
        }

        if (mIsOnTrail)
        {
            if(!mAudioSource.isPlaying)
            {
                playSound();
            }
        }
        else
        {
            mAudioSource.Stop();
        }
        if (mIsOnBorder || mIsOnTrail)
        {
            float move = Mathf.Max(Mathf.Abs(speed.x), Mathf.Abs(speed.y)); 
            if (speed.y > 0 && speed.x == 0)
            {
                transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
            }
            else if (speed.y < 0 && speed.x == 0)
            {
                transform.rotation = Quaternion.AngleAxis(180f, Vector3.forward);
            }
            else if (speed.x > 0 && speed.y == 0)
            {
                transform.rotation = Quaternion.AngleAxis(-90f, Vector3.forward); 
            }
            else if (speed.x < 0 && speed.y == 0)
            {
                transform.rotation = Quaternion.AngleAxis(90f, Vector3.forward); 
            }
            float speed_up = (mIsOnTrail ? 2.5f : 1f);
            gameObject.transform.Translate(0f, move * speed_up * Time.deltaTime, 0);

            if (mPreviousRotation != transform.rotation)
            {
                mPreviousRotation = transform.rotation;
                foreach(EnemyLink enemy_link in mLinks)
                {
                    enemy_link.setCollider(false);
                }
            }
        }
        else
        {
            gameObject.transform.Translate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, 0);
        }

        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        if (mIsOnTrail)
        {
            //sprite_renderer.material.SetFloat("_GhostBlend", 0.8f);
            sprite_renderer.material.SetFloat("_FishEyeUvAmount", 0.35f);
        }
        else
        {
            //sprite_renderer.material.SetFloat("_GhostBlend", 0f);
            sprite_renderer.material.SetFloat("_FishEyeUvAmount", 0f);
        }
    }


    new void OnCollisionEnter2D(Collision2D collision){}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Border"))
        {
            mIsOnBorder = true;
            mIsOnTrail = false;
            Border border = collision.gameObject.GetComponent<Border>();
            if(speed != Vector2.zero)
            {
                mPreviousSpeed = speed;
            }
            if (border.isVertical())
            {
                float shift = transform.position.x - border.mStartPoint.x;
                transform.position = new Vector3(border.mStartPoint.x, transform.position.y, transform.position.z);
                if (transform.position == border.mStartPoint)
                {
                    speed.y = border.isBottomToTop() ? Mathf.Abs(speed.x) : -Mathf.Abs(speed.x);

                }
                else if (transform.position == border.mEndPoint)
                {
                    speed.y = border.isBottomToTop() ? -Mathf.Abs(speed.x) : Mathf.Abs(speed.x);
                }
                else
                {
                    if (shift > 0)
                    {
                        speed.y = mReverseVertical ? -Mathf.Abs(speed.x) : Mathf.Abs(speed.x);
                    }
                    else
                    {
                        speed.y = mReverseVertical ? Mathf.Abs(speed.x) : -Mathf.Abs(speed.x);
                    }
                    //speed.y = (Random.value > 0.5f) ? -Mathf.Abs(speed.x) : Mathf.Abs(speed.x);
                }
                speed.x = 0f;
            }
            else
            {
                float shift = transform.position.y - border.mStartPoint.y;
                transform.position = new Vector3(transform.position.x, border.mStartPoint.y, transform.position.z);
                if (transform.position == border.mStartPoint)
                {
                    speed.x = border.isLeftToRight() ? Mathf.Abs(speed.y) : -Mathf.Abs(speed.y);

                }
                else if (transform.position == border.mEndPoint)
                {
                    speed.x = border.isLeftToRight() ? -Mathf.Abs(speed.y) : Mathf.Abs(speed.y);
                }
                else
                {
                    if (shift < 0)
                    {
                        speed.x = mReverseHorizontal ? -Mathf.Abs(speed.y) : Mathf.Abs(speed.y);
                    }
                    else
                    {
                        speed.x = mReverseHorizontal  ? Mathf.Abs(speed.y) : -Mathf.Abs(speed.y);
                    }
                    //speed.x = (Random.value > 0.5f) ? -Mathf.Abs(speed.y) : Mathf.Abs(speed.y);
                }
                speed.y = 0f;
            }
        }

        if (collision.name.Contains("Trail"))
        {
            mIsOnTrail = true;
            mIsOnBorder = false;
            Trail trail = collision.gameObject.GetComponent<Trail>();
            Vector3 start_point = trail.getStartPoint();
            Vector3 end_point = trail.getEndPoint();
            if (speed != Vector2.zero)
            {
                mPreviousSpeed = speed;
            }
            if (trail.isVertical())
            {
                transform.position = new Vector3(start_point.x, transform.position.y, transform.position.z);
                float direction = end_point.y - start_point.y;

                speed.y = (direction > 0) ? Mathf.Abs(speed.x) : -Mathf.Abs(speed.x);
                speed.x = 0f;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, start_point.y, transform.position.z);
                float direction = end_point.x - start_point.x;
                speed.x = (direction > 0) ? Mathf.Abs(speed.y) : -Mathf.Abs(speed.y);
                speed.y = 0f;
            }
        }
        if(collision.name == Utils.CHARACTER)
        {
            Lose();
        }
    }
}
