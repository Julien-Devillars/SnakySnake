using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Codice.Utils;

public class LevelPanel : MonoBehaviour
{
    public TextMeshProUGUI mWorldName;
    public GameObject mLevels;
    public Animator mTransitionAnimation;
    public GameObject mLock;
    public TextMeshProUGUI mTextDeathCounter;
    public TextMeshProUGUI mTextTimer;
    public GameObject mTimerGoal;
    public TextMeshProUGUI mTextTimerGoal;
    public static bool mForceDisplay = false;
    // Start is called before the first frame update
    void Start()
    {
        GameControler.currentWorld = 0;
        GameControler.currentLevel = 0;
        display();
    }

    private void Update()
    {
        if(mForceDisplay)
        {
            display();
            mForceDisplay = false;
        }
    }

    public bool isLock()
    {
        if (GameControler.currentWorld == 0) return false;
        if (ES3.Load<bool>($"PlayMode_World{GameControler.currentWorld}_Level0_status", false)) return false;
        int previous_world = GameControler.currentWorld - 1;
        int cpt = 0;
        for(int i = 0; i < mLevels.transform.childCount; ++i)
        {
            bool level_done = ES3.Load<bool>($"PlayMode_World{previous_world}_Level{i}_status", false);
            if(level_done)
            {
                cpt++;
            }
        }
        
        return cpt < 8;
    }

    public void display()
    {
        //Level current_level = Levels.levels[mDisplayIndex];
        //mLevelName.text = current_level.mLevelName;

        mWorldName.text = Worlds.worlds[GameControler.currentWorld].levels_name;

        bool is_lock = isLock();
        if(is_lock)
        {
            mLock.SetActive(true);
            mLevels.SetActive(false);
        }
        else
        {
            mLock.SetActive(false);
            mLevels.SetActive(true);
        }

        int cpt = 0;

        mTextDeathCounter.text = ES3.Load<int>($"PlayMode_World{GameControler.currentWorld}_death", 0).ToString();

        float timer = ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_timer", -1f);
        mTextTimer.text = Utils.getTimeFromFloat(timer);

        Levels current_world = Worlds.worlds[GameControler.currentWorld];
        mTextTimerGoal.text = current_world.getGoalTime(timer);
        mTimerGoal.GetComponent<Image>().material = current_world.getGoalMaterial(timer);

        foreach (Transform level in mLevels.transform)
        {
            cpt++;
            level.GetChild(0).GetComponent<TextMeshProUGUI>().text = cpt.ToString();
            int level_idx = cpt - 1;
            level.GetComponent<Button>().onClick.AddListener(delegate { Play(level_idx); });
            bool level_done = ES3.Load<bool>($"PlayMode_World{GameControler.currentWorld}_Level{level_idx}_status", false);
            level.GetComponent<Image>().material = level.GetComponent<Image>().material;
            if (level_done)
            {
                //level.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.blue;
                //level.GetComponent<Image>().color = Color.blue;
                level.GetComponent<Image>().material.SetFloat("_Glow", 4f);
                level.GetComponent<Image>().material.SetColor("_GlowColor", Color.yellow);
                level.GetComponent<ButtonHandler>().mStartColor = Color.yellow;
                level.GetComponent<ButtonHandler>().mStartIntensity = 4f;
            }
            else
            {
                //level.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
                //level.GetComponent<Image>().color = new Color(1f, 0.5f, 0f);
                //level.GetComponent<Image>().color = Color.magenta;
                level.GetComponent<Image>().material.SetFloat("_Glow", 8f);
                level.GetComponent<Image>().material.SetColor("_GlowColor", Color.magenta);
                level.GetComponent<ButtonHandler>().mStartColor = Color.magenta;
                level.GetComponent<ButtonHandler>().mStartIntensity = 8f;
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
        if(level == 0)
        {
            Timer.StartWorldTimer();
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
