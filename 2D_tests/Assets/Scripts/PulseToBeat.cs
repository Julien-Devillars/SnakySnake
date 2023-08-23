using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseToBeat : MonoBehaviour
{
    [SerializeField] bool mUseTestBeat;
    [SerializeField] float mPulseSize = 1.15f;
    [SerializeField] float mReturnSpeed = 5f;
    private Vector3 mStartSize;
    private void Start()
    {
        mStartSize = transform.localScale;
        if (mUseTestBeat)
        {
            StartCoroutine(TestBeat());
        }
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, mStartSize, Time.deltaTime * mReturnSpeed);
    }

    public void Pulse()
    {
        transform.localScale = mStartSize * mPulseSize;
    }

    IEnumerator TestBeat()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            Pulse();
        }
    }
}