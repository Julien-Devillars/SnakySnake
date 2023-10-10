using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHandler : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Material mMaterial;
    [HideInInspector] public Color mStartColor;
    [HideInInspector] public float mStartIntensity;
    public Color mSelectedColor;
    public float mSelectedIntensity = 15f;

    private void init()
    {
        mMaterial = new Material(GetComponent<Image>().material);
        GetComponent<Image>().material = mMaterial;
        mStartColor = mMaterial.GetColor("_GlowColor");
        mStartIntensity = mMaterial.GetFloat("_Glow");
    }

    private void highlight(bool flag)
    {
        if (mMaterial == null) init();
        if (flag)
        {
            mMaterial.SetColor("_GlowColor", mSelectedColor);
            mMaterial.SetFloat("_Glow", mSelectedIntensity);
            GetComponent<Image>().color = Color.blue;
        }
        else
        {
            mMaterial.SetColor("_GlowColor", mStartColor);
            mMaterial.SetFloat("_Glow", mStartIntensity);
            GetComponent<Image>().color = Color.white;
        }
    }
    private void highlight()
    {
        if (mMaterial == null) init();
        mMaterial.SetColor("_GlowColor", mSelectedColor);
        mMaterial.SetFloat("_Glow", mSelectedIntensity);
    }

    public void OnSelect(BaseEventData eventData)
    {
        highlight(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        highlight(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight(true);
        EventSystem.current.SetSelectedGameObject(eventData.pointerEnter);
        if(gameObject.name == "Level")
        {
            GameObject time_go = GameObject.Find("ChronometerText");
            GameObject death_go = GameObject.Find("DeathText");
            if (time_go == null || death_go == null) return;
            if (transform.childCount != 1) return;

            GameObject button_text_go = transform.GetChild(0).gameObject;
            TextMeshProUGUI button_text_mesh = button_text_go.GetComponent<TextMeshProUGUI>();
            int current_level = int.Parse(button_text_mesh.text) - 1;

            TextMeshProUGUI time_text_mesh = time_go.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI death_text_mesh = death_go.GetComponent<TextMeshProUGUI>();

            death_text_mesh.text = ES3.Load<int>($"PlayMode_World{GameControler.currentWorld}_Level{current_level}_death", 0).ToString();
            float timer = ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_Level{current_level}_timer", 0f);
            time_text_mesh.text = Utils.getTimeFromFloat(timer);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight(false);
        if (gameObject.name == "Level")
        {
            GameObject time_go = GameObject.Find("ChronometerText");
            GameObject death_go = GameObject.Find("DeathText");
            if (time_go == null || death_go == null) return;

            TextMeshProUGUI time_text_mesh = time_go.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI death_text_mesh = death_go.GetComponent<TextMeshProUGUI>();

            death_text_mesh.text = ES3.Load<int>($"PlayMode_World{GameControler.currentWorld}_death", 0).ToString();
            float timer = ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_timer", 0f);
            time_text_mesh.text = Utils.getTimeFromFloat(timer);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

}
