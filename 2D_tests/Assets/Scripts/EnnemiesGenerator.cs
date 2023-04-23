using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesGenerator : MonoBehaviour
{
    public int number_ennemies = 1;
    public List<GameObject> ennemies;

    public bool random_direction;
    [SerializeField]
    public List<Vector2> ennemies_direction;

    public bool default_position;
    public bool random_position;
    [SerializeField]
    public List<Vector2> ennemies_position;

    // Start is called before the first frame update
    void Start()
    {
        ennemies = new List<GameObject>();

        for (int i = 0; i < number_ennemies; ++i)
        {
            GameObject ennemy_go = new GameObject();
            ennemy_go.name = "Ennemy_" + i.ToString();
            ennemy_go.tag = "Ennemy";
            ennemy_go.layer = 7; // Ennemy Layer
            ennemy_go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            ennemy_go.transform.parent = gameObject.transform;

            // Add Sprite Renderer
            SpriteRenderer sprite_renderer = ennemy_go.AddComponent<SpriteRenderer>();
            sprite_renderer.material = new Material(Shader.Find("Sprites/Default"));
            sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/Circle");
            sprite_renderer.material.color = Color.black;
            sprite_renderer.sortingOrder = 1;

            // Add Circle Collider 2D
            /*CircleCollider2D collider_2D = */
            ennemy_go.AddComponent<CircleCollider2D>();
            // Add Rigidbody 2D
            Rigidbody2D rigidbody_2D = ennemy_go.AddComponent<Rigidbody2D>();
            rigidbody_2D.gravityScale = 0;

            EnnemyBehavior ennemy_behavior = ennemy_go.AddComponent<EnnemyBehavior>();
            ennemies.Add(ennemy_go);

            // Set ennemy direction
            if(random_direction)
            {
                ennemy_behavior.setRandomDirection();
            }
            else
            {
                ennemy_behavior.setDirection(ennemies_direction[i]);
            }

            // Set ennemy position
            if (default_position)
            {
                ennemy_behavior.setDefaultPosition();
            }
            else if (random_position)
            {
                ennemy_behavior.setRandomPosition();
            }
            else
            {
                ennemy_behavior.setPosition(ennemies_position[i]);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
