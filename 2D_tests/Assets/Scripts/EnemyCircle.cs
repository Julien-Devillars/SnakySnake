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

    private void Start()
    {
        base.Start();

        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Characters/Enemy2");

        mRotateCircle = new GameObject();
        mRotateCircle.transform.position = transform.position;
        mRotateCircle.transform.parent = transform;
        mRotateCircle.name = "Rotation Helper";

        mCircle = new GameObject();

        SpriteRenderer circle_sprite_renderer = mCircle.AddComponent<SpriteRenderer>();
        circle_sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Characters/Utils/EnemyArrow");
        circle_sprite_renderer.material = new Material(Shader.Find("AllIn1SpriteShader/AllIn1SpriteShader"));
        
        mCircle.transform.parent = mRotateCircle.transform;
        mCircle.transform.position = new Vector3(mRotateCircle.transform.position.x + 3f, mRotateCircle.transform.position.y, mRotateCircle.transform.position.z);
        mCircle.name = "Arrow";
        StartCoroutine(attack());
    }
    private void FixedUpdate()
    {
        if (Utils.GAME_STOPPED) return;
        gameObject.transform.Translate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, 0);
        mRotateCircle.transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * mRotateSpeed);
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
        }
    }
}
