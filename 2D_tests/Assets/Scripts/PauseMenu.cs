using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject mPauseMenu;
    private bool mIsPaused;

    private void Start()
    {
        mIsPaused = false;
        mPauseMenu.SetActive(false);
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
        Time.timeScale = 1f;
        mIsPaused = false;
        Utils.GAME_STOPPED = false;
    }
    public void Pause()
    {
        mPauseMenu.SetActive(true);
        //Time.timeScale = 0f;
        mIsPaused = true;
        Utils.GAME_STOPPED = true;
    }

    public void Back()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
