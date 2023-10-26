using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LeaderBoardLine : MonoBehaviour
{
    public TextMeshProUGUI mPositionText;
    public int mPosition = 0;
    public TextMeshProUGUI mNameText;
    public string mName = "";
    public TextMeshProUGUI mTimeText;
    public float mTime;
    private Color mStartColor;
    public Color mUserColor;
    private void Start()
    {
        mStartColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name.Contains("Title")) return;

        mPositionText.text = mPosition  >= 0 ? mPosition.ToString() : "";
        mNameText.text = mName;
        mTimeText.text = Utils.getTimeFromFloat(mTime);
    }

    public void display(bool flag)
    {
        GetComponent<CanvasGroup>().alpha = flag ? 1f : 0f;
    }

    public void overlay(bool flag)
    {
        GetComponent<Image>().color = flag ? mUserColor : mStartColor;
        mPositionText.color = flag ? Color.blue : Color.white;
        mNameText.color = flag ? Color.blue : Color.white;
        mTimeText.color = flag ? Color.blue : Color.white;
    }
}
