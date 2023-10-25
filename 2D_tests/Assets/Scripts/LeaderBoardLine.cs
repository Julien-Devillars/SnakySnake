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

    // Update is called once per frame
    void Update()
    {
        mPositionText.text = mPosition.ToString();
        mNameText.text = mName;
        mTimeText.text = Utils.getTimeFromFloat(mTime);
    }

    public void display(bool flag)
    {
        GetComponent<CanvasGroup>().alpha = flag ? 1f : 0f;
    }

    public void overlay(bool flag)
    {
        GetComponent<Image>().enabled = flag;
    }
}
