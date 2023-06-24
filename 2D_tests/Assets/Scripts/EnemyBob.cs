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
    }

    protected void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Enemy/Bob");

        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy/Bob"), LayerMask.NameToLayer("Border/Editable"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy/Bob"), LayerMask.NameToLayer("Enemy/Kevin"), true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Border"))
        {
            Border border = collision.gameObject.GetComponent<Border>();
            if (border.mIsEditable) return;

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
