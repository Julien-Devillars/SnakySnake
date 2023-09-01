using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCircle : Enemy
{
    GameObject mRotateCircle;
    GameObject mCircle;
    public float mRotateSpeed = 30f;
    public float mAttackSpeed = 5f;
    public float mStartRotation = 180f; // Between 0 & 360 0 is right
    public bool mInverse = false; 

    private Color mStartColor = Color.cyan;
    private Color mEndColor = Color.magenta;
    private float mTimer = 0f;

    private string str_OutlineWidth = "_OutlineWidth";
    private float mMinOutlineWidth = 0.01f;
    private float mMaxOutlineWidth = 0.2f;
    private string str_OutlineGlow = "_OutlineGlow";
    private float mMinOutlineGlow = 1f;
    private float mMaxOutlineGlow = 50f;
    private string str_OutlineColor = "_OutlineColor";

    private void Start()
    {
        base.Start();
        type = EnemyType.Circle;

        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Characters/EnemyCircle");
        sprite_renderer.material = Resources.Load<Material>("Materials/Enemies/EnemyCircleMaterial");
        name = "Circle";

        mRotateCircle = new GameObject();
        mRotateCircle.transform.position = transform.position;
        mRotateCircle.transform.parent = transform;
        mRotateCircle.name = "Rotation Helper";

        mCircle = new GameObject();

        SpriteRenderer circle_sprite_renderer = mCircle.AddComponent<SpriteRenderer>();
        circle_sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Characters/Utils/EnemyArrow");
        circle_sprite_renderer.color = Color.white;
        circle_sprite_renderer.material = new Material(Resources.Load<Material>("Materials/Arrow"));
        circle_sprite_renderer.material.SetFloat(str_OutlineGlow, mMinOutlineGlow);
        circle_sprite_renderer.material.SetFloat(str_OutlineWidth, mMinOutlineWidth);
        circle_sprite_renderer.material.SetColor(str_OutlineColor, mStartColor);

        //Vector3 enemy_sprite_size = sprite_renderer.bounds.size / 2f;
        float distance = Vector3.Distance(sprite_renderer.bounds.min, sprite_renderer.bounds.max) / 2f;
        distance = distance + circle_sprite_renderer.sprite.bounds.size.x / 2f;

        mCircle.transform.parent = mRotateCircle.transform;
        mCircle.transform.position = new Vector3(mRotateCircle.transform.position.x + distance, mRotateCircle.transform.position.y, mRotateCircle.transform.position.z);
        mCircle.name = "Arrow";

        mRotateCircle.transform.RotateAround(transform.position, Vector3.forward, mStartRotation);

        StartCoroutine(attack());
    }
    private void FixedUpdate()
    {
        if (Utils.GAME_STOPPED) return;
        gameObject.transform.Translate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, 0);
        mRotateCircle.transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * mRotateSpeed);

        SpriteRenderer circle_sprite_renderer = mCircle.GetComponent<SpriteRenderer>();
        mTimer += Time.deltaTime;
        Color new_color = Color.Lerp(mStartColor, mEndColor, mTimer / mAttackSpeed);
        float new_glow = Mathf.Lerp(mMinOutlineGlow, mMaxOutlineGlow, mTimer / mAttackSpeed);
        float new_width = Mathf.Lerp(mMinOutlineWidth, mMaxOutlineWidth, mTimer / mAttackSpeed);

        circle_sprite_renderer.material.SetFloat(str_OutlineGlow, new_glow);
        circle_sprite_renderer.material.SetFloat(str_OutlineWidth, new_width);
        circle_sprite_renderer.material.SetColor(str_OutlineColor, new_color);
    }

    IEnumerator attack()
    {
        while(true)
        {
            yield return new WaitForSeconds(mAttackSpeed);

            Vector3 circle_pos = mCircle.transform.position;
            Vector3 enemy_pos = transform.position;
            Vector3 new_direction = circle_pos - enemy_pos;

            Quaternion rotation = Quaternion.FromToRotation(speed, new_direction);

            speed = rotation * speed;
            mTimer = 0f;
            if(mInverse)
            {
                mRotateSpeed = -mRotateSpeed;
            }
        }
    }


    protected void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.name.Contains("Border"))
        {
            mRotateCircle.transform.RotateAround(transform.position, Vector3.forward, 180f);
        }
    }

}
