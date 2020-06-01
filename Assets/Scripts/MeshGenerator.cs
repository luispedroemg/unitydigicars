using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public int xSize = 20;
    public int zSize = 30;
    public float noiseXScale = 0.5f;
    public float noiseZScale = 0.5f;
    public float maxHeight = 3;
    public float scale = 2;
    public float flatRadius = 5f;
    public Gradient gradient;

    private Vector3[] _vertices;
    private int[] _triangles;
    private Mesh _mesh;
    private Vector2[] _uvs;
    private Color[] _colors;
    private float _maxTerrainHeight;

    private void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        CreateShape();
        CreateColors();
        UpdateMesh();
    }

    private void Update()
    {
        // CreateColors();
        // UpdateMesh();
    }

    private void CreateShape()
    {
        _vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z < zSize + 1; z++)
        {
            for (int x = 0; x < xSize + 1; x++)
            {
                Vector2 center = new Vector2(xSize / 2f, zSize / 2f);
                Vector2 current = new Vector2(x, z);
                float height = 0f;
                if (Vector2.Distance(current, center) > flatRadius)
                {
                    height = Mathf.PerlinNoise(x * noiseXScale + Random.value * 0.1f,
                        z * noiseZScale + Random.value * 0.1f) * maxHeight;
                }
                _vertices[i] = new Vector3(x * scale, height, z * scale);
                i++;
            }
        }

        _triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                _triangles[tris + 0] = vert + 0;
                _triangles[tris + 1] = vert + xSize + 1;
                _triangles[tris + 2] = vert + 1;
                _triangles[tris + 3] = vert + 1;
                _triangles[tris + 4] = vert + xSize + 1;
                _triangles[tris + 5] = vert + xSize + 2;

                tris += 6;
                vert++;
            }

            vert++;
        }

        _uvs = new Vector2[_vertices.Length];
        for (int i=0, z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                _uvs[i] = new Vector2((float)x/xSize, (float)z/zSize);
                i++;
            }
        }
    }
    private void CreateColors()
    {
        _colors = new Color[_vertices.Length];
        for (int i=0, z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                Debug.Log("max height: " + maxHeight);
                Debug.Log("target_y: " + _vertices[i].y);
                float height = Mathf.InverseLerp(0, maxHeight, _vertices[i].y);
                Debug.Log("Vertex Color: "+gradient.Evaluate(height));
                _colors[i] = gradient.Evaluate(height);
                i++;
            }
        }
    }

    private void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        //_mesh.uv = _uvs;
        _mesh.colors = _colors;
        _mesh.RecalculateNormals();
        gameObject.transform.position = new Vector3(-250,0,-265);
    }
}

