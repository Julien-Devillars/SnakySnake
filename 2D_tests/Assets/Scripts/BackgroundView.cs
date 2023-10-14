using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundView : MonoBehaviour
{
    // Start is called before the first frame update
    private Color mLineColorAtStart;
    void Start()
    {
        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        Vector3 min_pos = new Vector3(-width, -height, 1);
        Vector3 max_pos = new Vector3(width, height, 1);

        transform.position = (max_pos + min_pos) / 2;
        float scale_x = max_pos.x - min_pos.x;
        float scale_y = max_pos.y - min_pos.y;
        transform.localScale = new Vector3(scale_x, scale_y, 1);

        SpriteRenderer sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        if(Utils.SHADER_ON)
        {
            sprite_renderer.material = (Material)Resources.Load("Shaders/GlowBackground", typeof(Material));
            mLineColorAtStart = sprite_renderer.material.GetColor("_LineColor");
        }
        else
        {
            sprite_renderer.material = Resources.Load<Material>("Photoshop/Materials/Background");
            sprite_renderer.sprite = Resources.Load<Sprite>("Photoshop/Background");
        }
        sprite_renderer.material.color = Color.white;

    }

    void Update()
    {
        if (Utils.SHADER_ON)
        {
            Material material = GetComponent<SpriteRenderer>().material;

            // Set Line size between 0.02 & 0.05
            //material.SetFloat("_LineSize", Mathf.PingPong(Time.time, 3f) / 100f + 0.02f);


            // Set Line color intensity between 1 and 2
            float min = 0.1f;
            float max = 0.5f;
            float duration = 2f;
            float intensity = Mathf.PingPong(Time.time / duration, max - min) + min;
            Color new_color = new Color(mLineColorAtStart.r * intensity, mLineColorAtStart.g * intensity, mLineColorAtStart.b * intensity);
            //material.SetColor("_LineColor", new_color);
        }

            
    }

}
