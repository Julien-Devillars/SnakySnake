using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Intervals
{
    [SerializeField] private float mSteps;
    [SerializeField] private UnityEvent mTrigger;
    private int mLastInterval;

    public float getIntervalLength(float bpm)
    {
        return 60f / (bpm * mSteps);
    }

    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != mLastInterval)
        {
            mLastInterval = Mathf.FloorToInt(interval);
            mTrigger.Invoke();
        }
    }
}

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float mBpm;
    [SerializeField] private AudioSource mAudioSource;
    [SerializeField] private Intervals[] mIntervals;

    private void Update()
    {
        foreach(Intervals interval in mIntervals)
        {
            float sampled_time = (mAudioSource.timeSamples / (mAudioSource.clip.frequency * interval.getIntervalLength(mBpm)));
            interval.CheckForNewInterval(sampled_time);
        }
    }
};

