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
    public static float GameVolume = 0.5f;
    public static int currentLevel = 0;
}