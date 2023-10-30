using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamIntegration : MonoBehaviour
{
    public static SteamIntegration instance = null;
    public static bool mHasSteam = false;
    public bool mTryToReconnect = false;
    private float mTimeToReconnect = 10f;
    IEnumerator tryToConnectToSteam()
    {
        mTryToReconnect = true;
        yield return new WaitForSeconds(mTimeToReconnect);
        try
        {
            Steamworks.SteamClient.Shutdown();
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
        mTryToReconnect = false;
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
            }
            return;
        }
        Destroy(this.gameObject);
    }

    private void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
        if(!mTryToReconnect && !mHasSteam)
        {
            Debug.Log("Try to reconnect to steam");
            StartCoroutine(tryToConnectToSteam());
        }
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
