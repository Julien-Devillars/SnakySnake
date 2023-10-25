using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortingIcon : MonoBehaviour
{
    public LeaderBoard.Sorting mSortingType;
    Image mImage;
    Image mParentImage;
    Color mStartColor;
    Color mStartParentColor;
    void Start()
    {
        mImage = gameObject.GetComponent<Image>();
        mParentImage = gameObject.transform.parent.gameObject.GetComponent<Image>();

        mImage.material = new Material(mImage.material);
        mParentImage.material = new Material(mParentImage.GetComponent<Image>().material);
        mStartColor = mImage.color;
        mStartParentColor = mParentImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(mSortingType == LeaderBoard.mSorting)
        {
            mImage.color = Color.cyan;
            mParentImage.color = new Color(0f, 0f, 1f, 0.75f);
        }
        else
        {
            mImage.color = mStartColor;
            mParentImage.color = mStartParentColor;
        }
    }
}
