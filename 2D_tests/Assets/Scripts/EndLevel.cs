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
    public bool mFinish;
    void Start()
    {
        mMenu.SetActive(false);
        //mNextLevelButton.interactable = true;
        Time.timeScale = 1f;
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
            Time.timeScale = 0f;    
            mFinish = true;
        }
        else
        {
            return;
        }
        if (GameControler.status == GameControler.GameStatus.Win)
        {
            //mNextLevelButton.interactable = true;
            //mTextLevel.text = $"World {GameControler.currentWorld} - Level {GameControler.currentLevel + 1}<br>CLEARED";
            Next();
        }
        if (GameControler.status == GameControler.GameStatus.Lose)
        {
            //mNextLevelButton.interactable = false;
            //mNextLevelButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Skip Level";
            // mTextLevel.text = $"World {GameControler.currentWorld} - Level {GameControler.currentLevel + 1}<br>FAILED";
            int death = ES3.Load<int>($"PlayMode_World{GameControler.currentWorld}_death", 0);
            ES3.Save<int>($"PlayMode_World{GameControler.currentWorld}_death", ++death);
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
                GameControler.SaveTime();
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
        Time.timeScale = 1f;
        mTransitionAnimation.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(0.15f);
        if(!mIsLoadingLevel)
        {
            mIsLoadingLevel = true;
            SceneManager.LoadSceneAsync(level_name);
        }
    }
}
