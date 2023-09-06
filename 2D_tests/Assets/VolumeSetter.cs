using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class VolumeSetter : MonoBehaviour
{

    void Start()
    {
        GetComponent<AudioSource>().volume = ES3.Load<float>("VolumeSlider", 0.5f);
    }

    void Update()
    {
        GetComponent<AudioSource>().volume = ES3.Load<float>("VolumeSlider", 0.5f);
    }
}
