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
    public static GameStatus status = GameStatus.InProgress;
    public static float GameVolume = 0.5f;
    public static int ScoreFixedForLevels = 50;
}