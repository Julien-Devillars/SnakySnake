using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private float width;
    private float height;
    private bool flag;
    void Start()
    {
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        width = sprite_renderer.bounds.size.x;
        height = sprite_renderer.bounds.size.y;
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(flag)
        {
            return;
        }
        Vector3 center = transform.position;
        Vector3 top_left = new Vector3(center.x - width / 2f, center.y + height/2f);
        Vector3 top_right = new Vector3(center.x + width / 2f, center.y + height/2f);
        Vector3 bot_left = new Vector3(center.x - width / 2f, center.y - height/2f);
        Vector3 bot_right = new Vector3(center.x + width / 2f, center.y - height/2f);
        List<Vector3> points_to_check = new List<Vector3>{ top_left, top_right, bot_left, bot_right};

        if(allPointsAreInBackgroundsWithoutEnemies(points_to_check))
        {
            flag = true;
            SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
            sprite_renderer.material.color = Color.red;
        }
    }

    bool allPointsAreInBackgroundsWithoutEnemies(List<Vector3> points)
    {
        foreach (Vector3 point in points)
        {
            bool found = false;
            foreach (Background bg in Utils.getBackgrounds())
            {
                if (bg.contains(point) && !bg.hasEnemies())
                {
                    found = true;
                    break;
                }
            }
            if(!found)
            {
                return false;
            }
        }
        
        return true;
    }
}
