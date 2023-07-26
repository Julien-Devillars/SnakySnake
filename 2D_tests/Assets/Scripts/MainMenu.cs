using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //public GameObject mMainMenu; // 0
    //public GameObject mPlayMenu; // 1
    //public GameObject mLevelsMenu; // 2
    //public GameObject mInfinityMenu; //3
    //public GameObject mOptionsMenu; // 4
    //public GameObject mCreditsMenu; // 5

    public List<GameObject> mMenus = new List<GameObject>();

    public int mPreviousIndexMenu = 0;
    public void Start()
    {
        GameControler.GameVolume = 0.5f;
        foreach(GameObject menu in mMenus)
        {
            menu.SetActive(false);
        }
        mMenus[0].SetActive(true);

        Utils.GAME_STOPPED = false;
        mPreviousIndexMenu = 0;
        InfinityControler.mIsInfinity = false;
    }

    public void switchMenu(int index)
    {
        mMenus[mPreviousIndexMenu].SetActive(false);
        mMenus[index].SetActive(true);
        mPreviousIndexMenu = index;
    }

    public void Update()
    {
        GetComponent<AudioSource>().volume = GameControler.GameVolume;
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
        InfinityControler.mIsInfinity = true;
        InfinityControler.mScore= 25 * difficulty;
        InfinityControler.mCurrentLevel = 1;
        SceneManager.LoadScene("InfinityLevel");
    }
}
