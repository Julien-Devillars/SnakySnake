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
        for (int j = 0; j < Worlds.worlds[GameControler.currentWorld].levels.Count; ++j)
        {
            all_done &= ES3.Load<bool>($"PlayMode_World{GameControler.currentWorld}_Level{j}_status", false);
            if (!all_done) break;
        }
        if (all_done)
        {
            SteamUserStats.SetAchievement("ACH_WIN_ONE_GAME");
            SteamUserStats.SetAchievement($"FINISH_WORLD_{GameControler.currentWorld + 1}");
            SteamUserStats.StoreStats();
        }
    }
    static public void noDeathInWorld()
    {
        if (!SteamManager.Initialized) return;

        SteamUserStats.SetAchievement("ACH_WIN_100_GAMES");
        SteamUserStats.SetAchievement($"NO_DEATH_WORLD_{GameControler.currentWorld + 1}");
        SteamUserStats.StoreStats();
    }
}
