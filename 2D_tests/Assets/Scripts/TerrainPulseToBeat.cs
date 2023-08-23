using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPulseToBeat : MonoBehaviour
{
    [SerializeField] bool mUseTestBeatColorIntensity;
    [SerializeField] bool mUseTestBeatColorIntensity2;

    // Handle Color intensity
    [SerializeField] float mPulseSizeColorIntensity = 1.2f;
    [SerializeField] float mReturnSpeedColorIntensity = 5f;
    private Color mStartColorColorIntensity;

    // Handle Line Size intensity
    [SerializeField] float mPulseSizeLineSize = 3f;
    [SerializeField] float mReturnSpeedLineSize = 5f;
    private float mStartLineSize;

    MeshRenderer mMesh;
    private void Start()
    {
        mMesh = GetComponent<MeshRenderer>();
        mStartColorColorIntensity = mMesh.material.GetColor("_LineColor");
        mStartLineSize = mMesh.material.GetFloat("_LineSize");

        if (mUseTestBeatColorIntensity) StartCoroutine(TestBeatColorIntensity());
        if (mUseTestBeatColorIntensity2) StartCoroutine(TestBeatLineSize());
    }

    private void Update()
    {
        returnToStartColorIntensity();
        returnToStartLineSize();
    }

    // Pulse for Line Color
    public void PulseColorIntensity()
    {
        Color color = mMesh.material.GetColor("_LineColor");
        mMesh.material.SetColor("_LineColor", color * mPulseSizeColorIntensity);
    }

    public void returnToStartColorIntensity()
    {
        Color color = mMesh.material.GetColor("_LineColor");
        Color new_color = Color.Lerp(color, mStartColorColorIntensity, Time.deltaTime * mReturnSpeedColorIntensity);
        mMesh.material.SetColor("_LineColor", new_color);
    }

    IEnumerator TestBeatColorIntensity()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            PulseColorIntensity();
        }
    }

    // Pulse for Line Size
    public void PulseLineSize()
    {
        float line_size = mMesh.material.GetFloat("_LineSize");
        mMesh.material.SetFloat("_LineSize", line_size * mPulseSizeLineSize);
    }

    public void returnToStartLineSize()
    {
        float line_size = mMesh.material.GetFloat("_LineSize");
        float new_line_size = Mathf.Lerp(line_size, mStartLineSize, Time.deltaTime * mReturnSpeedLineSize);
        mMesh.material.SetFloat("_LineSize", new_line_size);
    }

    IEnumerator TestBeatLineSize()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            PulseLineSize();
        }
    }
}
