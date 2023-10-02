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
    private Vector2 mPreviousSpeed;

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
        Destroy(gameObject.GetComponent<Collider2D>());
        name = "Driller";

        character = GameObject.Find(Utils.CHARACTER).GetComponent<CharacterBehavior>();
        last_border = null;
        last_last_border = null;
        mIsOnBorder = false;

        mPreviousSpeed = speed;
    }

    private void FixedUpdate()
    {
        if (Utils.GAME_STOPPED) return;

        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        SpriteRenderer character_sprite_renderer = character.GetComponent<SpriteRenderer>();
        if (sprite_renderer.bounds.Intersects(character_sprite_renderer.bounds))
        {
            Lose();
        }

        if(speed == Vector2.zero)
        {
            speed = mPreviousSpeed;
        }

        if (!mIsOnBorder)
        {
            foreach (Border border in Utils.getBorders())
            {
                if (border.onSmallFuzzyBorder(transform.position))
                {
                    last_border = border;
                    mIsOnBorder = true;
                    mPreviousSpeed = speed;
                    if (border.isVertical())
                    {
                        transform.position = new Vector3(border.mStartPoint.x, transform.position.y, transform.position.z);
                        speed.y = Mathf.Sqrt(Mathf.Pow(speed.x, 2f) + Mathf.Pow(speed.y, 2f));
                        speed.y = border.isBottomToTop() ? speed.y : -speed.y;
                        speed.x = 0f;
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x, border.mStartPoint.y, transform.position.z);
                        speed.x = Mathf.Sqrt(Mathf.Pow(speed.x, 2f) + Mathf.Pow(speed.y, 2f));
                        speed.x = border.isLeftToRight() ? speed.x : -speed.x;
                        speed.y = 0f;
                    }
                }
            }
            gameObject.transform.Translate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, 0);
        }
        else
        {
            foreach (Border border in Utils.getBorders())
            {
                if (last_border != null && last_border == border) continue;
                if (last_last_border != null && last_last_border == border) continue;

                if (border.onSmallFuzzyBorder(transform.position))
                {
                    last_last_border = last_border;
                    last_border = border;
                    mPreviousSpeed = speed;

                    if (border.isVertical())
                    {
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
                            speed.y = (Random.value > 0.5f) ? -Mathf.Abs(speed.x) : Mathf.Abs(speed.x); 
                        }
                        speed.x = 0f;
                    }
                    else
                    {
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
                            speed.x = (Random.value > 0.5f) ? -Mathf.Abs(speed.y) : Mathf.Abs(speed.y);
                        }
                        speed.y = 0f;
                    }
                }
            }

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
            gameObject.transform.Translate(0f, move * Time.deltaTime, 0);
        }
        
        // Before
        

        //Vector3 center = transform.position;
        //float width = sprite_renderer.bounds.size.x / 2f;
        //float height = sprite_renderer.bounds.size.y;
        //Vector3 top_left = new Vector3(center.x - width / 2f, center.y + height / 2f);
        //Vector3 top_right = new Vector3(center.x + width / 2f, center.y + height / 2f);
        //Vector3 bot_left = new Vector3(center.x - width / 2f, center.y - height / 2f);
        //Vector3 bot_right = new Vector3(center.x + width / 2f, center.y - height / 2f);
        //List<Vector3> points_to_check = new List<Vector3> { center };
        //if(Utils.allPointsAreInBackgroundsWithoutEnemies(points_to_check))
        //{
        //    sprite_renderer.material.SetColor("_GlowColor", Color.magenta);
        //    sprite_renderer.material.SetFloat("_GhostBlend", 0f);
        //}
        //else
        //{
        //    sprite_renderer.material.SetColor("_GlowColor", Color.cyan);
        //    sprite_renderer.material.SetFloat("_GhostBlend", 1f);
        //}
    }


    new void OnCollisionEnter2D(Collision2D collision){}
}
