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
    }

    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }
}
