using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class LevelPanel : MonoBehaviour
{
    public TextMeshProUGUI mWorldName;
    public GameObject mLevels;
    public Animator mTransitionAnimation;
    public GameObject mLock;
    public TextMeshProUGUI mLockNumber;
    public TextMeshProUGUI mTextDeathCounter;
    public TextMeshProUGUI mTextTimer;
    public GameObject mChronometerBronze;
    public GameObject mChronometerSilver;
    public GameObject mChronometerGoal;
    public TextMeshProUGUI mTextTimerGoalGold;
    public TextMeshProUGUI mTextTimerGoalSilver;
    public TextMeshProUGUI mTextTimerGoalBronze;
    public static bool mForceDisplay = false;
    // Start is called before the first frame update

    private Navigation mDefaultLeftNavigation;
    private Navigation mDefaultRightNavigation;
    private Navigation mDefaultBackNavigation;

    private PlayerControl mPlayerControlInput;
    public GameObject mRightSelectedButton;
    public GameObject mLeftSelectedButton;

    private void Awake()
    {
        mPlayerControlInput = new PlayerControl();
        mPlayerControlInput.PlayerController.LeftPanel.performed += ctx =>
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            onLeft();
            playSound("MenuUINavigate");
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(selected.activeInHierarchy ? selected : mLeftSelectedButton);
        };
        mPlayerControlInput.PlayerController.RightPanel.performed += ctx =>
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            onRight();
            playSound("MenuUINavigate");
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(selected.activeInHierarchy ? selected : mRightSelectedButton);
            //EventSystem.current.SetSelectedGameObject(mDefaultSelectedButton);
        };
    }
    private void playSound(string go_str)
    {
        GameObject sound = GameObject.Find(go_str);
        if (!sound) return;
        sound.GetComponent<AudioSource>().Play();
    }
    private void OnEnable()
    {
        mPlayerControlInput.PlayerController.Enable();
    }

    private void OnDisable()
    {
        mPlayerControlInput.PlayerController.Disable();
    }
    void Start()
    {
        //GameControler.currentWorld = 0;
        //GameControler.currentLevel = 0;

        GameObject left = GameObject.Find("LeftLevel");
        GameObject right = GameObject.Find("RightLevel");
        GameObject back = GameObject.Find("BackLevel");

        if (left && right && back)
        {
            Button left_button = left.GetComponent<Button>();
            Button right_button = right.GetComponent<Button>();
            Button back_button = back.GetComponent<Button>();

            mDefaultLeftNavigation = left_button.navigation;
            mDefaultRightNavigation = right_button.navigation;
            mDefaultBackNavigation = back_button.navigation;
        }

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

    public int isLock()
    {
        if (GameControler.currentWorld == 0) return 12;
        if (ES3.Load<bool>($"PlayMode_World{GameControler.currentWorld}_Level0_status", false)) return 12;
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
        
        return cpt ;
    }


    public void changeNavigationOnLock(bool flag)
    {
        GameObject left = GameObject.Find("LeftLevel");
        GameObject right = GameObject.Find("RightLevel");
        GameObject back = GameObject.Find("BackLevel");

        if (!left) return;
        if (!right) return;
        if (!back) return;

        Button left_button = left.GetComponent<Button>();
        Button right_button = right.GetComponent<Button>();
        Button back_button = back.GetComponent<Button>();

        if (flag)
        {
            Navigation new_left_navigation = new Navigation();
            Navigation new_right_navigation = new Navigation();
            Navigation new_back_navigation = new Navigation();

            new_left_navigation.mode = Navigation.Mode.Explicit;
            new_right_navigation.mode = Navigation.Mode.Explicit;
            new_back_navigation.mode = Navigation.Mode.Explicit;

            new_left_navigation = left_button.navigation;
            new_right_navigation = right_button.navigation;
            new_back_navigation = back_button.navigation;

            new_left_navigation.selectOnRight = right_button;
            new_right_navigation.selectOnLeft = left_button;
            new_back_navigation.selectOnUp = left_button;

            right_button.navigation = new_right_navigation;
            left_button.navigation = new_left_navigation;
            back_button.navigation = new_back_navigation;
        }
        else
        {
            left_button.navigation = mDefaultLeftNavigation;
            right_button.navigation = mDefaultRightNavigation;
            back_button.navigation = mDefaultBackNavigation;
        }
    }

    public void display()
    {
        //Level current_level = Levels.levels[mDisplayIndex];
        //mLevelName.text = current_level.mLevelName;
        string world_name = Worlds.worlds[GameControler.currentWorld].levels_name;
        world_name = world_name.Replace("World", Translation.GetTranslation("World", ES3.Load<SystemLanguage>("Language", Application.systemLanguage)));
        mWorldName.text = world_name;


        int nb_level_done = isLock();
        bool is_lock = Utils.isLock(nb_level_done);
        if (is_lock)
        {
            mLock.SetActive(true);
            if(GameControler.isDemo)
            {
                mLockNumber.text = Translation.GetTranslation("Thanks for testing the game. Unlock other levels by buying the game.", Translation.getLanguage());
            }
            else
            {
                mLockNumber.text = $"{nb_level_done} / {Worlds.mNBLevelToNext}";
            }
            mLevels.SetActive(false);
            changeNavigationOnLock(true);
        }
        else
        {
            mLock.SetActive(false);
            mLevels.SetActive(true);
            changeNavigationOnLock(false);
        }

        int cpt = 0;

        mTextDeathCounter.text = ES3.Load<int>($"PlayMode_World{GameControler.currentWorld}_death", 0).ToString();

        float timer = ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_timer", -1f);
        mTextTimer.text = $"{Utils.getTimeFromFloat(timer)}";

        Levels current_world = Worlds.worlds[GameControler.currentWorld];
        mTextTimerGoalGold.text = Utils.getTimeFromFloat(current_world.mGoldTime);
        mTextTimerGoalSilver.text = Utils.getTimeFromFloat(current_world.mSilverTime);
        mTextTimerGoalBronze.text = Utils.getTimeFromFloat(current_world.mBronzeTime);
        //Material mat = current_world.getGoalMaterial(timer);
        Utils.updateChronometersWithTime(timer, 
            current_world.mGoldTime, 
            current_world.mSilverTime, 
            current_world.mBronzeTime, 
            mChronometerGoal, 
            mChronometerSilver, 
            mChronometerBronze);
        //f (mat == null)
        //
        //   //chronometer_go.GetComponent<Image>().enabled = false;
        //   mChronometer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/Icons/ChronometerGray");
        //   mChronometer.GetComponent<Image>().color = new Color(0.85f, 0.6f, 0.3f);
        //
        //lse
        //
        //   //chronometer_go.GetComponent<Image>().enabled = true;
        //   mChronometer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/Icons/Chronometer");
        //   mChronometer.GetComponent<Image>().color = Color.white;
        //
        //Chronometer.GetComponent<Image>().material = mat;
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

        LeaderBoard.updateLeaderBoardWorldLevel(GameControler.currentWorld, -1);

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
