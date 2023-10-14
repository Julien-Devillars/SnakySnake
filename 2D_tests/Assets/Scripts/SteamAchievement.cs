using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamAchievement : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (SteamManager.Initialized) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            SteamUserStats.SetAchievement("FINISH_WORLD_1");
            SteamUserStats.StoreStats();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SteamUserStats.ResetAllStats(true);
        }
    }
}
