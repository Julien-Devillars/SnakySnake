using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Star : MonoBehaviour
{
    private float width;
    private float height;

    private Vector3 mMinPos;
    private Vector3 mMaxPos;
    private Animator mAnimator;
    private bool active = true;

    private void Awake()
    {

        Camera cam = Camera.main;

        float cam_width = cam.aspect * cam.orthographicSize;
        float cam_height = cam.orthographicSize;

        mMinPos = new Vector3(-cam_width, -cam_height, 0);
        mMaxPos = new Vector3(cam_width, cam_height, 0);
        GetComponent<SpriteRenderer>().material = new Material(GetComponent<SpriteRenderer>().material);
        mAnimator = gameObject.AddComponent<Animator>();
        mAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Star");
        active = true;
    }
    void Start()
    {
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
        width = sprite_renderer.bounds.size.x;
        height = sprite_renderer.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = transform.position;
        Vector3 top_left = new Vector3(center.x - width / 2f, center.y + height/2f);
        Vector3 top_right = new Vector3(center.x + width / 2f, center.y + height/2f);
        Vector3 bot_left = new Vector3(center.x - width / 2f, center.y - height/2f);
        Vector3 bot_right = new Vector3(center.x + width / 2f, center.y - height/2f);
        List<Vector3> points_to_check = new List<Vector3>{ top_left, top_right, bot_left, bot_right};


        GameObject character = GameObject.Find(Utils.CHARACTER);
        SpriteRenderer character_sprite_renderer = character.GetComponent<SpriteRenderer>();
        SpriteRenderer flag_sprite_renderer = GetComponent<SpriteRenderer>();
        if (active && flag_sprite_renderer.bounds.Intersects(character_sprite_renderer.bounds) )//|| Utils.allPointsAreInBackgroundsWithoutEnemies(points_to_check))
        {
            AudioSource audio = transform.parent.GetComponent<AudioSource>();
            if(audio)
            {
                audio.volume = Mathf.Lerp(0f, 0.5f, ES3.Load<float>("VolumeSlider", 0.5f));
                audio.pitch = Random.Range(0.85f, 1f);
                audio.Play();
            }
            //flag = true;
            //SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
            //sprite_renderer.material.color = Color.red;
            mAnimator.SetTrigger("Death");
            active = false;
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
