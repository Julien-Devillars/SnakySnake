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

    void Start()
    {
        mMenu.SetActive(false);
        mNextLevelButton.interactable = false;
        Time.timeScale = 1f;
        GameControler.status = GameControler.GameStatus.InProgress;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameControler.status == GameControler.GameStatus.Win || GameControler.status == GameControler.GameStatus.Lose)
        {
            mMenu.SetActive(true);
        }
        else
        {
            mMenu.SetActive(false);
            return;
        }

        Time.timeScale = 0f;
        if (GameControler.status == GameControler.GameStatus.Win)
        {
            mNextLevelButton.interactable = true;
            mTextLevel.text = "LEVEL CLEARED";
        }
        if (GameControler.status == GameControler.GameStatus.Lose)
        {
            mNextLevelButton.interactable = false;
            mTextLevel.text = "LEVEL FAILED";
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene("PlayLevel");
    }

    public void Next()
    {
        if (GameControler.currentLevel < Worlds.worlds[GameControler.currentWorld].levels.Count - 1)
        {
            GameControler.currentLevel++;
            Time.timeScale = 1f;
            SceneManager.LoadScene("PlayLevel");
        }
        else
        {
            if(GameControler.currentWorld < Worlds.worlds.Count - 1)
            {
                GameControler.currentWorld++;
                GameControler.currentLevel = 0;
                Time.timeScale = 1f;
                SceneManager.LoadScene("PlayLevel");
            }
            else
            {
                Menu();
            }
        }
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
