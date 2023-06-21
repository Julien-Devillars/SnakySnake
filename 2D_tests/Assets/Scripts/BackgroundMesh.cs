using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMesh : MonoBehaviour
{
    private Vector3 mMinBorderPos;
    private Vector3 mMaxBorderPos;

    private const int NB_VERTICES_BY_BACKGROUND = 4;
    private const int NB_UV_BY_BACKGROUND = 4;
    private const int NB_TRIANGLES_BY_BACKGROUND = 6;

    public Vector3[] vertices;
    public Vector2[] uv;
    public int[] triangles;

    private int mLastNbBackgrounds;
    private List<Background> mLastBackgrounds;
    public bool mForceUpdate;

    void Start()
    {

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize - Utils.EPSILON();
        float height = cam.orthographicSize - Utils.EPSILON();

        mMinBorderPos = new Vector3(-width, -height, 0);
        mMaxBorderPos = new Vector3(width, height, 0);
        
        transform.position = Vector3.zero;

        mLastNbBackgrounds = 0;
        mLastBackgrounds = new List<Background>();
    }

    private void Update()
    {
        animationMaterial();

        List<Background> backgrounds = Utils.getBackgrounds();

        List<Background> backgrounds_to_draw = new List<Background>();
        //
        foreach (Background background in backgrounds)
        {
            if(!background.hasEnemies())
            {
                backgrounds_to_draw.Add(background);
            }
        }

        if (mLastBackgrounds == backgrounds_to_draw) return;
        mLastBackgrounds = backgrounds_to_draw;
        //if (mLastNbBackgrounds == backgrounds_to_draw.Count) return;

        mLastNbBackgrounds = backgrounds_to_draw.Count;
        
        Mesh mesh = new Mesh();

        vertices = new Vector3[NB_VERTICES_BY_BACKGROUND * backgrounds_to_draw.Count];
        uv = new Vector2[NB_UV_BY_BACKGROUND * backgrounds_to_draw.Count];
        triangles = new int[NB_TRIANGLES_BY_BACKGROUND * backgrounds_to_draw.Count];
        
        for(int i = 0; i < backgrounds_to_draw.Count; ++i)
        {
            Background background = backgrounds_to_draw[i];
            Dictionary<string, Vector3> points = getMeshPointFromBackground(background);
            
            // Draw 4 vertices
            vertices[(i * NB_VERTICES_BY_BACKGROUND) + 0] = points["bottom-left"];
            vertices[(i * NB_VERTICES_BY_BACKGROUND) + 1] = points["top-left"];
            vertices[(i * NB_VERTICES_BY_BACKGROUND) + 2] = points["bottom-right"];
            vertices[(i * NB_VERTICES_BY_BACKGROUND) + 3] = points["top-right"];

            // Triangle 1
            triangles[(i * NB_TRIANGLES_BY_BACKGROUND) + 0] = (i * 4) + 0;
            triangles[(i * NB_TRIANGLES_BY_BACKGROUND) + 1] = (i * 4) + 1;
            triangles[(i * NB_TRIANGLES_BY_BACKGROUND) + 2] = (i * 4) + 2;

            // Triangle 2
            triangles[(i * NB_TRIANGLES_BY_BACKGROUND) + 3] = (i * 4) + 3;
            triangles[(i * NB_TRIANGLES_BY_BACKGROUND) + 4] = (i * 4) + 2;
            triangles[(i * NB_TRIANGLES_BY_BACKGROUND) + 5] = (i * 4) + 1;

            // UV
            uv[(i * NB_UV_BY_BACKGROUND) + 0] = getNormalizedVectorFromPoint(points["bottom-left"]);
            uv[(i * NB_UV_BY_BACKGROUND) + 1] = getNormalizedVectorFromPoint(points["top-left"]);
            uv[(i * NB_UV_BY_BACKGROUND) + 2] = getNormalizedVectorFromPoint(points["bottom-right"]);
            uv[(i * NB_UV_BY_BACKGROUND) + 3] = getNormalizedVectorFromPoint(points["top-right"]);

        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private Dictionary<string, Vector3> getMeshPointFromBackground(Background background)
    {
        Dictionary<string, Vector3> points = new Dictionary<string, Vector3>();

        points["bottom-left"] = new Vector3(background.mMinBorderPos.x, background.mMinBorderPos.y);
        points["top-left"] = new Vector3(background.mMinBorderPos.x, background.mMaxBorderPos.y);
        points["top-right"] = new Vector3(background.mMaxBorderPos.x, background.mMaxBorderPos.y);
        points["bottom-right"] = new Vector3(background.mMaxBorderPos.x, background.mMinBorderPos.y);

        return points;
    }

    private Vector2 getNormalizedVectorFromPoint(Vector3 point)
    {
        float x = Mathf.InverseLerp(mMinBorderPos.x, mMaxBorderPos.x, point.x);
        float y = Mathf.InverseLerp(mMinBorderPos.y, mMaxBorderPos.y, point.y);

        return new Vector2(x, y);
    }

    private void animationMaterial()
    {
        Material material = GetComponent<MeshRenderer>().material;

        material.SetFloat("_Glow", Mathf.PingPong(Time.time, 2.0f));
    }
}
