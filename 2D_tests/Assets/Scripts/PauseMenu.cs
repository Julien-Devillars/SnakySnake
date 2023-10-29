using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using log4net.Core;

public class PauseMenu : MonoBehaviour
{
    public GameObject mPauseMenu;
    public GameObject mMainMenu;
    public Button mMainMenuButtonFirstSelected;
    public Button mMainMenuButtonOnBackSelected;
    public GameObject mOptionMenu;
    public Button mOptionMenuButtonFirstSelected;
    public GameObject mHelper;
    private bool mIsPaused;
    public Animator mTransitionAnimation;
    private bool mIsLoadingLevel = false;
    public TextMeshProUGUI mLevelInfo;
    private DefaultInputActions mDefaultInputActions;
    private PlayerControl mPlayerControl;


    public AudioSource mAudioNavigate;
    public AudioSource mAudioSubmitBack;
    private GameObject mPreviousSelected = null;
    public GameObject mClearLevel;
    private bool mMenuWaiter;

private void Awake()
    {
        mMenuWaiter = false;
        mDefaultInputActions = new DefaultInputActions();
        mPlayerControl = new PlayerControl();
        mDefaultInputActions.UI.Cancel.performed += ctx =>
        {
            if (mOptionMenu.activeSelf)
            {
                BackOptions();
            }
        };
        mDefaultInputActions.UI.Cancel.performed += ctx =>
        {
            if (mMenuWaiter) return;
            if (mIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        };
        mPlayerControl.PlayerController.Escape.performed += ctx =>
        {
            if (mMenuWaiter) return;
            if (mIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        };
        mDefaultInputActions.UI.Submit.performed += ctx =>
        {
            mAudioSubmitBack.Play();
        };
        mDefaultInputActions.UI.Navigate.performed += ctx =>
        {
            if (GameControler.status == GameControler.GameStatus.Lose || GameControler.status == GameControler.GameStatus.Win) return;
            if (!mIsPaused) return;
            if (!mMenuWaiter) return;
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            Debug.Log(selected.name);
            if (selected != null && selected.GetComponent<ButtonHandler>()) return;
            if (selected != null && selected.GetComponent<Slider>())
            {
                selected.transform.parent.gameObject.GetComponent<Button>().Select();
                return;
            }
            if (selected == null || selected.name == "Text")// || (selected.GetComponent<ButtonHandler>() == null && selected.GetComponent<SliderHandler>() == null))
            {
                if(mMainMenu.activeSelf)
                {
                    mMainMenuButtonFirstSelected.Select();
                }
                else if (mOptionMenu.activeSelf)
                {
                    mOptionMenuButtonFirstSelected.Select();
                }
            }
        };
    }
    private void Start()
    {
        mIsPaused = false;
        mPauseMenu.SetActive(false);
        mMainMenu.SetActive(false);
        mOptionMenu.SetActive(false);
        mHelper.SetActive(true);
        string world_tr = Translation.GetTranslation("World", ES3.Load<SystemLanguage>("Language", Application.systemLanguage));
        string level_tr = Translation.GetTranslation("Level", ES3.Load<SystemLanguage>("Language", Application.systemLanguage));
        mLevelInfo.text = $"{world_tr} {GameControler.currentWorld + 1} - {level_tr} {GameControler.currentLevel + 1}";
        mClearLevel.SetActive(false);
        
    }

    private void OnEnable()
    {
        mDefaultInputActions.UI.Enable();
        mPlayerControl.PlayerController.Enable();
    }

    private void OnDisable()
    {
        mDefaultInputActions.UI.Disable();
        mPlayerControl.PlayerController.Disable();
    }
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != mPreviousSelected)
        {
            if(mPreviousSelected != null) mAudioNavigate.Play();
            mPreviousSelected = EventSystem.current.currentSelectedGameObject;
        }
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if(mIsPaused)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        Pause();
        //    }
        //}
    }
    IEnumerator waiterMenu()
    {

        mMenuWaiter = true;
        yield return new WaitForSeconds(0.1f);
        mMenuWaiter = false;
    }
    public void Resume()
    {
        Debug.Log("Resume");
        StartCoroutine(waiterMenu());
        mPauseMenu.SetActive(false);
        mOptionMenu.SetActive(false);
        mMainMenu.SetActive(false);
        Time.timeScale = 1f;
        mIsPaused = false;
        Utils.GAME_STOPPED = false;
        Timer.EndLevelPauseTimer();
    }
    public void Pause()
    {
        Debug.Log("Pause");
        StartCoroutine(waiterMenu());
        if (GameControler.status != GameControler.GameStatus.InProgress) return;
        mPauseMenu.SetActive(true);
        mMainMenu.SetActive(true);
        mOptionMenu.SetActive(false);
        //Time.timeScale = 0f;
        mIsPaused = true;
        Utils.GAME_STOPPED = true;
        mMainMenuButtonFirstSelected.Select();
        Timer.StartLevelPauseTimer();
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
        mMainMenuButtonOnBackSelected.Select();
    }

    public void Option()
    {
        mMainMenu.SetActive(false);
        mOptionMenu.SetActive(true);
        mOptionMenuButtonFirstSelected.Select();
    }


    public int nextLevelIsLock()
    {
        int cpt = 0;
        for (int i = 0; i <  Worlds.worlds[GameControler.currentWorld].levels.Count - 1; ++i)
        {
            bool level_done = ES3.Load<bool>($"PlayMode_World{GameControler.currentWorld}_Level{i}_status", false);
            if (level_done)
            {
                cpt++;
            }
        }

        return cpt;
    }

    private bool hasNext = false;
    public void Next()
    {
        if (hasNext) return;
        hasNext = true;
        mIsPaused = false;
        Utils.GAME_STOPPED = false;
        Timer.CancelTimer();
        if (GameControler.currentLevel < Worlds.worlds[GameControler.currentWorld].levels.Count - 1)
        {
            GameControler.currentLevel++;
            StartCoroutine(LoadLevel("PlayLevel"));
        }
        else
        {
            if(GameControler.currentWorld < Worlds.worlds.Count - 1)
            {
                int nb_level_done = nextLevelIsLock();
                if(nb_level_done < Worlds.mNBLevelToNext)
                {
                    GameControler.currentWorld++;
                    StartCoroutine(LoadLevel("MainMenu"));
                    return;
                }

                GameControler.currentWorld++;
                GameControler.currentLevel = 0;
                Timer.StartWorldTimer();
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
