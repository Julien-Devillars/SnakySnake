using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

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

private void Awake()
    {

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
        mLevelInfo.text = $"World {GameControler.currentWorld + 1} - Level {GameControler.currentLevel + 1}";
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
    public void Resume()
    {
        Debug.Log("Resume");
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
