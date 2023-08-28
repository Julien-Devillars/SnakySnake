using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePulseToBeat : MonoBehaviour
{
    [SerializeField] bool mUseTestBeatColorIntensity;
    bool mWaiterColorIntensity = false;
    [SerializeField] bool mUseTestBeatLineSize;
    bool mWaiterLineSize = false;

    // Handle Color intensity
    [SerializeField] float mPulseSizeColorIntensity = 1.2f;
    [SerializeField] float mReturnSpeedColorIntensity = 5f;
    private Color mStartColorColorIntensity;

    // Handle Line Size intensity
    [SerializeField] float mPulseSizeLineSize = 3f;
    [SerializeField] float mReturnSpeedLineSize = 5f;
    private float mStartLineSize;
    private GameObject mBackground;

    SpriteRenderer mSprite;
    private void Start()
    {
        mBackground = GameObject.Find(Utils.BACKGROUND_VIEW_STR);
        mSprite = mBackground.GetComponent<SpriteRenderer>();

        mStartColorColorIntensity = mSprite.material.GetColor("_LineColor");
        mStartLineSize = mSprite.material.GetFloat("_LineSize");

        mWaiterLineSize = false;
        mWaiterColorIntensity = false;
    }

    private void Update()
    {
        mBackground = GameObject.Find(Utils.BACKGROUND_VIEW_STR);
        if (mBackground == null) return;
        mSprite = mBackground.GetComponent<SpriteRenderer>();


        if (mUseTestBeatColorIntensity && !mWaiterColorIntensity) StartCoroutine(TestBeatColorIntensity());
        if (mUseTestBeatLineSize && !mWaiterLineSize) StartCoroutine(TestBeatLineSize());
        returnToStartColorIntensity();
        returnToStartLineSize();
    }

    void updateSpriteIfNeeded()
    {
        if (mSprite != null) return;

        mBackground = GameObject.Find(Utils.BACKGROUND_VIEW_STR);
        mSprite = mBackground.GetComponent<SpriteRenderer>();
    }

    // Pulse for Line Color
    public void PulseColorIntensity()
    {
        if (GameControler.status != GameControler.GameStatus.InProgress) return;
        updateSpriteIfNeeded();
        if (mSprite == null) return;
        Color color = mSprite.material.GetColor("_LineColor");
        mSprite.material.SetColor("_LineColor", color * mPulseSizeColorIntensity);
    }

    public void returnToStartColorIntensity()
    {
        Color color = mSprite.material.GetColor("_LineColor");
        Color new_color = Color.Lerp(color, mStartColorColorIntensity, Time.deltaTime * mReturnSpeedColorIntensity);
        mSprite.material.SetColor("_LineColor", new_color);
    }

    IEnumerator TestBeatColorIntensity()
    {
        mWaiterColorIntensity = true;
        yield return new WaitForSeconds(1f);
        PulseColorIntensity();
        mWaiterColorIntensity = false;
    }

    // Pulse for Line Size
    public void PulseLineSize()
    {
        if (GameControler.status != GameControler.GameStatus.InProgress) return;
        updateSpriteIfNeeded();
        if (mSprite == null) return;
        float line_size = mSprite.material.GetFloat("_LineSize");
        mSprite.material.SetFloat("_LineSize", line_size * mPulseSizeLineSize);
    }

    public void returnToStartLineSize()
    {
        float line_size = mSprite.material.GetFloat("_LineSize");
        float new_line_size = Mathf.Lerp(line_size, mStartLineSize, Time.deltaTime * mReturnSpeedLineSize);
        mSprite.material.SetFloat("_LineSize", new_line_size);
    }

    IEnumerator TestBeatLineSize()
    {
        mWaiterLineSize = true;
        yield return new WaitForSeconds(1f);
        PulseLineSize();
        mWaiterLineSize = false;
    }
}
