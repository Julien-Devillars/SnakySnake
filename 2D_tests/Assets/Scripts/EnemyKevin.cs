using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyKevin : Enemy
{
    protected void Awake()
    {
        base.Awake();
        mSprite = "Photoshop/EnemyKevin";
        mMaterial = "Materials/EnemyKevinMaterial";
    }
    protected void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Enemy/Kevin");

        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy/Bob"), LayerMask.NameToLayer("Enemy/Kevin"), true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
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
            GameControler.status = GameControler.GameStatus.Lose;
            if (Utils.HAS_LOSE)
            {
                SceneManager.LoadScene("Level_1");
            }
        }
    }
}
