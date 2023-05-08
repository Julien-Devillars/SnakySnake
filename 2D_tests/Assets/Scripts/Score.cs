using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update

    public float mCurrentScore;
    public int mGoalScore;
    public static Score Instance { get; private set; }

    private float mTotalArea;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        mCurrentScore = 0;
        mGoalScore = 70;

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mTotalArea = 2 * width * 2 *height;

    }

    void Update()
    {
        Text text_ui = gameObject.GetComponent<Text>();
        int area_percentage = (int)(mCurrentScore * 100f / mTotalArea);
        checkWinCondition(area_percentage);
        text_ui.text = "Score : " + area_percentage.ToString() + "/" + mGoalScore.ToString() + "%";
    }

    void checkWinCondition(float area_percentage)
    {
        if (area_percentage >= mGoalScore)
        {
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

}
