using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
        EventSystem.current.SetSelectedGameObject(null);
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
