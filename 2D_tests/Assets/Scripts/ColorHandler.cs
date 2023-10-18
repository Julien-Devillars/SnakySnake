using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHandler : MonoBehaviour
{
    public Color mMainColor1;
    public Color mMainColor2;
    public Color mSecondColor1;
    public Color mSecondColor2;

    public GameObject mBackgroundView;
    public GameObject mBackgroundMesh;

    public void Start()
    {
        Levels world = Worlds.getCurrentWorld();
        mMainColor1 = world.mWorldColorPrincipal_1;
        mMainColor2 = world.mWorldColorPrincipal_2;
        mSecondColor1 = world.mWorldColorSecond_1;
        mSecondColor2 = world.mWorldColorSecond_2;
    }

    public void Update()
    {
        mBackgroundView.GetComponent<BackgroundView>().mLineColorAtStart = mMainColor1;
        
        Color color = mBackgroundMesh.GetComponent<MeshRenderer>().material.GetColor("_Color");
        Color mesh_color = new Color(mMainColor1.r, mMainColor1.g, mMainColor1.b, color.a);
        mBackgroundMesh.GetComponent<MeshRenderer>().material.SetColor("_Color", mesh_color);
    }
}
