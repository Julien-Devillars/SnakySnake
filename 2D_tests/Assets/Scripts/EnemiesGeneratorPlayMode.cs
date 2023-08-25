using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGeneratorPlayMode : MonoBehaviour
{
    private List<GameObject> enemies;
    public bool test_level = true;
    public int mLevel = -1;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();

        int level_index;
        if (test_level)
        {
            level_index = mLevel - 1;
            Levels.createLevels();
            GameControler.currentLevel = mLevel;
            GameControler.type = GameControler.GameType.Play;
        }
        else
        {
            level_index = GameControler.currentLevel;
        }

        foreach (EnemyInfo enemy in Levels.levels[level_index].mEnemies)
        {
            GameObject enemy_go = new GameObject();
            enemy_go.name = "Enemy";
            enemy_go.tag = "Enemy";
            enemy_go.layer = 7; // Enemy Layer
            enemy_go.transform.localScale = new Vector3(enemy.scale, enemy.scale, 0.5f);
            enemy_go.transform.parent = gameObject.transform;

            // Add Sprite Renderer
            SpriteRenderer sprite_renderer = enemy_go.AddComponent<SpriteRenderer>();
            sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Characters/Enemy1");
            if (Utils.SHADER_ON)
            {
                sprite_renderer.material = Resources.Load<Material>("Materials/Enemies/EnemyMaterial");
            }
            else
            {
                sprite_renderer.material = new Material(Shader.Find("Sprites/Default"));
            }
            sprite_renderer.material.color = Color.white;
            sprite_renderer.sortingOrder = 1;

            // Add Circle Collider 2D
            enemy_go.AddComponent<CircleCollider2D>();
            // Add Rigidbody 2D
            Rigidbody2D rigidbody_2D = enemy_go.AddComponent<Rigidbody2D>();
            rigidbody_2D.gravityScale = 0;


            Enemy enemy_behavior;
            switch(enemy.type)
            {
                case EnemyType.Basic:
                    enemy_behavior = enemy_go.AddComponent<Enemy>();
                    break;
                case EnemyType.Circle:
                    EnemyCircleInfo enemy_circle_info = (EnemyCircleInfo)enemy;
                    EnemyCircle enemy_circle = enemy_go.AddComponent<EnemyCircle>();
                    enemy_circle.mRotateSpeed = enemy_circle_info.mRotateSpeed;
                    enemy_circle.mStartRotation = enemy_circle_info.mStartRotation;
                    enemy_circle.mAttackSpeed = enemy_circle_info.mAttackSpeed;
                    enemy_behavior = enemy_circle;
                    break;
                case EnemyType.Follower:
                    enemy_behavior = enemy_go.AddComponent<EnemyFollower>();
                    break;
                case EnemyType.Flyer:
                    enemy_behavior = enemy_go.AddComponent<EnemyFlyer>();
                    break;
                case EnemyType.Driller:
                    enemy_behavior = enemy_go.AddComponent<EnemyDriller>();
                    break;
                default:
                    enemy_behavior = new Enemy();
                    Debug.Log("No enemy type found !");
                    break;
            }

            enemies.Add(enemy_go);

            GameObject particle_go = Instantiate(Resources.Load<GameObject>("Particles/EnemyParticle"));
            particle_go.transform.parent = enemy_go.transform;

            // Set enemy direction
            enemy_behavior.setDirection(enemy.direction);
            enemy_behavior.setScale(enemy.scale);


            // Set enemy position
            enemy_behavior.setRelativePosition(enemy.position);
        }


        List<GameObject> stars = new List<GameObject>();
        GameObject stars_go = GameObject.Find(Utils.STARS_STR);
        stars_go.AddComponent<StarsVictory>();

        foreach (StarInfo star in Levels.levels[level_index].mStars)
        {
            GameObject star_go = new GameObject();
            star_go.name = Utils.STAR_STR;
            star_go.tag = Utils.STAR_STR;
            star_go.layer = 13; // Star Layer
            star_go.transform.localScale = new Vector3(star.scale, star.scale, 0.5f);
            star_go.transform.parent = stars_go.transform;

            // Add Sprite Renderer
            SpriteRenderer sprite_renderer = star_go.AddComponent<SpriteRenderer>();

            sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/UI/StarNOK");
            sprite_renderer.material = new Material(Shader.Find("Sprites/Default"));
            sprite_renderer.sortingOrder = 1;

            Star star_script = star_go.AddComponent<Star>();
            star_script.setRelativePosition(star.position);

            stars.Add(star_go);
        }
    }
}

