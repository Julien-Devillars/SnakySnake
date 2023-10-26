using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortingIcon : MonoBehaviour
{
    public LeaderBoard.Sorting mSortingType;
    Image mImage;
    Image mParentImage;
    Button mParentButton;
    Color mStartColor;
    Color mStartParentColor;
    void Start()
    {
        mImage = gameObject.GetComponent<Image>();
        mParentImage = gameObject.transform.parent.gameObject.GetComponent<Image>();
        mParentButton = gameObject.transform.parent.gameObject.GetComponent<Button>();

        mImage.material = new Material(mImage.material);
        mParentImage.material = new Material(mParentImage.GetComponent<Image>().material);
        mStartColor = mImage.color;
        mStartParentColor = mParentButton.colors.normalColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(mSortingType == LeaderBoard.mSorting)
        {
            mImage.color = Color.cyan;
            ColorBlock cb = mParentButton.colors;
            cb.normalColor = new Color(0f, 0f, 1f, 0.75f);
            mParentButton.colors = cb;
            //mParentImage.color = new Color(0f, 0f, 1f, 0.75f);
        }
        else
        {
            mImage.color = mStartColor;
            ColorBlock cb = mParentButton.colors;
            cb.normalColor = mStartParentColor;
            mParentButton.colors = cb;
            //mParentImage.color = mStartParentColor;
        }
    }
}
