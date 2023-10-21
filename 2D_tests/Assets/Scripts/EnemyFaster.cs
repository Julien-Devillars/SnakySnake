using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class EnemyFaster : Enemy
{

    CharacterBehavior character;
    Sprite[] mNumberSprite;
    GameObject mCounterGameObject;
    TextMeshPro mCounterText;
    int mCounterMax = 3;
    int mCounter ;
    public bool mKeepMoving = true;
    public float mWaiterKeepMoving = 1.5f;
    public float mMultiplier = 1.5f;
    public Vector2 mOriginalSpeed;
    public Vector2 mNextOriginalSpeed;

    new private void Awake()
    {
        base.Awake();
        name = "Faster " + mCounter.ToString();
    }
    new private void Start()
    {
        base.Start();
        type = EnemyType.Faster;

        mCounter = mCounterMax;
        mOriginalSpeed = new Vector2(speed.x, speed.y);

        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Characters/EnemyFollower");
        sprite_renderer.material = Resources.Load<Material>("Materials/Enemies/EnemyFollowerMaterial");

        mCounterGameObject = new GameObject();
        mCounterGameObject.transform.parent = transform;
        mCounterGameObject.name = "Coounter";

        mNumberSprite = Resources.LoadAll<Sprite>($"Sprites/Characters/Utils/Numbers");

        SpriteRenderer exclamation_mark_sprite_renderer = mCounterGameObject.AddComponent<SpriteRenderer>();

        exclamation_mark_sprite_renderer.sprite = mNumberSprite[mCounterMax];
        exclamation_mark_sprite_renderer.material = new Material(Resources.Load<Material>("Materials/Number"));

        mCounterGameObject.transform.transform.localScale = new Vector2(0.25f, 0.25f);

        float distance = Vector3.Distance(sprite_renderer.bounds.min, sprite_renderer.bounds.max) / 2f;
        distance = distance + exclamation_mark_sprite_renderer.sprite.bounds.size.y / 2f - mCounterGameObject.transform.transform.localScale.y;

        mCounterGameObject.transform.position = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
    }

    protected IEnumerator waiterMove()
    {
        mKeepMoving = false;
        yield return new WaitForSeconds(mWaiterKeepMoving);
        mKeepMoving = true;
        mCounter = mCounterMax;
        speed = mNextOriginalSpeed;
        mCounterGameObject.GetComponent<SpriteRenderer>().sprite = mNumberSprite[mCounter];
    }

    public void reduceIndex()
    {
        if(mCounter == 1)
        {
            mCounter--;
            speed = Vector2.zero;
            StartCoroutine(waiterMove());
        }
        else
        {
            mCounter--;
            speed *= mMultiplier;
        }
        mCounterGameObject.GetComponent<SpriteRenderer>().sprite = mNumberSprite[mCounter];
    }

    new protected void FixedUpdate()
    {
        if (Utils.GAME_STOPPED) return;
        if (GameControler.status == GameControler.GameStatus.Waiting) return;
        if (GameControler.status == GameControler.GameStatus.Lose) return;
        if (!mKeepMoving) return;
        gameObject.transform.Translate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, 0);
    }

    new void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Border"))
        {
            launchParticle(getParticleSystem(Utils.PARTICLE_BORDER_HIT_STR));
            Border border = collision.gameObject.GetComponent<Border>();
            border.hit();
            playSound();

            if (!mHasCollideVertical && collision.gameObject.tag == "VerticalBorder")
            {
                StartCoroutine(waiterColliderVertical());
                speed.x = -speed.x;
                Vector2 original_speed = new Vector2(
                    (Mathf.Sign(speed.x) == Mathf.Sign(mOriginalSpeed.x) ? mOriginalSpeed.x : -mOriginalSpeed.x),
                    mOriginalSpeed.y);
                mNextOriginalSpeed = original_speed;
                reduceIndex();
            }
            if (!mHasCollideHorizontal && collision.gameObject.tag == "HorizontalBorder")
            {
                StartCoroutine(waiterColliderHorizontal());
                speed.y = -speed.y;
                Vector2 original_speed = new Vector2(
                    mOriginalSpeed.x,
                    (Mathf.Sign(speed.y) == Mathf.Sign(mOriginalSpeed.y) ? mOriginalSpeed.y : -mOriginalSpeed.y));

                mNextOriginalSpeed = original_speed;
                reduceIndex();
            }
        }

        if (collision.gameObject.tag.Contains("Trail"))
        {
            Lose();
        }
    }
}
