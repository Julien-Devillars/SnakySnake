using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelPanel : MonoBehaviour
{
    public TextMeshProUGUI mWorldName;
    public GameObject mLevels;
    public Animator mTransitionAnimation;
    // Start is called before the first frame update
    void Start()
    {
        GameControler.currentWorld = 0;
        GameControler.currentLevel = 0;
        display();
    }


    public void display()
    {
        //Level current_level = Levels.levels[mDisplayIndex];
        //mLevelName.text = current_level.mLevelName;

        mWorldName.text = Worlds.worlds[GameControler.currentWorld].levels_name;
        int cpt = 0;
        foreach(Transform level in mLevels.transform)
        {
            cpt++;
            level.GetChild(0).GetComponent<TextMeshProUGUI>().text = cpt.ToString();
            int level_idx = cpt - 1;
            level.GetComponent<Button>().onClick.AddListener(delegate { Play(level_idx); });
            bool level_done = ES3.Load<bool>($"PlayMode_World{GameControler.currentWorld}_Level{level_idx}_status", false);
            level.GetComponent<Image>().material = new Material(level.GetComponent<Image>().material);
            if (level_done)
            {
                level.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.blue;
                level.GetComponent<Image>().color = Color.blue;
                level.GetComponent<Image>().material.SetFloat("_Glow", 3f);
            }
            else
            {
                level.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
                //level.GetComponent<Image>().color = new Color(1f, 0.5f, 0f);
                level.GetComponent<Image>().color = Color.magenta;
                level.GetComponent<Image>().material.SetFloat("_Glow", 0f);
            }
        }

    }

    public void onLeft()
    {
        if(GameControler.currentWorld <= 0)
        {
            GameControler.currentWorld = Worlds.worlds.Count - 1;
        }
        else
        {
            GameControler.currentWorld--;
        }
        display();
    }
    public void onRight()
    {
        if (GameControler.currentWorld >= Worlds.worlds.Count - 1)
        {
            GameControler.currentWorld = 0;
        }
        else
        {
            GameControler.currentWorld++;
        }
        display();
    }

    public void Play(int level)
    {
        GameControler.currentLevel = level;
        GameControler.type = GameControler.GameType.Play;
        if(Worlds.worlds.Count == 0)
        {
            Worlds.createWorlds();
        }
        EnemiesGeneratorPlayMode.test_level = false;
        GameControler.status = GameControler.GameStatus.InProgress;
        StartCoroutine(LoadLevel());
    }

    private bool mIsLoadingLevel = false;
    IEnumerator LoadLevel()
    {
        mTransitionAnimation.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.15f);

        if (!mIsLoadingLevel)
        {
            mIsLoadingLevel = true;
            SceneManager.LoadSceneAsync("PlayLevel");
        }
    }

}
