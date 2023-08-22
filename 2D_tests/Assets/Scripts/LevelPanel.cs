using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelPanel : MonoBehaviour
{
    public TextMeshProUGUI mLevelName;
    public TextMeshProUGUI mGoalScore;
    public TextMeshProUGUI mBestReach;
    public Slider mSlider;
    public GameObject mStars;
    int mDisplayIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        mDisplayIndex = 0;
        display();
    }


    public void display()
    {
        Level current_level = Levels.levels[mDisplayIndex];
        mLevelName.text = current_level.mLevelName;
        mGoalScore.text = (current_level.mGoalScore * Score.mMultiplier).ToString();
        int best = ES3.Load<int>($"Play_Level_{mDisplayIndex}_HighScore", 0);
        mBestReach.text = best.ToString() + "%";
        mSlider.value = best;

        for(int i = 0; i < mStars.transform.childCount; ++i)
        {
            Transform star_child = mStars.transform.GetChild(i);
            bool has_star = ES3.Load<bool>($"Play_Level_{mDisplayIndex}_Star_{i}", false);
            if (has_star)
            {
                star_child.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/StarOK");
            }
            else
            {
                star_child.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/StarNOK");
            }
        }
    }

    public void onLeft()
    {
        if(mDisplayIndex <= 0)
        {
            mDisplayIndex = Levels.levels.Count - 1;
        }
        else
        {
            mDisplayIndex--;
        }
        display();
    }
    public void onRight()
    {
        if (mDisplayIndex >= Levels.levels.Count - 1)
        {
            mDisplayIndex = 0;
        }
        else
        {
            mDisplayIndex++;
        }
        display();
    }
}
