using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update

    public int mCurrentScore;
    public int mGoalScore;
    public static Score Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        mCurrentScore = 0;
        mGoalScore = 1000;
    }

    void Update()
    {
        Text text_ui = gameObject.GetComponent<Text>();
        text_ui.text = "Score : " + mCurrentScore.ToString() + "/" + mGoalScore.ToString();
    }

}
