using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public float mPreviousScore;
    public float mCurrentScore;
    public int mCurrentPercentScore;
    public float mStep = 250f;
    public int mGoalPercent;
    public int mGoalScore;
    public GameObject mScoreGoalGameObject;
    public static Score Instance { get; private set; }

    private float mTotalArea;
    static public int mMultiplier = 1000;

    private float mStartTime;
    public float mDuration = 0.3f;
    public bool mToUpdate = false;

    private void Awake()
    {
        Instance = this;
        //GameObject level_controller = GameObject.Find(Utils.LEVEL_STR);
        //LevelSettings level_settings = level_controller.GetComponent<LevelSettings>();
        //mGoalPercent = level_settings.score;
        
    }

    void Start()
    {
        mCurrentScore = 0f;
        mPreviousScore = 0f;
        if (GameControler.type == GameControler.GameType.Infinity)
        {
            mGoalPercent = InfinityControler.mScore;
        }
        else
        {
            //mGoalPercent = Levels.levels[GameControler.currentLevel].mGoalScore;
        }
        mGoalScore = mGoalPercent *  mMultiplier;

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        float offset = Utils.EPSILON();

        mTotalArea = 2 * (width - offset) * 2 * (height - offset);

        TextMeshProUGUI goal_text = mScoreGoalGameObject.GetComponent<TextMeshProUGUI>();
        goal_text.text = mGoalScore.ToString();
    }

    void Update()
    {
        float area_percentage = mCurrentScore / mTotalArea;
        area_percentage = area_percentage * 100 * mMultiplier;
        int area_percentage_int = (int)area_percentage;
        mCurrentPercentScore = area_percentage_int / mMultiplier;
        area_percentage_int = (int) updateScore(area_percentage_int);
        checkWinCondition(area_percentage_int);
    }

    void checkWinCondition(float area_percentage)
    {
        if (Utils.HAS_WIN && area_percentage >= mGoalScore)
        {
            GameControler.status = GameControler.GameStatus.Win;

            if (GameControler.type == GameControler.GameType.Infinity)
            {
                InfinityControler.mCurrentLevel++;
                SceneManager.LoadScene("InfinityLevel");
            }

            if (GameControler.type == GameControler.GameType.Play)
            {
                GameObject stars = GameObject.Find(Utils.STARS_STR);
                int cpt = 0;
                foreach(Transform star in stars.transform)
                {
                    if(!star.gameObject.activeSelf)
                    {
                        cpt++;
                    }
                }
                for(int i = 0; i < cpt; ++i)
                {
                    ES3.Save<bool>($"Play_Level_{GameControler.currentLevel}_Star_{i}", true);
                }
                ES3.Save<int>($"Play_Level_{GameControler.currentLevel}_HighScore", 100);

                //if (GameControler.currentLevel < Levels.levels.Count - 1)
                //{
                //    GameControler.currentLevel++;
                //    SceneManager.LoadScene("PlayLevel");
                //}
                //else
                //{
                //    SceneManager.LoadScene("MainMenu");
                //}
            }
        }
    }
    float updateScore(int area_percentage_int)
    {
        TextMeshProUGUI text_ui = gameObject.GetComponent<TextMeshProUGUI>();
        if(mPreviousScore != area_percentage_int && mToUpdate)
        {
            mStartTime = Time.time;
            mToUpdate = false;
        }
        // Slow start & slow end
        float animT = (Time.time - mStartTime) / mDuration;
        float a = Mathf.Round(animT);
        animT = 4 * Mathf.Pow(animT, 3) * (1 - a) + (1 - 4 * Mathf.Pow(1 - animT, 3)) * a; 
        animT = Mathf.Clamp01(animT);

        float animScore = (int)Mathf.Lerp(mPreviousScore, area_percentage_int, animT);

        text_ui.text = animScore.ToString();

        if (animScore == area_percentage_int)
        {
            mPreviousScore = area_percentage_int;
            mToUpdate = true;
        }
        return animScore;
    }

}
