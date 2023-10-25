using Steamworks;
using Steamworks.Data;
using Steamworks.ServerList;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
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
    public enum Sorting
    {
        World,
        Friends,
        Local
    }
    static public Sorting mSorting = Sorting.Local; 

    private void Start()
    {
        mLeaderBoardLines = new List<LeaderBoardLine>();

        foreach (Transform t_leaderboard_line in mLeaderBoardLinesHeader.transform)
        {
            mLeaderBoardLines.Add(t_leaderboard_line.gameObject.GetComponent<LeaderBoardLine>());
        }
        mSorting = ES3.Load<Sorting>("LD_SORTING", Sorting.Local);
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
        LeaderboardEntry[] result = null;
        LeaderboardEntry[] my_result = null;
        if (mSorting == Sorting.World)
        {
            result = await leaderboard.Value.GetScoresAsync(mLeaderBoardLines.Count - 2);
            my_result = await leaderboard.Value.GetScoresAroundUserAsync(0, 0);

        }
        else if (mSorting == Sorting.Friends)
        {
            result = await leaderboard.Value.GetScoresFromFriendsAsync();
        }
        else if (mSorting == Sorting.Local)
        {
            if(mLeaderBoardLines.Count % 2 == 0)
            {
                result = await leaderboard.Value.GetScoresAroundUserAsync(-(mLeaderBoardLines.Count-1) / 2, (mLeaderBoardLines.Count-1) / 2);
            }
            else
            {
                result = await leaderboard.Value.GetScoresAroundUserAsync(-(mLeaderBoardLines.Count-1) / 2, (mLeaderBoardLines.Count-2) / 2);
            }
        }

        foreach (LeaderBoardLine leaderboard_line in mLeaderBoardLines)
        {
            leaderboard_line.display(true);
        }

        int cpt = 1;
        bool user_in_list = false;
        foreach (var res in result)
        {
            //Debug.Log($"{res.GlobalRank} -  {res.User.Name} : {res.Score}");
            // Do not handle title line
            mLeaderBoardLines[cpt].mPosition = res.GlobalRank;
            mLeaderBoardLines[cpt].mName = res.User.Name;
            mLeaderBoardLines[cpt].mTime = res.Score / 1000f;
            mLeaderBoardLines[cpt].overlay(res.User.IsMe);
            user_in_list |= res.User.IsMe;
            cpt++;
        }

        if((mSorting == Sorting.World || mSorting == Sorting.Friends) && !user_in_list && my_result.Length == 1)
        {
            mLeaderBoardLines[cpt - 1].mPosition = my_result[0].GlobalRank;
            mLeaderBoardLines[cpt - 1].mName = my_result[0].User.Name;
            mLeaderBoardLines[cpt - 1].mTime = my_result[0].Score / 1000f;
            mLeaderBoardLines[cpt - 1].overlay(true);
        }

        for(int i = cpt; i < mLeaderBoardLines.Count; ++i)
        {
            //Debug.Log($"Hide line {i}");
            mLeaderBoardLines[i].display(false);
        }
        mAnimation.ResetTrigger("Refresh");
    }

    public void changeSorting(int sorting)
    {
        mSorting = (Sorting)sorting;
        mUpdateLeaderBoard = true;
    }

}
