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

    public void onClickSelectMusicSlider()
    {
        mSliderMusic.Select();
    }
    public void onClickSelectLuminositySlider()
    {
        mSliderLuminosity.Select();
    }
}
