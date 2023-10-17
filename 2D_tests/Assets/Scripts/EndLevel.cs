using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndLevel : MonoBehaviour
{
    public TextMeshProUGUI mTextLevel;
    public Button mNextLevelButton;
    public GameObject mMenu;
    public Animator mTransitionAnimation;
    public Animator mCharacterDeathAnimation;
    public bool mFinish;
    public AudioSource mDeathAudio;
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
        if (GameControler.status == GameControler.GameStatus.Win || GameControler.status == GameControler.GameStatus.Lose)
        {
            //mMenu.SetActive(true);
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
            SteamAchievement.checkWorlFinish();
            Timer.SaveLevelTime();
            Next();
        }
        if (GameControler.status == GameControler.GameStatus.Lose)
        {
            Timer.addDeath();
            mDeathAudio.Play();
            mCharacterDeathAnimation.SetTrigger("Death");
            StartCoroutine(Replay());
        }
    }
    public IEnumerator Replay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(LoadLevel("PlayLevel"));
    }

    public void Next()
    {
        if (GameControler.currentLevel < Worlds.worlds[GameControler.currentWorld].levels.Count - 1)
        {
            GameControler.currentLevel++;
            StartCoroutine(LoadLevel("PlayLevel"));
        }
        else
        {
            if(GameControler.currentWorld < Worlds.worlds.Count - 1)
            {
                Timer.SaveWorldTime();
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
