using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    private AudioSource mAudioSource;
    public static MusicHandler instance = null;
    [SerializeField]
    private List<AudioClip> mMusics;

    public float stop_volume = 0.05f;
    public float smooth_time_on_stop = 0.15f;

    BeatHandler mBeatHandler;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            mAudioSource = GetComponent<AudioSource>();
            AudioClip music = Resources.Load<AudioClip>("Musics/Synthwave/20 - Galaxy");

            //int idx_music = Random.Range(0, musics.Length);
            mAudioSource.clip = music;// musics[idx_music];
            Debug.Log("Music playing : " + mAudioSource.clip.name);
            mAudioSource.Play();
            mAudioSource.volume = ES3.Load<float>("VolumeSlider", 0.5f); ;
            //mBeatHandler = new BeatHandler();
            return;
        }
        mAudioSource = GetComponent<AudioSource>();
        mAudioSource.Stop();
        Destroy(this.gameObject);
    }

    public void PlayMusic()
    {
        if (mAudioSource.isPlaying) return;
        mAudioSource.Play();
    }

    public void StopMusic()
    {
        mAudioSource.Stop();
    }
    int cpt = 0;

    void Update()
    {

        float[] spectrum = new float[1024];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        float shift = 0f;
        float new_volume;
        float current_volume = ES3.Load<float>("VolumeSlider", 0.5f);
        if (Utils.GAME_STOPPED)
        {
            GameObject option_menu = GameObject.Find("Options Menu");
            if (option_menu != null && option_menu.activeSelf)
            {
                new_volume = current_volume;
            }
            else
            {
                if (current_volume > stop_volume)
                {
                    new_volume = Mathf.SmoothDamp(mAudioSource.volume, stop_volume, ref shift, smooth_time_on_stop);
                }
                else
                {
                    new_volume = Mathf.SmoothDamp(mAudioSource.volume, current_volume, ref shift, smooth_time_on_stop);
                }
            }
        }
        else
        {
            new_volume = Mathf.SmoothDamp(mAudioSource.volume, current_volume, ref shift, smooth_time_on_stop);
        }
        mAudioSource.volume = new_volume;

        //mBeatHandler.addSpectrum(spectrum);
        //Debug.Log(mBeatHandler.isBeat(spectrum));
        //
        //float avg = 0f;
        //for(int i = 0; i < 1024; ++i)
        //{
        //    avg += spectrum[i];
        //}
    }
}

class Spectrum
{
    int nbData = 1024;
    public float mAverage;
    public Spectrum(float[] spectrum_data)
    {
        mAverage = 0f;
        for (int i = 0; i < nbData; ++i)
        {
            mAverage += spectrum_data[i];
        }
        mAverage /= nbData;
    }
}

class BeatHandler
{
    int mNbSamples = 15;
    float mThreshold = 0.0001f;
    List<Spectrum> mSpectrums;
    public BeatHandler()
    {
        mSpectrums = new List<Spectrum>();
    }
    public void addSpectrum(float[] spectrum_data)
    {
        Spectrum spectrum = new Spectrum(spectrum_data);
        mSpectrums.Add(spectrum);
    }
    public float getAverageSpectrum()
    {
        float average = 0f;
        for(int i = mSpectrums.Count - mNbSamples; i < mSpectrums.Count; ++i)
        {
            Spectrum spectrum = mSpectrums[i];
            average += spectrum.mAverage;
        }

        return average / mNbSamples;
    }
    public bool isBeat(Spectrum spectrum)
    {
        if(mSpectrums.Count > mNbSamples)
        {
            float average = getAverageSpectrum();
            //Debug.Log("All Average : " + average);
            //Debug.Log("Current Average : " + spectrum.mAverage);
            if (spectrum.mAverage > average + mThreshold)
            {
                return true;
            }
        }
        return false;
    }
    public bool isBeat(float[] spectrum_data)
    {
        Spectrum spectrum = new Spectrum(spectrum_data);
        return isBeat(spectrum);
    }
}
