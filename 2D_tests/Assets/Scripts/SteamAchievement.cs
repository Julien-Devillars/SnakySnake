using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamAchievement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!SteamManager.Initialized) return;
    
        if (Input.GetKeyDown(KeyCode.A))
        {
            //SteamUserStats.SetAchievement("FINISH_WORLD_1");
            SteamUserStats.SetAchievement("ACH_WIN_ONE_GAME");
            SteamUserStats.StoreStats();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SteamUserStats.ResetAllStats(true);
            SteamUserStats.StoreStats();
        }
    }

    static public void checkWorlFinish()
    {
        if (!SteamManager.Initialized) return;
        bool all_done = true;
        bool all_gold = true;
        for (int j = 0; j < Worlds.getCurrentWorld().levels.Count; ++j)
        {
            all_done &= ES3.Load<bool>($"PlayMode_World{GameControler.currentWorld}_Level{j}_status", false);
            float timer = ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_Level{GameControler.currentLevel}_timer", -1f);

            all_done &= (timer >= 0f);
            all_gold &= all_done && (timer < Worlds.getCurrentWorld().levels[j].mGoldTime);
        }
        if (all_done)
        {
            //SteamUserStats.SetAchievement("ACH_WIN_ONE_GAME");
            SteamUserStats.SetAchievement($"FINISH_WORLD_{GameControler.currentWorld + 1}");
        }
        if (all_gold)
        {
            SteamUserStats.SetAchievement($"GOLD_WORLD_{GameControler.currentWorld + 1}");
            //SteamUserStats.SetAchievement("ACH_WIN_100_GAMES");
        }
        if(all_done || all_gold)
        {
            SteamUserStats.StoreStats();
        }
    }

    static public void saveTimeInSteamStats()
    {
        if (!SteamManager.Initialized) return;

        if (!SteamUserStats.RequestCurrentStats()) return;
        float best_time;
        SteamUserStats.GetStat($"Time_{GameControler.currentWorld}_{GameControler.currentLevel}", out best_time);
        if(Timer.GetBestLevelTime() < best_time)
        {
            Debug.Log($"Save {best_time} in steam stats");
            SteamUserStats.SetStat($"Time_{GameControler.currentWorld}_{GameControler.currentLevel}", best_time);
        }
        SteamUserStats.StoreStats();
        
    }
    static public void noDeathInWorld()
    {
        if (!SteamManager.Initialized) return;

        SteamUserStats.SetAchievement($"NO_DEATH_WORLD_{GameControler.currentWorld + 1}");
        SteamUserStats.StoreStats();
    }
}
