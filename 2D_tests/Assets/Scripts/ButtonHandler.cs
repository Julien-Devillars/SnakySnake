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
            transform.localScale = new Vector3(1.05f, 1.05f, 1.05f); 
        }
        else
        {
            mMaterial.SetColor("_GlowColor", mStartColor);
            mMaterial.SetFloat("_Glow", mStartIntensity);
            GetComponent<Image>().color = Color.white;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (gameObject.name == "Level")
        {
            GameObject time_go = GameObject.Find("ChronometerText");
            GameObject death_go = GameObject.Find("DeathText");
            GameObject time_goal_gold_text_go = GameObject.Find("ChronometerGoalGoldText");
            GameObject time_goal_silver_text_go = GameObject.Find("ChronometerGoalSilverText");
            GameObject time_goal_bronze_text_go = GameObject.Find("ChronometerGoalBronzeText");
            GameObject chronometer_go = GameObject.Find("Chronometer");
            if (time_go == null) return;
            if (death_go == null) return;
            if (time_goal_gold_text_go == null) return; 
            if (time_goal_silver_text_go == null) return;
            if (time_goal_bronze_text_go == null) return;
            if (chronometer_go == null) return;
            if (transform.childCount != 1) return;

            GameObject button_text_go = transform.GetChild(0).gameObject;
            TextMeshProUGUI button_text_mesh = button_text_go.GetComponent<TextMeshProUGUI>();
            int current_level_idx = int.Parse(button_text_mesh.text) - 1;

            TextMeshProUGUI time_text_mesh = time_go.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI death_text_mesh = death_go.GetComponent<TextMeshProUGUI>();

            TextMeshProUGUI time_goal_gold_text_mesh = time_goal_gold_text_go.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI time_goal_silver_text_mesh = time_goal_silver_text_go.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI time_goal_bronze_text_mesh = time_goal_bronze_text_go.GetComponent<TextMeshProUGUI>();
            Material mat = null;
            if (flag)
            {
                death_text_mesh.text = ES3.Load<int>($"PlayMode_World{GameControler.currentWorld}_Level{current_level_idx}_death", 0).ToString();
                float timer = ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_Level{current_level_idx}_timer", -1f);
                time_text_mesh.text = $"{Utils.getTimeFromFloat(timer)}";
                Level current_level = Worlds.getLevel(GameControler.currentWorld, current_level_idx);
                time_goal_gold_text_mesh.text = Utils.getTimeFromFloat(current_level.mGoldTime);
                time_goal_silver_text_mesh.text = Utils.getTimeFromFloat(current_level.mSilverTime);
                time_goal_bronze_text_mesh.text = Utils.getTimeFromFloat(current_level.mBronzeTime);
                mat = current_level.getGoalMaterial(timer);
            }
            else
            {
                death_text_mesh.text = ES3.Load<int>($"PlayMode_World{GameControler.currentWorld}_death", 0).ToString();
                float timer = ES3.Load<float>($"PlayMode_World{GameControler.currentWorld}_timer", -1f);
                time_text_mesh.text = $"{Utils.getTimeFromFloat(timer)}";
                Levels current_world = Worlds.getWorld(GameControler.currentWorld);
                time_goal_gold_text_mesh.text = Utils.getTimeFromFloat(current_world.mGoldTime);
                time_goal_silver_text_mesh.text = Utils.getTimeFromFloat(current_world.mSilverTime);
                time_goal_bronze_text_mesh.text = Utils.getTimeFromFloat(current_world.mBronzeTime);
                mat = current_world.getGoalMaterial(timer);
            }
            if (mat == null)
            {
                //chronometer_go.GetComponent<Image>().enabled = false;
                chronometer_go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/Icons/ChronometerGray");
                chronometer_go.GetComponent<Image>().color = new Color(0.85f, 0.6f, 0.3f);
            }
            else
            {
                //chronometer_go.GetComponent<Image>().enabled = true;
                chronometer_go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/Icons/Chronometer");
                chronometer_go.GetComponent<Image>().color = Color.white;
            }
            chronometer_go.GetComponent<Image>().material = mat;
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight(false);
    }

    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

}
