using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public float mPreviousScore;
    public float mCurrentScore;
    public float mStep = 250f;
    public int mGoalScore;
    public GameObject mScoreGoalGameObject;
    public static Score Instance { get; private set; }

    private float mTotalArea;
    public int mMultiplier = 1000;

    private float mStartTime;
    public float mDuration = 0.3f;
    public bool mToUpdate = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        mCurrentScore = 0f;
        mPreviousScore = 0f;
        mGoalScore = 70 *  mMultiplier;

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mTotalArea = 2 * width * 2 *height;

        TextMeshProUGUI goal_text = mScoreGoalGameObject.GetComponent<TextMeshProUGUI>();
        goal_text.text = mGoalScore.ToString();
    }

    void Update()
    {
        float area_percentage = mCurrentScore / mTotalArea;
        area_percentage = area_percentage * 100 * mMultiplier;
        int area_percentage_int = (int)area_percentage;
        area_percentage_int = (int) updateScore(area_percentage_int);
        checkWinCondition(area_percentage_int);
    }

    void checkWinCondition(float area_percentage)
    {
        if (Utils.HAS_WIN && area_percentage >= mGoalScore)
        {
            GameControler.status = GameControler.GameStatus.Win;
            Scene scene = SceneManager.GetActiveScene();
            string scene_name = scene.name;
            string[] scene_split = scene_name.Split('_');

            if(scene_split.Length != 2)
            {
                return;
            }

            int scene_number = int.Parse(scene_split[1]);
            scene_number += 1;

            SceneManager.LoadScene(scene_split[0] + "_" + scene_number.ToString());
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
