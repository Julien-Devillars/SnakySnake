using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSlider : MonoBehaviour
{
    public void setVolume(float volume)
    {
        GameControler.GameVolume = volume;
    }
}
