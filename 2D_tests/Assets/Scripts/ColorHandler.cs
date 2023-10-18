using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHandler : MonoBehaviour
{
    public Color mMainColor1;
    public Color mMainColor2;
    public Color mSecondColor1;
    public Color mSecondColor2;
    public float mHue;

    public GameObject mBackgroundView;
    public GameObject mBackgroundMesh;

    public void Start()
    {
        Levels world = Worlds.getCurrentWorld();
        mMainColor1 = world.mWorldColorPrincipal_1;
        mMainColor2 = world.mWorldColorPrincipal_2;
        mSecondColor1 = world.mWorldColorSecond_1;
        mSecondColor2 = world.mWorldColorSecond_2;
        mHue = world.mWorldHue;
    }

    public void changeStarColor()
    {
        GameObject stars = GameObject.Find(Utils.STARS_STR);
        foreach(Transform star in stars.transform)
        {
            SpriteRenderer sprite_renderer = star.gameObject.GetComponent<SpriteRenderer>();
            sprite_renderer.material.SetFloat("_HsvShift", mHue);
            sprite_renderer.material.SetColor("_GlowColor", mMainColor2);
        }
    }
    public void changeTrailColor()
    {
        Trail.mStartColor = mSecondColor1;
        Trail.mEndColor = mSecondColor2;
        GameObject trails = GameObject.Find(Utils.TRAILS_STR);
        foreach (Transform trail in trails.transform)
        {
            LineRenderer line_renderer = trail.gameObject.GetComponent<LineRenderer>();
            line_renderer.material.SetColor("_GlowColor", mSecondColor1);
        }
    }

    public void Update()
    {

        Levels world = Worlds.getCurrentWorld();
        mMainColor1 = world.mWorldColorPrincipal_1;
        mMainColor2 = world.mWorldColorPrincipal_2;
        mSecondColor1 = world.mWorldColorSecond_1;
        mSecondColor2 = world.mWorldColorSecond_2;
        mHue = world.mWorldHue;

        mBackgroundView.GetComponent<BackgroundView>().mLineColorAtStart = mMainColor1;
        
        Color color = mBackgroundMesh.GetComponent<MeshRenderer>().material.GetColor("_Color");
        Color mesh_color = new Color(mMainColor1.r, mMainColor1.g, mMainColor1.b, color.a);
        mBackgroundMesh.GetComponent<MeshRenderer>().material.SetColor("_Color", mesh_color);

        changeStarColor();
        changeTrailColor();
    }
}
