using Codice.Client.Common.GameUI;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SteamIntegration : MonoBehaviour
{
    public static SteamIntegration instance = null;
    public static bool mHasSteam = false;
    private float mTimeToReconnect = 10f;
    IEnumerator tryToConnectToSteam()
    {
        while (!mHasSteam)
        {
            yield return new WaitForSeconds(mTimeToReconnect);
            try
            {
                Steamworks.SteamClient.Init(2573150);
                Debug.Log("Steam Found");
                mHasSteam = true;
            }
            catch (System.Exception e)
            {
                Debug.Log("Steam not found");
                Debug.Log(e);
                mHasSteam = false;
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            try
            {
                Steamworks.SteamClient.Init(2573150);
                mHasSteam = true;
            }
            catch (System.Exception e)
            {
                Debug.Log("Steam not found");
                Debug.Log(e);
                mHasSteam = false;
                StartCoroutine(tryToConnectToSteam());
            }
            return;
        }
        Destroy(this.gameObject);
    }

    private void Update()
    {
        Steamworks.SteamClient.RunCallbacks();

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    foreach (var achievement in SteamUserStats.Achievements)
        //    {
        //        achievement.Clear();
        //    }
        //}
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Quit Steam Integration");
        Steamworks.SteamClient.Shutdown();
    }
}
