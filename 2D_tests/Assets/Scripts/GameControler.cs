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
    public static float startTimer = 0f;

    public static void StartTimer()
    {
        Debug.Log("Start Timer");
        calculateTimer = true;
        startTimer = Time.realtimeSinceStartup;
    }
    public static void SaveTime()
    {
        if (!calculateTimer) return;
        float time = Time.realtimeSinceStartup - startTimer;
        Debug.Log("time : " + time);
        float previous_time = ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_timer", 9999999f);
        Debug.Log("previous_time : " + previous_time);
        if (previous_time < time) return;

        Debug.Log("Time Saved : " + time);
        ES3.Save<float>($"PlayMode_World{GameControler.currentWorld}_timer", time);
    }
    public static void CancelTimer()
    {
        if (allowSkip) return;
        Debug.Log("Timer cancelled");
        calculateTimer = false;
    }
}