using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
