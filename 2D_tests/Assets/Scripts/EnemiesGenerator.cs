using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    private List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        GameObject level_controller_go = GameObject.Find(Utils.LEVEL_STR);
        LevelSettings settings = level_controller_go.GetComponent<LevelSettings>();

        int number_enemies = settings.number_enemies;
        bool random_direction = settings.random_direction;
        bool random_position = settings.random_position;
        List<Vector2> enemies_direction = settings.enemies_direction;
        List<Vector2> enemies_position = settings.enemies_position;
        Vector2 scale = settings.scale;

        enemies = new List<GameObject>();

        for (int i = 0; i < number_enemies; ++i)
        {
            GameObject enemy_go = new GameObject();
            enemy_go.name = "Enemy_" + i.ToString();
            enemy_go.tag = "Enemy";
            enemy_go.layer = 7; // Enemy Layer
            enemy_go.transform.localScale = new Vector3(scale.x, scale.y, 0.5f);
            enemy_go.transform.parent = gameObject.transform;

            // Add Sprite Renderer
            SpriteRenderer sprite_renderer = enemy_go.AddComponent<SpriteRenderer>();
            sprite_renderer.sprite = Resources.Load<Sprite>("Photoshop/EnemySprite");
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

            EnemyBehavior enemy_behavior = enemy_go.AddComponent<EnemyBehavior>();
            enemies.Add(enemy_go);

            GameObject particle_go = Instantiate(Resources.Load<GameObject>("Particles/EnemyParticle"));
            particle_go.transform.parent = enemy_go.transform;

            // Set enemy direction
            if (random_direction)
            {
                enemy_behavior.setRandomDirection();
            }
            else
            {
                enemy_behavior.setDirection(enemies_direction[i]);
            }

            // Set enemy position
            if (random_position)
            {
                enemy_behavior.setRandomPosition();
            }
            else
            {
                enemy_behavior.setPosition(enemies_position[i]);
            }
        }
    }
}

