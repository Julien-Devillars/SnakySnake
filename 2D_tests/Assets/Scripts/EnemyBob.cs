using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBob : Enemy
{
    protected void Awake()
    {
        base.Awake();
        mSprite = "Photoshop/EnemyBob";
        mMaterial = "Materials/EnemyBobMaterial";

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinPos = new Vector3(-width, -height, 0);
        mMaxPos = new Vector3(width, height, 0);
    }

    protected void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Enemy/Bob");

        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy/Bob"), LayerMask.NameToLayer("Border/Editable"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy/Bob"), LayerMask.NameToLayer("Border/NotEditable"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy/Bob"), LayerMask.NameToLayer("Enemy/Kevin"), true);
    }
    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        if (position.x <= mMinPos.x || position.x >= mMaxPos.x)
        {
            speed.x = -speed.x;
        }
        if (position.y <= mMinPos.y || position.y >= mMaxPos.y)
        {
            speed.y = -speed.y;
        }
        base.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Trail") || collision.gameObject.CompareTag("Character"))
        {
            GameControler.status = GameControler.GameStatus.Lose;
            if (Utils.HAS_LOSE)
            {
                SceneManager.LoadScene("Level_1");
            }
        }
    }
}
