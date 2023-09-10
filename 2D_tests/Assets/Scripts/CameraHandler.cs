using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static Vector3 mTargetPosition;
    public float mTime = 7f;
    public float mMultiplier = 5f;
    public float mOffset = -0.025f;
    public float mRotationMax = 5f;
    private float mOriginalOrthographicSize;

    private Vector3 mOriginalPosition;
    Vector3 mMinPos;
    Vector3 mMaxPos;
    void Start()
    {
        mTargetPosition = new Vector3();
        mOriginalOrthographicSize = GetComponent<Camera>().orthographicSize;
        mOriginalPosition = transform.position;

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinPos = new Vector3(-width, -height, 0);
        mMaxPos = new Vector3(width, height, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if(GameControler.status == GameControler.GameStatus.Lose)
        {
            Vector3 target_pos = (mOriginalPosition * (mOffset + 1 / mMultiplier) + mTargetPosition * (1 - mOffset - 1 / mMultiplier));
            Vector3 new_pos = Vector3.Lerp(transform.position, target_pos, Time.unscaledDeltaTime * mTime);
            transform.position = new Vector3(new_pos.x, new_pos.y, transform.position.z);

            float screen_ratio = Mathf.InverseLerp(0f, mMaxPos.x, Mathf.Abs(target_pos.x));
            Debug.Log("Ratio : " + screen_ratio);
            float target_rotation = (target_pos.x < 0) ? mRotationMax : -mRotationMax;
            target_rotation *= screen_ratio;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f,0f, target_rotation), Time.unscaledDeltaTime * mTime);

            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, mOriginalOrthographicSize / mMultiplier, Time.unscaledDeltaTime * mTime);

        }
        else
        {
            Vector3 new_pos = Vector3.Lerp(transform.position, mOriginalPosition, Time.unscaledDeltaTime * mTime);
            transform.position = new Vector3(new_pos.x, new_pos.y, transform.position.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.unscaledDeltaTime * mTime);


            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, mOriginalOrthographicSize, Time.unscaledDeltaTime * mTime);
        }
    }
}
