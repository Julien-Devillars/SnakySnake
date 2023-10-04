using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFollower : Enemy
{

    CharacterBehavior character;
    GameObject mExcalamationMark;
    public bool mKeepMoving = false;

    new private void Awake()
    {
        base.Awake();
        name = "Follower " + mCounter.ToString();
    }
    new private void Start()
    {
        base.Start();
        type = EnemyType.Follower;

        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Characters/EnemyFollower");
        sprite_renderer.material = Resources.Load<Material>("Materials/Enemies/EnemyFollowerMaterial");

        mExcalamationMark = new GameObject();
        mExcalamationMark.transform.parent = transform;
        mExcalamationMark.name = "ExclamationMark";

        SpriteRenderer exclamation_mark_sprite_renderer = mExcalamationMark.AddComponent<SpriteRenderer>();
        exclamation_mark_sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Characters/Utils/ExclamationMark");
        exclamation_mark_sprite_renderer.material = new Material(Resources.Load<Material>("Materials/ExclamationMark"));

        float distance = Vector3.Distance(sprite_renderer.bounds.min, sprite_renderer.bounds.max) / 2f;
        distance = distance + exclamation_mark_sprite_renderer.sprite.bounds.size.x / 2f;

        mExcalamationMark.transform.position = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
        mExcalamationMark.SetActive(false);

        character = GameObject.Find(Utils.CHARACTER).GetComponent<CharacterBehavior>();

        // Add Circle Collider 2D
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius -= collider.radius / 3f;
    }
    new private void FixedUpdate()
    {
        if (Utils.GAME_STOPPED) return;

        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();

        if (character.mTrails.Count > 0)
        {
            Vector3 character_pos = character.transform.position;
            Vector3 enemy_pos = transform.position;
            Vector3 new_direction = character_pos - enemy_pos;

            Quaternion rotation = Quaternion.FromToRotation(speed, new_direction);

            speed = rotation * speed;
            mExcalamationMark.SetActive(true);
            sprite_renderer.material.SetColor("_GlowColor", Color.magenta);
            gameObject.transform.Translate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, 0);
        }
        else
        {
            mExcalamationMark.SetActive(false);
            sprite_renderer.material.SetColor("_GlowColor", Color.cyan);
            if(mKeepMoving)
            {
                gameObject.transform.Translate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, 0);
            }
        }


    }

}
