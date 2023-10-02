using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLink : MonoBehaviour
{
    LineRenderer lineRenderer;
    MeshCollider mesh;
    BoxCollider2D lineCollider;
    private Enemy mStartEnemy;
    private Enemy mEndEnemy;
    private float scale = 0.5f;
    void Awake()
    {
        gameObject.name = "Link";
        gameObject.tag = "Link";
        gameObject.layer = 21;
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        // Set 2 points
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.numCapVertices = 8;
        // Hide linerenderer until update
        //lineRenderer.enabled = false;
        lineRenderer.sortingLayerName = "Link";

        // Create Box collider
        lineCollider = gameObject.AddComponent<BoxCollider2D>();
        lineCollider.size = new Vector2(transform.localScale.x, transform.localScale.x);
        lineCollider.isTrigger = false;

        // Set Material
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        mStartEnemy = null;
        mEndEnemy = null;
    }

    public void setWidth(float width)
    {
        lineRenderer.startWidth = scale * width;
        lineRenderer.endWidth = scale * width;
        lineCollider.size = new Vector2(scale * (width - width * 0.3f), lineCollider.size.y);
    }

    public void setStartEnemy(Enemy _enemy) 
    {
        mStartEnemy = _enemy;
        updateStartPosition();
    }
    public void setEndEnemy(Enemy _enemy)
    {
        mEndEnemy = _enemy;
        updateEndPosition();
    }
    public void updateStartPosition()
    {
        lineRenderer.SetPosition(0, mStartEnemy.transform.position);
    }
    public void updateEndPosition()
    {
        lineRenderer.SetPosition(1, mEndEnemy.transform.position);
    }
    public void updatePosition()
    {
        transform.position = (mStartEnemy.transform.position + mEndEnemy.transform.position) / 2f;
        updateCollider();
    }
    public void updateCollider()
    {
        float dist = Vector2.Distance(mStartEnemy.transform.position, transform.position);            
        lineCollider.size = new Vector2(lineCollider.size.x, dist * 2f);

        Vector3 relative_pos = mStartEnemy.transform.position - transform.position;
        float angle = Mathf.Atan2(relative_pos.y, relative_pos.x) * Mathf.Rad2Deg;
        angle += 90f; // Because the default forward is lookign at top and not right
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
        transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        updateStartPosition();
        updateEndPosition();
        updatePosition();
        updateCollider();
    }

    protected void Lose()
    {
        CameraHandler.mTargetPosition = transform.position;
        GameControler.status = GameControler.GameStatus.Lose;
        if (Utils.HAS_LOSE)
        {
            if (GameControler.type == GameControler.GameType.Play)
            {
                CameraHandler.mTargetPosition = transform.position;
                GameControler.status = GameControler.GameStatus.Lose;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CameraHandler.mTargetPosition = collision.gameObject.transform.position;
        GameControler.status = GameControler.GameStatus.Lose;
    }

}
