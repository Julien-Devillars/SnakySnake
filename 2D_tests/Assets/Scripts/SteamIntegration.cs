using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SteamIntegration : MonoBehaviour
{
    public static SteamIntegration instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            try
            {
                Steamworks.SteamClient.Init(2573150);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
            return;
        }
        Destroy(this.gameObject);
    }

    private void Update()
    {
        Steamworks.SteamClient.RunCallbacks();

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var achievement in SteamUserStats.Achievements)
            {
                achievement.Clear();
            }
        }

        if(Input.GetKeyDown(KeyCode.L)) 
        {
            //SteamAchievement.checkLeaderBoard("Leaderboard_World1_Level1");
            //SteamAchievement.checkLeaderBoard("Leaderboard_World1_Level2");
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(Steamworks.SteamUserStats.SetStat("test", 10));
        }

    }

    private void OnApplicationQuit()
    {
        Debug.Log("Quit Steam Integration");
        Steamworks.SteamClient.Shutdown();
    }
}
