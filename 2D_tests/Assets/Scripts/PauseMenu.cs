using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject mPauseMenu;
    public GameObject mMainMenu;
    public GameObject mOptionMenu;
    public GameObject mHelper;
    private bool mIsPaused;
    public Animator mTransitionAnimation;
    private bool mIsLoadingLevel = false;
    public TextMeshProUGUI mLevelInfo;

    private void Start()
    {
        mIsPaused = false;
        mPauseMenu.SetActive(false);
        mMainMenu.SetActive(false);
        mOptionMenu.SetActive(false);
        mHelper.SetActive(true);
        mLevelInfo.text = $"World {GameControler.currentWorld + 1} - Level {GameControler.currentLevel + 1}";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(mIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        mPauseMenu.SetActive(false);
        mOptionMenu.SetActive(false);
        mMainMenu.SetActive(false);
        Time.timeScale = 1f;
        mIsPaused = false;
        Utils.GAME_STOPPED = false;
    }
    public void Pause()
    {
        if (GameControler.status != GameControler.GameStatus.InProgress) return;
        mPauseMenu.SetActive(true);
        mMainMenu.SetActive(true);
        mOptionMenu.SetActive(false);
        //Time.timeScale = 0f;
        mIsPaused = true;
        Utils.GAME_STOPPED = true;
    }

    public void Back()
    {
        DestroyImmediate(MusicHandler.instance.gameObject);
        //SceneManager.LoadSceneAsync("MainMenu");
        StartCoroutine(LoadLevel("MainMenu"));
    }
    public void Replay()
    {
        mIsPaused = false;
        Utils.GAME_STOPPED = false;
        //SceneManager.LoadSceneAsync("MainMenu");
        StartCoroutine(LoadLevel("PlayLevel"));
    }
    public void BackOptions()
    {
        mMainMenu.SetActive(true);
        mOptionMenu.SetActive(false);
    }

    public void Option()
    {
        mMainMenu.SetActive(false);
        mOptionMenu.SetActive(true);
    }
    public void Next()
    {
        mIsPaused = false;
        Utils.GAME_STOPPED = false;
        if (GameControler.currentLevel < Worlds.worlds[GameControler.currentWorld].levels.Count - 1)
        {
            GameControler.currentLevel++;
            StartCoroutine(LoadLevel("PlayLevel"));
        }
        else
        {
            if(GameControler.currentWorld < Worlds.worlds.Count - 1)
            {
                GameControler.currentWorld++;
                GameControler.currentLevel = 0;
                StartCoroutine(LoadLevel("PlayLevel"));
            }
            else
            {
                StartCoroutine(LoadLevel("MainMenu"));
            }
        }
    }
    public void Quit()
    {
        mIsPaused = false;
        Utils.GAME_STOPPED = false;
        Application.Quit();
    }
    IEnumerator LoadLevel(string level_name)
    {
        GameControler.status = GameControler.GameStatus.Waiting;
        Time.timeScale = 1f;
        mTransitionAnimation.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.15f);
        if (!mIsLoadingLevel)
        {
            mIsLoadingLevel = true;
            SceneManager.LoadSceneAsync(level_name);
        }
    }
}
