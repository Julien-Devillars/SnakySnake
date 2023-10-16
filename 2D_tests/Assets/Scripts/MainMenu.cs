using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

[System.Serializable]
public struct ButtonWithSelect
{
    [SerializeField] public GameObject mMenu;
    [SerializeField] public Button mButton;
    [SerializeField] public Button mBackButton;
}

public class MainMenu : MonoBehaviour
{
    //public GameObject mMainMenu; // 0
    //public GameObject mPlayMenu; // 1
    //public GameObject mLevelsMenu; // 2
    //public GameObject mInfinityMenu; //3
    //public GameObject mOptionsMenu; // 4
    //public GameObject mCreditsMenu; // 5

    //public List<Button> mSelectedInMenu = new List<Button>();
    public List<ButtonWithSelect> mMenusWithSelected = new List<ButtonWithSelect>();
    private DefaultInputActions mDefaultInputActions;
    public AudioSource mAudioMusic;
    public AudioSource mAudioNavigate;
    public AudioSource mAudioSubmitBack;
    private GameObject mPreviousSelected = null;
    public int mPreviousIndexMenu = 0;
    private void Awake()
    {

        mDefaultInputActions = new DefaultInputActions();

        mDefaultInputActions.UI.Cancel.performed += ctx =>
        {
            for (int i = 1;  i < mMenusWithSelected.Count; ++i)
            {
                ButtonWithSelect back_menu = mMenusWithSelected[i];

                if (back_menu.mMenu.activeSelf)
                {
                    mMenusWithSelected[mPreviousIndexMenu].mMenu.SetActive(false);
                    mMenusWithSelected[0].mMenu.SetActive(true);
                    mPreviousIndexMenu = 0;
                    back_menu.mBackButton.Select();
                    return;
                }
            }
        };

        mDefaultInputActions.UI.Navigate.performed += ctx =>
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            if (selected != null && selected.GetComponent<Slider>())
            {
                selected.transform.parent.gameObject.GetComponent<Button>().Select();
                return;
            }
            if (selected == null || selected.name == "Text")
            {
                mMenusWithSelected[mPreviousIndexMenu].mButton.Select();
            }
        };

        //mDefaultInputActions.UI.Submit.performed += ctx =>
        //{
        //    mAudioSubmitBack.Play();
        //};
        //mDefaultInputActions.UI.Cancel.performed += ctx =>
        //{
        //    if (mPreviousIndexMenu == 0) return;
        //    mAudioSubmitBack.Play();
        //};
        Timer.CancelTimer();
    }

    public void Start()
    {
        Time.timeScale = 1f;
        if (MusicHandler.instance != null)
        {
            DestroyImmediate(MusicHandler.instance.gameObject);
        }
        foreach (ButtonWithSelect menu in mMenusWithSelected)
        {
            menu.mMenu.SetActive(false);
        }
        mMenusWithSelected[0].mMenu.SetActive(true);
        mMenusWithSelected[0].mButton.Select();
        mPreviousSelected = mMenusWithSelected[0].mButton.gameObject;

        Utils.GAME_STOPPED = false;
        mPreviousIndexMenu = 0;
        GameControler.type= GameControler.GameType.None;

        Worlds.createWorlds();
    }
    private void OnEnable()
    {
        mDefaultInputActions.UI.Enable();
    }

    private void OnDisable()
    {
        mDefaultInputActions.UI.Disable();
    }

    public void switchMenu(int index)
    {
        mMenusWithSelected[mPreviousIndexMenu].mMenu.SetActive(false);
        mMenusWithSelected[index].mMenu.SetActive(true);
        if (index == 0)
        {
            mMenusWithSelected[mPreviousIndexMenu].mBackButton.Select();
        }
        else
        {
            mMenusWithSelected[index].mButton.Select();
        }

        mAudioSubmitBack.Play();
        mPreviousIndexMenu = index;
    }

    public void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != mPreviousSelected)
        {
            mAudioNavigate.Play();
            mPreviousSelected = EventSystem.current.currentSelectedGameObject;
        }
        //mAudioMusic.volume = ES3.Load<float>("VolumeSlider", 0.5f);
        //mAudioNavigate.volume = ES3.Load<float>("VolumeSlider", 0.5f);
        //mAudioSubmitBack.volume = ES3.Load<float>("VolumeSlider", 0.5f);
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
