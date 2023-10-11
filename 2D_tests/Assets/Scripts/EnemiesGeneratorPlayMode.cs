using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGeneratorPlayMode : MonoBehaviour
{
    private List<GameObject> enemies;
    public static bool test_level = true;
    public int mLevel = -1;
    public int mWorld = -1;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();

        int level_index;
        int world_index;
        GameControler.type = GameControler.GameType.Play;
        if (test_level)
        {
            level_index = mLevel - 1;
            world_index = mWorld - 1;
            Worlds.createWorlds();
            GameControler.currentLevel = level_index;
            GameControler.currentWorld = world_index;
        }
        else
        {
            level_index = GameControler.currentLevel;
            world_index = GameControler.currentWorld;
        }

        Level current_level = Worlds.getLevel(world_index, level_index);
        Enemy.mCounter = 0;
        foreach (EnemyInfo enemy in current_level.mEnemies)
        {
            GameObject enemy_go = new GameObject();
            enemy_go.name = "Enemy " + Enemy.mCounter.ToString();
            enemy_go.tag = "Enemy";
            enemy_go.layer = 7; // Enemy Layer
            enemy_go.transform.localScale = new Vector3(enemy.scale, enemy.scale, 0.5f);
            enemy_go.transform.parent = gameObject.transform;

            // Add Sprite Renderer
            SpriteRenderer sprite_renderer = enemy_go.AddComponent<SpriteRenderer>();
            sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Characters/Enemy1");
            sprite_renderer.sortingLayerName = "Enemy";
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
            CircleCollider2D collider = enemy_go.AddComponent<CircleCollider2D>();
            collider.radius -= collider.radius/10f;

            // Add Rigidbody 2D
            Rigidbody2D rigidbody_2D = enemy_go.AddComponent<Rigidbody2D>();
            rigidbody_2D.gravityScale = 0;
            
            // Particles
            GameObject particle_go = Instantiate(Resources.Load<GameObject>("Particles/EnemyParticle"));
            particle_go.transform.parent = enemy_go.transform;
            particle_go.transform.localScale = new Vector3(enemy.scale, enemy.scale, enemy.scale) / Utils.ENEMY_DEFAULT_SCALE;

            Enemy enemy_behavior;
            switch(enemy.type)
            {
                case EnemyType.Dummy:
                    enemy_behavior = enemy_go.AddComponent<Enemy>();
                    collider.enabled = false;
                    sprite_renderer.enabled = false;
                    enemy.direction = Vector2.zero;
                    enemy.scale = 0.01f;
                    particle_go.SetActive(false);
                    break;
                case EnemyType.Basic:
                    enemy_behavior = enemy_go.AddComponent<Enemy>();
                    break;
                case EnemyType.Circle:
                    EnemyCircleInfo enemy_circle_info = (EnemyCircleInfo)enemy;
                    EnemyCircle enemy_circle = enemy_go.AddComponent<EnemyCircle>();
                    enemy_circle.mRotateSpeed = enemy_circle_info.mRotateSpeed;
                    enemy_circle.mStartRotation = enemy_circle_info.mStartRotation;
                    enemy_circle.mAttackSpeed = enemy_circle_info.mAttackSpeed;
                    enemy_circle.mInverse = enemy_circle_info.mInverse;
                    enemy_behavior = enemy_circle;
                    break;
                case EnemyType.Follower:
                    EnemyFollowerInfo enemy_follower_info = (EnemyFollowerInfo)enemy;
                    EnemyFollower enemy_follower = enemy_go.AddComponent<EnemyFollower>();
                    enemy_follower.mKeepMoving = enemy_follower_info.mKeepMoving;
                    enemy_behavior = enemy_follower;
                    break;
                case EnemyType.Flyer:
                    enemy_behavior = enemy_go.AddComponent<EnemyFlyer>();
                    break;
                case EnemyType.Driller:
                    EnemyDrillerInfo enemy_driller_info = (EnemyDrillerInfo)enemy;
                    EnemyDriller enemy_driller = enemy_go.AddComponent<EnemyDriller>();
                    enemy_driller.mReverseHorizontal = enemy_driller_info.mReverseHorizontal;
                    enemy_driller.mReverseVertical = enemy_driller_info.mReverseVertical;
                    enemy_behavior = enemy_driller;
                    break;
                default:
                    enemy_behavior = new Enemy();
                    Debug.Log("No enemy type found !");
                    break;
            }
            enemies.Add(enemy_go);

            // Set enemy direction
            enemy_behavior.setDirection(enemy.direction);
            enemy_behavior.setScale(enemy.scale);


            // Set enemy position
            enemy_behavior.setRelativePosition(enemy.position);
            Enemy.mCounter++;
        }

        for(int i = 0; i < current_level.mEnemies.Count; ++i)
        {
            EnemyInfo enemy_info = current_level.mEnemies[i];
            if (enemy_info.link == null) continue;

            Enemy enemy = enemies[i].GetComponent<Enemy>();
            foreach(int index in enemy_info.link)
            {
                enemy.addLink(enemies[index].GetComponent<Enemy>());
            }
        }     



        List<GameObject> stars = new List<GameObject>();
        GameObject stars_go = GameObject.Find(Utils.STARS_STR);
        stars_go.AddComponent<StarsVictory>();

        foreach (StarInfo star in current_level.mStars)
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
            sprite_renderer.material = Resources.Load<Material>("Materials/Star"); //new Material(Shader.Find("Sprites/Default"));
            sprite_renderer.sortingLayerName = "Star";
            sprite_renderer.sortingOrder = 1;

            Star star_script = star_go.AddComponent<Star>();
            star_script.setRelativePosition(star.position);

            stars.Add(star_go);
        }
        Timer.StartLevelTimer();
    }
}

