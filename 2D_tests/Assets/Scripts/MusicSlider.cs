using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Slider>().value = GameControler.GameVolume;
    }

    public void setVolume(float volume)
    {
        ES3.Save<float>("Game_Volume", volume);
        GameControler.GameVolume = volume;
    }
}
