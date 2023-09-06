using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumerSetter : MonoBehaviour
{
    private Volume mVolume;
    private Bloom mBloom;

    void Start()
    {
        mVolume = GetComponent<Volume>();
        mVolume.profile.TryGet(out mBloom);
        mBloom.intensity.value = ES3.Load<float>("LuminositySlider", 0.5f);
    }

    void Update()
    {
        mBloom.intensity.value = ES3.Load<float>("LuminositySlider", 0.5f);
    }
}
