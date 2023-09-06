using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenu : MonoBehaviour
{
    //public GameObject mMainMenu; // 0
    //public GameObject mPlayMenu; // 1
    //public GameObject mLevelsMenu; // 2
    //public GameObject mInfinityMenu; //3
    //public GameObject mOptionsMenu; // 4
    //public GameObject mCreditsMenu; // 5

    public List<GameObject> mMenus = new List<GameObject>();
    public List<TextMeshProUGUI> mInfinityBestScoreText = new List<TextMeshProUGUI>();

    public int mPreviousIndexMenu = 0;
    public void Start()
    {
        Time.timeScale = 1f;
        if (MusicHandler.instance != null)
        {
            DestroyImmediate(MusicHandler.instance.gameObject);
        }
        foreach (GameObject menu in mMenus)
        {
            menu.SetActive(false);
        }
        mMenus[0].SetActive(true);

        Utils.GAME_STOPPED = false;
        mPreviousIndexMenu = 0;
        GameControler.type= GameControler.GameType.None;
        mInfinityBestScoreText[0].text = "Easy - Best : " + ES3.Load<int>("Infinity_HighScore_1", 0).ToString();
        mInfinityBestScoreText[1].text = "Medium - Best : " + ES3.Load<int>("Infinity_HighScore_2", 0).ToString();
        mInfinityBestScoreText[2].text = "Hard - Best : " + ES3.Load<int>("Infinity_HighScore_3", 0).ToString();

        Worlds.createWorlds();
    }

    public void switchMenu(int index)
    {
        mMenus[mPreviousIndexMenu].SetActive(false);
        mMenus[index].SetActive(true);
        mPreviousIndexMenu = index;
    }

    public void Update()
    {
        GetComponent<AudioSource>().volume = ES3.Load<float>("VolumeSlider", 0.5f); ;
    }

    public void PlayGame()
    {
        Debug.Log("Click Button");
        SceneManager.LoadScene("Level_1");
        Debug.Log("After Async");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LaunchInfinityLevel(int difficulty)
    {
        GameControler.type = GameControler.GameType.Infinity;
        InfinityControler.mDifficulty = difficulty;
        InfinityControler.mScore = 25 * difficulty;
        InfinityControler.mCurrentLevel = 1;
        SceneManager.LoadScene("InfinityLevel");
    }
}
