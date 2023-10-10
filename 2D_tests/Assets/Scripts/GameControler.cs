using Codice.CM.Client.Differences;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameControler
{
    public enum GameStatus
    {
        Win,
        Lose,
        InProgress,
        Waiting
    }
    public enum GameType
    {

        None,
        Play,
        Infinity
    }
    public static GameStatus status = GameStatus.InProgress;
    public static GameType type = GameType.None;
    public static int currentLevel = 0;
    public static int currentWorld = 0;
    public static bool calculateTimer = false;
    public static bool allowSkip = true;
    public static float startWorldTimer = 0f;
    public static float startLevelTimer = 0f;

    public static void StartLevelTimer()
    {
        startLevelTimer = Time.realtimeSinceStartup;
    }
    public static void SaveLevelTime()
    {
        float time = Time.realtimeSinceStartup - startLevelTimer;
        float previous_time = ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_Level{GameControler.currentLevel}_timer", 9999999f);
        Debug.Log("Level Time : " + time);
        if (previous_time < time) return;
        Debug.Log("Best -> Saved");
        ES3.Save<float>($"PlayMode_World{GameControler.currentWorld}_Level{GameControler.currentLevel}_timer", time);
    }
    public static void SaveWorldTime()
    {
        if (!calculateTimer) return;
        float time = Time.realtimeSinceStartup - startWorldTimer;
        Debug.Log("World Time : " + time);
        float previous_time = ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_timer", 9999999f);
        
        if (previous_time < time) return;
        Debug.Log("Best -> Saved");
        ES3.Save<float>($"PlayMode_World{GameControler.currentWorld}_timer", time);
    }
    public static void StartWorldTimer()
    {
        Debug.Log("Start World Timer");
        calculateTimer = true;
        startWorldTimer = Time.realtimeSinceStartup;
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
    }
}