using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFlyer : Enemy
{
    CharacterBehavior character;
    GameObject mExcalamationMark;

    private void Start()
    {
        base.Start();
        mCanCrossBorder = true;
        type = EnemyType.Flyer;

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinPos = new Vector3(-width, -height, 0);
        mMaxPos = new Vector3(width, height, 0);

        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Characters/EnemyFlyer");
        sprite_renderer.material = Resources.Load<Material>("Materials/Enemies/EnemyFlyerMaterial");
        Destroy(gameObject.GetComponent<Collider2D>());
        name = "Flyer";

        character = GameObject.Find(Utils.CHARACTER).GetComponent<CharacterBehavior>();

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
        float size = sprite_renderer.bounds.size.x / 2;
        if (transform.position.y - size <= mMinPos.y||  transform.position.y + size >= mMaxPos.y)
        {
            speed.y = -speed.y;
        }
        if (transform.position.x - size <= mMinPos.x || transform.position.x + size >= mMaxPos.x)
        {
            speed.x = -speed.x;
        }

        gameObject.transform.Translate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, 0);

        Vector3 center = transform.position;
        //float width = sprite_renderer.bounds.size.x / 2f;
        //float height = sprite_renderer.bounds.size.y;
        //Vector3 top_left = new Vector3(center.x - width / 2f, center.y + height / 2f);
        //Vector3 top_right = new Vector3(center.x + width / 2f, center.y + height / 2f);
        //Vector3 bot_left = new Vector3(center.x - width / 2f, center.y - height / 2f);
        //Vector3 bot_right = new Vector3(center.x + width / 2f, center.y - height / 2f);
        List<Vector3> points_to_check = new List<Vector3> { center };
        if(Utils.allPointsAreInBackgroundsWithoutEnemies(points_to_check))
        {
            sprite_renderer.material.SetColor("_GlowColor", Color.magenta);
            sprite_renderer.material.SetFloat("_GhostBlend", 0f);
        }
        else
        {
            sprite_renderer.material.SetColor("_GlowColor", Color.cyan);
            sprite_renderer.material.SetFloat("_GhostBlend", 1f);
        }
    }


    new void OnCollisionEnter2D(Collision2D collision){}
}
