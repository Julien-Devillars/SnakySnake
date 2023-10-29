using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class EndLevel : MonoBehaviour
{
    public TextMeshProUGUI mTextLevel;
    public Button mNextLevelButton;
    public GameObject mMenu;
    public Animator mTransitionAnimation;
    public Animator mCharacterDeathAnimation;
    public bool mFinish;
    public AudioSource mDeathAudio;
    public LevelEndStats mLevelEndStats;


    private DefaultInputActions mDefaultInputActions;

    private void Awake()
    {
        mDefaultInputActions = new DefaultInputActions();

        mDefaultInputActions.UI.Navigate.performed += ctx =>
        {
            if (GameControler.status != GameControler.GameStatus.Win) return;
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            //Debug.Log(selected.name);
            if (selected == null || selected.name == "Text")// || (selected.GetComponent<ButtonHandler>() == null && selected.GetComponent<SliderHandler>() == null))
            {
                mNextLevelButton.Select();
            }
        };
    }

    void Start()
    {
        mMenu.SetActive(false);
        //mNextLevelButton.interactable = true;
        //Time.timeScale = 1f;
        GameControler.status = GameControler.GameStatus.InProgress;
        mFinish = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (GameControler.status == GameControler.GameStatus.Win)
        {
            mMenu.SetActive(true);
        }
        else if(GameControler.status == GameControler.GameStatus.Lose)
        {

        }
        else
        {
            //mMenu.SetActive(false);
            return;
        }
        if(!mFinish)
        {
            //Time.timeScale = 0f;    
            mFinish = true;
        }
        else
        {
            return;
        }
        if (GameControler.status == GameControler.GameStatus.Win)
        {
            Timer.SaveLevelTime();
            Timer.StartLevelPauseTimer();
            string world_tr = Translation.GetTranslation("World", ES3.Load<SystemLanguage>("Language", Application.systemLanguage));
            string level_tr = Translation.GetTranslation("Level", ES3.Load<SystemLanguage>("Language", Application.systemLanguage));
            mTextLevel.text = $"{world_tr} {GameControler.currentWorld + 1} - {level_tr} {GameControler.currentLevel + 1}";
            mLevelEndStats.updateStatsInPanel();
            Timer.resetDeathLevelCounter();
            mNextLevelButton.Select();
            SteamAchievement.checkWorlFinish();
            //Next();
        }
        if (GameControler.status == GameControler.GameStatus.Lose)
        {
            Timer.addDeath();
            mDeathAudio.Play();
            mCharacterDeathAnimation.SetTrigger("Death");
            StartCoroutine(Replay(0.5f));
        }
    }
    private void OnEnable()
    {
        mDefaultInputActions.UI.Enable();
    }

    private void OnDisable()
    {
        mDefaultInputActions.UI.Disable();
    }
    public void ReplayButton()
    {
        Timer.EndLevelPauseTimer();
        StartCoroutine(Replay(0f));
    }
    public IEnumerator Replay(float time_to_wait)
    {
        yield return new WaitForSecondsRealtime(time_to_wait);
        StartCoroutine(LoadLevel("PlayLevel"));
    }

    public int nextLevelIsLock()
    {
        int cpt = 0;
        for (int i = 0; i < Worlds.worlds[GameControler.currentWorld].levels.Count - 1; ++i)
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
        Timer.EndLevelPauseTimer();
        if (GameControler.currentLevel < Worlds.worlds[GameControler.currentWorld].levels.Count - 1)
        {
            GameControler.currentLevel++;
            Timer.resetDeathLevelCounter();
            StartCoroutine(LoadLevel("PlayLevel"));
        }
        else
        {
            if(GameControler.currentWorld < Worlds.worlds.Count - 1)
            {
                Timer.SaveWorldTime();
                Timer.resetDeathLevelCounter();

                int nb_level_done = nextLevelIsLock();
                if (nb_level_done < Worlds.mNBLevelToNext)
                {
                    GameControler.currentWorld++;
                    StartCoroutine(LoadLevel("MainMenu"));
                    return;
                }

                GameControler.currentWorld++;
                GameControler.currentLevel = 0;
                StartCoroutine(LoadLevel("PlayLevel"));
            }
            else
            {
                Menu();
            }
        }
    }
    public void Menu()
    {
        StartCoroutine(LoadLevel("MainMenu"));
    }
    private bool mIsLoadingLevel = false;
    IEnumerator LoadLevel(string level_name)
    {
        GameControler.status = GameControler.GameStatus.Waiting;
        //Time.timeScale = 1f;
        mTransitionAnimation.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(0.15f);
        if(!mIsLoadingLevel)
        {
            mIsLoadingLevel = true;
            SceneManager.LoadSceneAsync(level_name);
        }
    }
}
