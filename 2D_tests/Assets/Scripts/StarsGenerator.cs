using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsGenerator : MonoBehaviour
{
    private List<GameObject> stars;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject level_controller_go = GameObject.Find(Utils.LEVEL_STR);
        //LevelSettings settings = level_controller_go.GetComponent<LevelSettings>();

        //Vector2 scale = settings.scale;
        //List<Vector2> star_positions = settings.star_positions;

        //stars = new List<GameObject>();

        //for (int i = 0; i < star_positions.Count; ++i)
        //{
        //    GameObject star_go = new GameObject();
        //    star_go.name = Utils.STAR_STR + "_" + i.ToString();
        //    star_go.tag = Utils.STAR_STR;
        //    star_go.layer = 13; // Star Layer
        //    star_go.transform.localScale = new Vector3(scale.x, scale.y, 0.5f);
        //    star_go.transform.parent = gameObject.transform;
        //
        //
        //    // Add Sprite Renderer
        //    SpriteRenderer sprite_renderer = star_go.AddComponent<SpriteRenderer>();
        //    sprite_renderer.sprite = Resources.Load<Sprite>("Photoshop/StarSprite");
        //    if (Utils.SHADER_ON)
        //    {
        //        //sprite_renderer.material = Resources.Load<Material>("Materials/StarMaterial");
        //        sprite_renderer.material = new Material(Shader.Find("Sprites/Default"));
        //    }
        //    else
        //    {
        //        sprite_renderer.material = new Material(Shader.Find("Sprites/Default"));
        //    }
        //    sprite_renderer.material.color = Color.yellow;
        //    sprite_renderer.sortingOrder = 1;
        //
        //    Star star_script = star_go.AddComponent<Star>();
        //
        //    star_go.transform.position = star_positions[i];
        //
        //    stars.Add(star_go);
        //}

        stars = new List<GameObject>();
        foreach (StarInfo star in Levels.levels[GameControler.currentLevel].mStars)
        {
            GameObject star_go = new GameObject();
            star_go.name = Utils.STAR_STR;
            star_go.tag = Utils.STAR_STR;
            star_go.layer = 13; // Star Layer
            star_go.transform.localScale = new Vector3(star.scale, star.scale, 0.5f);
            star_go.transform.parent = gameObject.transform;

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
