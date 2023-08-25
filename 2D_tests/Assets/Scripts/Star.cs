using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private float width;
    private float height;
    private bool flag;

    private Vector3 mMinPos;
    private Vector3 mMaxPos;

    private void Awake()
    {

        Camera cam = Camera.main;

        float cam_width = cam.aspect * cam.orthographicSize;
        float cam_height = cam.orthographicSize;

        mMinPos = new Vector3(-cam_width, -cam_height, 0);
        mMaxPos = new Vector3(cam_width, cam_height, 0);
    }
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


        GameObject character = GameObject.Find(Utils.CHARACTER);
        SpriteRenderer character_sprite_renderer = character.GetComponent<SpriteRenderer>();
        SpriteRenderer flag_sprite_renderer = GetComponent<SpriteRenderer>();
        if (flag_sprite_renderer.bounds.Intersects(character_sprite_renderer.bounds) )//|| Utils.allPointsAreInBackgroundsWithoutEnemies(points_to_check))
        {
            gameObject.SetActive(false);
            //flag = true;
            //SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
            //sprite_renderer.material.color = Color.red;
        }
    }

    public void setRelativePosition(Vector2 relative_pos)
    {
        float x = mMinPos.x * (1 - relative_pos.x) + mMaxPos.x * relative_pos.x;
        float y = mMinPos.y * (1 - relative_pos.y) + mMaxPos.y * relative_pos.y;
        Vector2 absolute_pos = new Vector2(x, y);
        transform.position = absolute_pos;
    }
}
