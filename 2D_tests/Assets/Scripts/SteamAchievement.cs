//using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Steamworks;
using Steamworks.Data;
using System.Threading.Tasks;
using System;
/*
public class SteamLeaderboards
{
private const string s_leaderboardName = "Leaderboard_World1_Level1";
private const ELeaderboardUploadScoreMethod s_leaderboardMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest;

private static SteamLeaderboard_t s_currentLeaderboard;
private static bool s_initialized = false;
private static CallResult<LeaderboardFindResult_t> m_findResult = new CallResult<LeaderboardFindResult_t>();
private static CallResult<LeaderboardScoreUploaded_t> m_uploadResult = new CallResult<LeaderboardScoreUploaded_t>();


public static void UpdateScore(int score)
{
if (!s_initialized)
{
UnityEngine.Debug.Log("Can't upload to the leaderboard because isn't loadded yet");
}
else
{
UnityEngine.Debug.Log("uploading score(" + score + ") to steam leaderboard(" + s_leaderboardName + ")");
SteamAPICall_t hSteamAPICall = SteamUserStats.UploadLeaderboardScore(s_currentLeaderboard, s_leaderboardMethod, score, null, 0);
m_uploadResult.Set(hSteamAPICall, OnLeaderboardUploadResult);
}
}

public static void Init()
{
SteamAPICall_t hSteamAPICall = SteamUserStats.FindLeaderboard(s_leaderboardName);
m_findResult.Set(hSteamAPICall, OnLeaderboardFindResult);
InitTimer();
}

static private void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool failure)
{
UnityEngine.Debug.Log("STEAM LEADERBOARDS: Found - " + pCallback.m_bLeaderboardFound + " leaderboardID - " + pCallback.m_hSteamLeaderboard.m_SteamLeaderboard);
s_currentLeaderboard = pCallback.m_hSteamLeaderboard;
s_initialized = true;
}

static private void OnLeaderboardUploadResult(LeaderboardScoreUploaded_t pCallback, bool failure)
{
UnityEngine.Debug.Log("STEAM LEADERBOARDS: failure - " + failure + " Completed - " + pCallback.m_bSuccess + " NewScore: " + pCallback.m_nGlobalRankNew + " Score " + pCallback.m_nScore + " HasChanged - " + pCallback.m_bScoreChanged);
}



private static System.Threading.Timer timer1;
public static void InitTimer()
{
timer1 = new System.Threading.Timer(timer1_Tick, null, 0, 1000);
}

private static void timer1_Tick(object state)
{
SteamAPI.RunCallbacks();
}
}

*/
public class SteamAchievement : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach(var achievement in SteamUserStats.Achievements)
            {
                achievement.Clear();
            }
        }
    }
    
    static public void checkWorlFinish()
    {
        bool all_done = true;
        bool all_gold = true;

        for (int j = 0; j < Worlds.getCurrentWorld().levels.Count; ++j)
        {
            all_done &= ES3.Load<bool>($"PlayMode_World{GameControler.currentWorld}_Level{j}_status", false);
            float timer = ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_Level{j}_timer", -1f);

            all_done &= (timer >= 0f);
            all_gold &= all_done && (timer < Worlds.getCurrentWorld().levels[j].mGoldTime);
            //Debug.Log($"{j} -> {all_gold} : {timer} < {Worlds.getCurrentWorld().levels[j].mGoldTime} ({timer < Worlds.getCurrentWorld().levels[j].mGoldTime})");
            //Debug.Log(all_gold);
        }
        if (all_done)
        {
            var achievement = new Achievement($"FINISH_WORLD_{GameControler.currentWorld + 1}");
            achievement.Trigger();
        }
        if (all_gold)
        {
            var achievement = new Achievement($"GOLD_WORLD_{GameControler.currentWorld + 1}");
            achievement.Trigger();
        }
    }
    static public void noDeathInWorld()
    {
        var achievement = new Achievement($"NO_DEATH_WORLD_{GameControler.currentWorld + 1}");
        achievement.Trigger();
    }

    static async public void addBestTimeInLeaderBoard(float best_score)
    {
        string leaderboard_name = string.Format("LB_BestTime_W{0:00}_L{1:00}", GameControler.currentWorld + 1, GameControler.currentLevel + 1);

        var leaderboard = await SteamUserStats.FindLeaderboardAsync(leaderboard_name);
        if (!leaderboard.HasValue)
        {
            Debug.Log($"Leaderboard '{leaderboard_name}'not found !");
            return;
        }
        else
        {
            Debug.Log($"Found {leaderboard.Value.Name}");
            Debug.Log($" - Display : {leaderboard.Value.Display}");
            Debug.Log($" - Entries : {leaderboard.Value.EntryCount}");
        }

        var result = await leaderboard.Value.SubmitScoreAsync((int)(best_score * 1000f));

        if (result.HasValue)
        {
            Debug.Log(result.Value.Score);
            Debug.Log(result.Value.OldGlobalRank);
            Debug.Log(result.Value.NewGlobalRank);
            if (result.Value.Changed)
            {
                Debug.Log("Value Changed in leaderboard");
            }
            else
            {
                Debug.Log("Value NOT Changed in leaderboard");
            }
        }
    }

    static async public void checkLeaderBoard(string str_leaderboard)
    {
        var leaderboard = await SteamUserStats.FindLeaderboardAsync(str_leaderboard);
        if(!leaderboard.HasValue)
        {
            Debug.Log($"Leaderboard '{str_leaderboard}'not found !");
            return;
        }
        else
        {
            Debug.Log($"Found {leaderboard.Value.Name}");
            Debug.Log($" - Display : {leaderboard.Value.Display}");
            Debug.Log($" - Entries : {leaderboard.Value.EntryCount}");
        }
        var result = await leaderboard.Value.GetScoresAsync(5);

        foreach (var res in result)
        {
            Debug.Log($"{res.User.Name} has {res.Score}");

        }
            /*if(result.HasValue)
        {
            Debug.Log(result.Value.Score);
            Debug.Log(result.Value.OldGlobalRank);
            Debug.Log(result.Value.NewGlobalRank);
            if (result.Value.Changed)
            {
                Debug.Log("Value Changed in leaderboard");
            }
            else
            {
                Debug.Log("Value NOT Changed in leaderboard");
            }
        }*/
    }

}
