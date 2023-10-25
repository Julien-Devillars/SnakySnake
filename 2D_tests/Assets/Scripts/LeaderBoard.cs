using Steamworks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;


public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private List<LeaderBoardLine> mLeaderBoardLines;
    public GameObject mLeaderBoardLinesHeader;
    public static int mCurrentWorld = -1;
    public static int mCurrentLevel = -1;
    private static bool mUpdateLeaderBoard = false;
    public Animator mAnimation;

    private string mLeaderBoardFormat = "LB_BestTime_W{0:00}_L{1:00}";

    private void Start()
    {
        mLeaderBoardLines = new List<LeaderBoardLine>();

        foreach (Transform t_leaderboard_line in mLeaderBoardLinesHeader.transform)
        {
            mLeaderBoardLines.Add(t_leaderboard_line.gameObject.GetComponent<LeaderBoardLine>());
        }
    }

    public static void updateLeaderBoardWorldLevel(int world, int level)
    {
        Debug.Log("Update world" + world);
        Debug.Log("Update level" + level);
        mCurrentWorld = world;
        mCurrentLevel = level;

        mUpdateLeaderBoard = true;
    }

    void Update()
    {
        //Debug.Log(mCurrentWorld);
        //Debug.Log(mCurrentLevel);
        if (mUpdateLeaderBoard)
        {
            mAnimation.SetTrigger("Refresh");
            mUpdateLeaderBoard = false;
            updateLeaderBoard(string.Format(mLeaderBoardFormat, mCurrentWorld + 1, mCurrentLevel + 1));
        }
    }

    async public void updateLeaderBoard(string str_leaderboard)
    {
        var leaderboard = await SteamUserStats.FindLeaderboardAsync(str_leaderboard);
        if (!leaderboard.HasValue)
        {
            Debug.Log($"Leaderboard '{str_leaderboard}'not found !");
            return;
        }
        else
        {
            Debug.Log($"Found {leaderboard.Value.Name}");
        }
        var result = await leaderboard.Value.GetScoresAsync(mLeaderBoardLines.Count - 1);
        
        foreach (LeaderBoardLine leaderboard_line in mLeaderBoardLines)
        {
            leaderboard_line.display(true);
        }

        int cpt = 0;
        foreach (var res in result)
        {
            //Debug.Log($"{res.GlobalRank} -  {res.User.Name} : {res.Score}");
            mLeaderBoardLines[cpt].mPosition = res.GlobalRank;
            mLeaderBoardLines[cpt].mName = res.User.Name;
            mLeaderBoardLines[cpt].mTime = res.Score / 1000f;
            mLeaderBoardLines[cpt].overlay(res.User.IsMe);
            cpt++;
        }

        for(int i = cpt; i < mLeaderBoardLines.Count; ++i)
        {
            //Debug.Log($"Hide line {i}");
            mLeaderBoardLines[i].display(false);
        }
        mAnimation.ResetTrigger("Refresh");
    }

    }
