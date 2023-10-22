using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class OptionSliderGamepad : MonoBehaviour
{
    private DefaultInputActions mDefaultInputActions;
    public Button mButtonMusic;
    public Slider mSliderMusic;
    public Button mButtonLuminosity;
    public Slider mSliderLuminosity;
    public GameObject mClearSaveUI;
    public Button mClearSaveNoButton;
    public Button mClearSaveButton;
    private void Awake()
    {
        mDefaultInputActions = new DefaultInputActions();

        mDefaultInputActions.UI.Submit.performed += ctx =>
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            if (selected == mButtonMusic.gameObject)
            {
                mSliderMusic.Select();
            }
            else if (selected == mButtonLuminosity.gameObject)
            {
                mSliderLuminosity.Select();
            }
            else if(selected == mSliderMusic.gameObject)
            {
                mButtonMusic.Select();
            }
            else if (selected == mSliderLuminosity.gameObject)
            {
                mButtonLuminosity.Select();
            }
        };
        mDefaultInputActions.UI.Navigate.performed += ctx =>
        {
            Vector2 navigation = mDefaultInputActions.UI.Navigate.ReadValue<Vector2>();
            if(Mathf.Abs(navigation.x) == 1)
            {
                GameObject selected = EventSystem.current.currentSelectedGameObject;
                if (selected == mButtonMusic.gameObject)
                {
                    mSliderMusic.Select();
                }
                else if (selected == mButtonLuminosity.gameObject)
                {
                    mSliderLuminosity.Select();
                }
            }
        };
    }

    private void OnEnable()
    {
        mDefaultInputActions.UI.Enable();
    }

    private void OnDisable()
    {
        mDefaultInputActions.UI.Disable();
    }
    
    bool has_click = false;
    public void clearSave()
    {

        LevelPanel.mForceDisplay = true;
        GameControler.currentLevel = 0;
        GameControler.currentWorld = 0;
        if(!has_click)
        {
            float music = ES3.Load<float>("VolumeSlider", 0.5f);
            float luminosity = ES3.Load<float>("LuminositySlider", 0.5f);
            ES3.DeleteFile();
            ES3.Save<float>("VolumeSlider", music);
            ES3.Save<float>("LuminositySlider", luminosity);

        }
        else
        {
            for (int i = 0; i < Worlds.worlds.Count; ++i)
            {
                for (int j = 0; j < Worlds.worlds[i].levels.Count; ++j)
                {
                    ES3.Save<bool>($"PlayMode_World{i}_Level{j}_status", has_click);
                    ES3.Save<int>($"PlayMode_World{i}_Level{j}_death", 0);
                    ES3.Save<float>($"PlayMode_World{i}_Level{j}_timer", -1f);
                }
                ES3.Save<int>($"PlayMode_World{i}_death", 0);
                ES3.Save<float>($"PlayMode_World{i}_timer", -1f);

            }
        }
        has_click = !has_click;
    }
    public void displayClearSaveUI(bool flag)
    {
        mClearSaveUI.SetActive(flag);
        if(flag)
        {
            mClearSaveNoButton.Select();
        }
        else
        {
            mClearSaveButton.Select();
        }
    }

}
