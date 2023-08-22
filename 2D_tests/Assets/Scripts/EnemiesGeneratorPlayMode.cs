using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGeneratorPlayMode : MonoBehaviour
{
    private List<GameObject> enemies;
    public int mLevel = -1;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();

        int level_index = mLevel < 0 ? GameControler.currentLevel : mLevel;
        if(Levels.levels.Count == 0)
        {
            Levels.createLevels();
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
                sprite_renderer.material = Resources.Load<Material>("Materials/EnemyMaterial");
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

            Enemy enemy_behavior = enemy_go.AddComponent<Enemy>();
            enemies.Add(enemy_go);

            GameObject particle_go = Instantiate(Resources.Load<GameObject>("Particles/EnemyParticle"));
            particle_go.transform.parent = enemy_go.transform;

            // Set enemy direction
            enemy_behavior.setDirection(enemy.direction);
            enemy_behavior.setScale(enemy.scale);


            // Set enemy position
            enemy_behavior.setRelativePosition(enemy.position);
        }
    }
}

