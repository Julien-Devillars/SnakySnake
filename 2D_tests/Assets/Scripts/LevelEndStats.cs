using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelEndStats : MonoBehaviour
{
    public Image mChronometerBronze;
    public Image mChronometerSilver;
    public Image mChronometerGold;

    public GameObject mBest;
    public TextMeshProUGUI mChronometerText;

    public TextMeshProUGUI mChronometerGoldText;
    public TextMeshProUGUI mChronometerSilverText;
    public TextMeshProUGUI mChronometerBronzeText;

    public TextMeshProUGUI mDeathCounter;

    private float mPreviousBestTime;

    private void changeChronometerImage(Image chronometer)
    {
        chronometer.sprite = Resources.Load<Sprite>("Sprites/UI/Icons/Chronometer");
        chronometer.material = Resources.Load<Material>($"Materials/UI/{chronometer.gameObject.name}");
        chronometer.color = Color.white;
    }

    public void Awake()
    {
        mPreviousBestTime = Timer.GetBestLevelTime();
        mBest.SetActive(false);
    }
    public void Update()
    {
        /*
        float best_time = Timer.GetBestLevelTime();
        if (mPreviousBestTime < 0f || best_time < mPreviousBestTime)
        {
            mBest.SetActive(true);
        }
        else
        {
            mBest.SetActive(false);
        }*/
    }

    public void updateStatsInPanel()
    {
        Level current_level = Worlds.getCurrentLevel();

        float gold_timer = current_level.mGoldTime;
        float silver_timer = current_level.mSilverTime;
        float bronze_timer = current_level.mBronzeTime;

        mChronometerGoldText.text = Utils.getTimeFromFloat(gold_timer);
        mChronometerSilverText.text = Utils.getTimeFromFloat(silver_timer);
        mChronometerBronzeText.text = Utils.getTimeFromFloat(bronze_timer);

        float current_time = Timer.GetLevelTime();
        mChronometerText.text = Utils.getTimeFromFloat(current_time);

        if(current_time < gold_timer)
        {
            changeChronometerImage(mChronometerGold);
        }
        if (current_time < silver_timer)
        {
            changeChronometerImage(mChronometerSilver);
        }
        if (current_time < bronze_timer)
        {
            changeChronometerImage(mChronometerBronze);
        }

        mDeathCounter.text = Timer.deathCountInLevel.ToString();


        float best_time = Timer.GetBestLevelTime();
        if (mPreviousBestTime < 0f || best_time < mPreviousBestTime)
        {
            mBest.SetActive(true);
        }
        else
        {
            mBest.SetActive(false);
        }
    }
}
