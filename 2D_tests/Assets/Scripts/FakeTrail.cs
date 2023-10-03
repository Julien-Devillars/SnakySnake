using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FakeTrail : Trail
{
    LineRenderer lineRenderer;
    Vector3 mMinPos;
    Vector3 mMaxPos;

    void Awake()
    {
        gameObject.name = "FakeTrail";
        gameObject.tag = "Trail";
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinPos = new Vector3(-width + Utils.EPSILON(), -height + Utils.EPSILON(), 0);
        mMaxPos = new Vector3(width - Utils.EPSILON(), height - Utils.EPSILON(), 0);

        lineRenderer.positionCount = 2;
        // Hide linerenderer until update
        lineRenderer.sortingLayerName = "Trail";
        mIsFake = true;
    }

    public void setStartPoint(Vector3 point)
    {
        lineRenderer.SetPosition(0, point);
    }
    public void setEndPoint(Vector3 point)
    {
        lineRenderer.SetPosition(1, point);
    }
    public Vector3 getPosFromRelative(Vector3 relative_pos)
    {
        return new Vector3(Mathf.Lerp(mMinPos.x, mMaxPos.x, relative_pos.x), Mathf.Lerp(mMinPos.y, mMaxPos.y, relative_pos.y), 0f);
    }
    public void setRelativeStartPoint(Vector3 relative_pos)
    {
        Vector3 absolute_pos = getPosFromRelative(relative_pos);
        lineRenderer.SetPosition(0, absolute_pos);
    }
    public void setRelativeEndPoint(Vector3 relative_pos)
    {
        Vector3 absolute_pos = getPosFromRelative(relative_pos);
        lineRenderer.SetPosition(1, absolute_pos);
    }

}
