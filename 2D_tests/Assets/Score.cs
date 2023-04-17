using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
        mGoalScore = 60;

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mTotalArea = 2 * width * 2 *height;

    }

    void Update()
    {
        Text text_ui = gameObject.GetComponent<Text>();
        int area_percentage = (int)(mCurrentScore * 100f / mTotalArea);
        text_ui.text = "Score : " + area_percentage.ToString() + "/" + mGoalScore.ToString() + "%";
    }

}
