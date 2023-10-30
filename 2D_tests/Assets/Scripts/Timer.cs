using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Timer
{
    public static bool calculateTimer = false;
    public static bool allowSkip = false;
    public static float startWorldTimer = 0f;
    public static float startLevelTimer = 0f;
    public static float currentPauseLevelTimer = 0f;
    public static float totalPauseWorldTimer = 0f;
    public static float totalPauseLevelTimer = 0f;
    public static int deathCountInLevel = 0;

    static public bool hasDiedInWorld = false;
    public static void StartLevelPauseTimer()
    {
        currentPauseLevelTimer = Time.realtimeSinceStartup;
    }
    public static void StartWorldPauseTimer()
    {
        totalPauseWorldTimer = 0f;
    }
    public static void EndLevelPauseTimer()
    {
        totalPauseLevelTimer += Time.realtimeSinceStartup - currentPauseLevelTimer;
        totalPauseWorldTimer += Time.realtimeSinceStartup - currentPauseLevelTimer;
        //Debug.Log("totalPauseLevelTimer : " + totalPauseLevelTimer);
        //Debug.Log("totalPauseWorldTimer : " + totalPauseWorldTimer);
    }
    public static void StartLevelTimer()
    {
        startLevelTimer = Time.realtimeSinceStartup;
        totalPauseLevelTimer = 0f;
    }
    public static float GetLevelTime()
    {
        float time = Time.realtimeSinceStartup - startLevelTimer - totalPauseLevelTimer;
        return time;
    }
    public static float GetBestLevelTime()
    {
        return ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_Level{GameControler.currentLevel}_timer", -1f);
    }

    public static void SaveLevelTime()
    {
        float time = GetLevelTime();
        float previous_time = GetBestLevelTime();
        Debug.Log("Level Time : " + time);
        SteamAchievement.addBestTimeInLeaderBoard(time);
        if (previous_time >= 0f && previous_time < time) return;
        Debug.Log("Best -> Saved");
        ES3.Save<float>($"PlayMode_World{GameControler.currentWorld}_Level{GameControler.currentLevel}_timer", time);
    }
    public static void SaveWorldTime()
    {

        if (!calculateTimer) return;
        if (!hasDiedInWorld)
        {
            SteamAchievement.noDeathInWorld();
        }
        float time = Time.realtimeSinceStartup - startWorldTimer - totalPauseWorldTimer;

        float previous_time = ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_timer", -1f);
        SteamAchievement.addBestWorldTimeInLeaderBoard(time);

        if (previous_time >= 0f && previous_time < time) return;
        ES3.Save<float>($"PlayMode_World{GameControler.currentWorld}_timer", time);
    }
    public static void StartWorldTimer()
    {
        Debug.Log("Start World Timer");
        calculateTimer = true;
        startWorldTimer = Time.realtimeSinceStartup;
        hasDiedInWorld = false;
        StartWorldPauseTimer();
    }
    public static void CancelTimer()
    {
        if (allowSkip) return;
        Debug.Log("Timer cancelled");
        calculateTimer = false;
    }

    public static void addDeath()
    {
        int death_world = ES3.Load<int>($"PlayMode_World{GameControler.currentWorld}_death", 0);
        ES3.Save<int>($"PlayMode_World{GameControler.currentWorld}_death", ++death_world);
        int death_level = ES3.Load<int>($"PlayMode_World{GameControler.currentWorld}_Level{GameControler.currentLevel}_death", 0);
        ES3.Save<int>($"PlayMode_World{GameControler.currentWorld}_Level{GameControler.currentLevel}_death", ++death_level);
        
        deathCountInLevel++;
        
        hasDiedInWorld = true;
    }
    public static void resetDeathLevelCounter()
    {
        deathCountInLevel = 0;
    }
}