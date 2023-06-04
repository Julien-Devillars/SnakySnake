using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class TerrainMesh : MonoBehaviour
{
    [SerializeField] Vector2 mPlaneSize = new Vector2(1,1);
    [SerializeField] int mPlaneResolution = 1;

    public List<Vector3> vertices;
    public List<Vector2> uvs;
    public List<int> triangles;

    private Mesh mMesh;
    private MeshFilter mMeshFilter;

    private Vector3 mMinBorderPos;
    private Vector3 mMaxBorderPos;
    // Start is called before the first frame update
    void Awake()
    {

        Camera cam = Camera.main;

        float width = cam.aspect * cam.orthographicSize;
        float height = cam.orthographicSize;

        mMinBorderPos = new Vector3(-width, -height, 0);
        mMaxBorderPos = new Vector3(width, height, 0);

        mMesh = new Mesh();
        mMeshFilter = GetComponent<MeshFilter>();
        mMeshFilter.mesh = mMesh;
    }

    void Update()
    {
        mMesh.Clear();
        vertices = new List<Vector3>();
        uvs = new List<Vector2>();
        triangles = new List<int>();

        GenerateP1ane(mPlaneSize, mPlaneResolution);
        EditPlane();
        AssignMesh();
    }
    void GenerateP1ane(Vector2 size, int resolution)
    {
        //Create vertices
        vertices = new List<Vector3>();
        float xPerStep = size.x / resolution;
        float yPerStep = size.y / resolution;

        for (int y = 0; y < resolution + 1; y++)
        {
            for (int x = -resolution / 2; x < resolution/2f + 1; x++)
            {
                vertices.Add(new Vector3(x * xPerStep, 0, y * yPerStep));
                uvs.Add(new Vector2((x * xPerStep) / size.x, (y * yPerStep) / size.y));
            }
        }
        //Create triangles
        triangles = new List<int>();

        for (int row = 0; row < resolution; row++)
        {
            for (int column = 0; column < resolution; column++)
            {
                int i = (row * resolution) + row + column;
                //first triangle
                triangles.Add(i);
                triangles.Add(i + resolution + 1);
                triangles.Add(i + resolution + 2);

                //second triangle
                triangles.Add(i);
                triangles.Add(i + resolution + 2);
                triangles.Add(i + 1);
            }
        }
    }

    void AssignMesh()
    {
        mMesh.vertices = vertices.ToArray();
        mMesh.triangles = triangles.ToArray();
        mMesh.uv = uvs.ToArray();
    }

    float Pow2(float val)
    {
        return Mathf.Pow(val, 2);
    }
    public float mOctane = 1f;
    public float mOffset = 1f;
    public float mAmplitude = 1f;
    void EditPlane()
    {
        for(int i = 0; i < vertices.Count; ++i)
        {
            Vector3 vertex = vertices[i];
            float alpha = 100f;

            //float gaussian // ℯ^(-x^(2)-y^(2))
            float gaussian = Mathf.Pow(vertex.x , 2) - Mathf.Pow(vertex.z , 2); // parabolic
            float x = vertex.x;
            float y = vertex.z;
            //float gaussian = Pow2(x * 0.1f) + Pow2(1.5f * y) * Mathf.Exp(-Pow2(x * 0.5f) - Pow2(y));
            //Debug.Log(gaussian);
            if(Mathf.Abs(vertex.x) < mPlaneSize.x/5f)
            {
                vertex.y = 0f;
            }
            else
            {
                float reduce = 1 - Mathf.Abs(vertex.x) * 2f / mPlaneSize.x;
                vertex.y = Mathf.PerlinNoise(vertex.x * mOctane + mOffset, vertex.z * mOctane + mOffset) * mAmplitude * reduce;
            }
            
            vertices[i] = vertex;
        }
    }

}
