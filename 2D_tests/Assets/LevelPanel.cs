using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class LevelInformation
{
    [SerializeField] public string mLevelName;
    [SerializeField] public int mGoalScore;
    int mBestReach = 0;
    public List<bool> mStars = new List<bool>() { false, false, false };
    LevelInformation(string name, int goal)
    {
        mLevelName = name;
        mGoalScore = goal;
    }
}


public class LevelPanel : MonoBehaviour
{
    public TextMeshProUGUI mLevelName;
    public TextMeshProUGUI mGoalScore;
    public TextMeshProUGUI mBestReach;
    public Slider mSlider;
    public List<LevelInformation> mLevels;
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
        mLevelName.text = mLevels[mDisplayIndex].mLevelName;
        mGoalScore.text = (mLevels[mDisplayIndex].mGoalScore * Score.mMultiplier).ToString();
        int best = ES3.Load<int>("Play_HighScore_" + mDisplayIndex, 0);
        mBestReach.text = best.ToString() + "%";
        mSlider.value = best;

        for(int i = 0; i < mStars.transform.childCount; ++i)
        {
            Transform star_child = mStars.transform.GetChild(i);
            bool star = mLevels[mDisplayIndex].mStars[i];
            if (star)
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
        if(mDisplayIndex == 0)
        {
            mDisplayIndex = mLevels.Count - 1;
        }
        else
        {
            mDisplayIndex--;
        }
        display();
    }
    public void onRight()
    {
        if (mDisplayIndex == mLevels.Count - 1)
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
