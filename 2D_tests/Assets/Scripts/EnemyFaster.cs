using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFaster : Enemy
{

    CharacterBehavior character;
    Sprite[] mNumberSprite;
    SpriteRenderer mSpriteRenderer;
    public float mGlowIntensity;
    Sprite mSpriteOK;
    Sprite mSpriteKO;
    GameObject mCounterGameObject;
    GameObject mRotateCircle;
    GameObject mCircle;
    TextMeshPro mCounterText;
    public int mCounterMax = 3;
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

        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius -= collider.radius / 10f;

        mSpriteRenderer = GetComponent<SpriteRenderer>();
        mSpriteOK = Resources.Load<Sprite>("Sprites/Characters/EnemyFaster");
        mSpriteKO = Resources.Load<Sprite>("Sprites/Characters/EnemyFasterKO");
        mSpriteRenderer.sprite = mSpriteOK;
        mSpriteRenderer.material = new Material(Resources.Load<Material>("Materials/Enemies/EnemyFasterMaterial"));
        mGlowIntensity = mSpriteRenderer.material.GetFloat("_Glow");
        mCounterGameObject = new GameObject();
        mCounterGameObject.transform.parent = transform;
        mCounterGameObject.name = "Counter";

        mNumberSprite = Resources.LoadAll<Sprite>($"Sprites/Characters/Utils/Numbers");

        SpriteRenderer exclamation_mark_sprite_renderer = mCounterGameObject.AddComponent<SpriteRenderer>();

        exclamation_mark_sprite_renderer.sprite = mNumberSprite[mCounterMax];
        exclamation_mark_sprite_renderer.material = new Material(Resources.Load<Material>("Materials/Number"));

        mCounterGameObject.transform.transform.localScale = new Vector2(0.25f, 0.25f);

        float distance = Vector3.Distance(mSpriteRenderer.bounds.min, mSpriteRenderer.bounds.max) / 2f;
        distance = distance + exclamation_mark_sprite_renderer.sprite.bounds.size.y / 2f - mCounterGameObject.transform.transform.localScale.y;

        mCounterGameObject.transform.position = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);

        mRotateCircle = new GameObject();
        mRotateCircle.transform.position = transform.position;
        mRotateCircle.transform.parent = transform;
        mRotateCircle.name = "Rotation Helper";


        mCircle = new GameObject();

        SpriteRenderer circle_sprite_renderer = mCircle.AddComponent<SpriteRenderer>();
        circle_sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Characters/Utils/EnemyArrow");
        circle_sprite_renderer.color = Color.white;
        circle_sprite_renderer.material = new Material(Resources.Load<Material>("Materials/ArrowFaster"));

        mCircle.transform.parent = mRotateCircle.transform;
        mCircle.transform.position = new Vector3(mRotateCircle.transform.position.x + distance, mRotateCircle.transform.position.y, mRotateCircle.transform.position.z);
        mCircle.name = "Arrow";

        mCounterGameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Counter";
        mCircle.GetComponent<SpriteRenderer>().sortingLayerName = "Arrow";

        mCircle.SetActive(false);
    }

    protected IEnumerator waiterMove()
    {
        mKeepMoving = false;
        yield return new WaitForSeconds(mWaiterKeepMoving);
        mKeepMoving = true;
        mCounter = mCounterMax;
        speed = mNextOriginalSpeed;
        mCounterGameObject.GetComponent<SpriteRenderer>().sprite = mNumberSprite[mCounter];
        mSpriteRenderer.material.SetFloat("_Glow", mGlowIntensity);
        mSpriteRenderer.sprite = mSpriteOK;
        mCircle.SetActive(false);
    }

    public void reduceIndex()
    {
        if(mCounter == 1)
        {
            mCounter--;
            speed = Vector2.zero;
            mSpriteRenderer.sprite = mSpriteKO;
            mSpriteRenderer.material.SetFloat("_Glow", 0f);

            Quaternion rotation = Quaternion.FromToRotation(Vector3.right, mNextOriginalSpeed);
            //Vector3 euler_rotation = Quaternion.ToEulerAngles(rotation);
            mRotateCircle.transform.rotation = rotation;
            mCircle.SetActive(true);
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
        if (GameControler.status == GameControler.GameStatus.Win) return;
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
                    (Mathf.Sign(speed.y) == Mathf.Sign(mOriginalSpeed.y) ? mOriginalSpeed.y : -mOriginalSpeed.y));
                mNextOriginalSpeed = original_speed;
                reduceIndex();
            }
            if (!mHasCollideHorizontal && collision.gameObject.tag == "HorizontalBorder")
            {
                StartCoroutine(waiterColliderHorizontal());
                speed.y = -speed.y;
                Vector2 original_speed = new Vector2(
                    (Mathf.Sign(speed.x) == Mathf.Sign(mOriginalSpeed.x) ? mOriginalSpeed.x : -mOriginalSpeed.x),
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
