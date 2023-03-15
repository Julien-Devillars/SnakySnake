using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBackground : MonoBehaviour
{
    public float mSize;
    public GameObject mTile;
    public GameObject mLeft;
    public GameObject mTop;
    public GameObject mRight;
    public GameObject mBottom;

    private List<List<GameObject>> mBackground;

    // Start is called before the first frame update
    void Start()
    {
        
        mBackground = new List<List<GameObject>>();
        int idx_y = 0;

        Vector3 top_left_corner = new Vector3(mLeft.transform.position.x + mLeft.transform.localScale.x, mTop.transform.position.y + mTop.transform.localScale.y, 0);
        Vector3 bot_right_corner = new Vector3(mRight.transform.position.x - mRight.transform.localScale.x, mBottom.transform.position.y - mBottom.transform.localScale.y, 0);

        float scale_x = mTile.transform.localScale.x;
        float scale_y = mTile.transform.localScale.y;

        for (float i = -top_left_corner.y; i < -bot_right_corner.y; i += scale_x, ++idx_y)
        {
            mBackground.Add(new List<GameObject>());
            for (float j = top_left_corner.x; j < bot_right_corner.x; j += scale_y)
            {
                GameObject tile = GameObject.Instantiate(mTile);
                tile.transform.parent = transform;
                tile.transform.position = new Vector3(j * scale_x, i * scale_y, 0);
                mBackground[idx_y].Add(tile);
            }
        }
        mTile.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
