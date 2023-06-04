using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    private AudioSource mAudioSource;
    private static MusicHandler instance = null;
    [SerializeField]
    private List<AudioClip> mMusics;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            mAudioSource = GetComponent<AudioSource>();
            int idx_music = Random.Range(0, mMusics.Count);
            mAudioSource.clip = mMusics[idx_music];
            Debug.Log("Music playing : " + mAudioSource.clip.name);
            mAudioSource.Play();
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
}