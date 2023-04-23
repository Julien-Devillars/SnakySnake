using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    private List<GameObject> enemies;
    
    [SerializeField] private LevelSettings settingsForLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        int number_enemies = settingsForLevel.number_enemies;
        bool random_direction = settingsForLevel.random_direction;
        bool random_position = settingsForLevel.random_position;
        List<Vector2> enemies_direction = settingsForLevel.enemies_direction;
        List<Vector2> enemies_position = settingsForLevel.enemies_position;

        enemies = new List<GameObject>();

        for (int i = 0; i < number_enemies; ++i)
        {
            GameObject enemy_go = new GameObject();
            enemy_go.name = "Enemy_" + i.ToString();
            enemy_go.tag = "Enemy";
            enemy_go.layer = 7; // Enemy Layer
            enemy_go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            enemy_go.transform.parent = gameObject.transform;

            // Add Sprite Renderer
            SpriteRenderer sprite_renderer = enemy_go.AddComponent<SpriteRenderer>();
            sprite_renderer.material = new Material(Shader.Find("Sprites/Default"));
            sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Circle");
            sprite_renderer.material.color = Color.black;
            sprite_renderer.sortingOrder = 1;

            // Add Circle Collider 2D
            enemy_go.AddComponent<CircleCollider2D>();
            // Add Rigidbody 2D
            Rigidbody2D rigidbody_2D = enemy_go.AddComponent<Rigidbody2D>();
            rigidbody_2D.gravityScale = 0;

            EnemyBehavior enemy_behavior = enemy_go.AddComponent<EnemyBehavior>();
            enemies.Add(enemy_go);

            // Set enemy direction
            if(random_direction)
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


    // Update is called once per frame
    void Update()
    {
        
    }
}

