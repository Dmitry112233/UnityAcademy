using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{

    private Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();
    }



    Vector3[] GenerateVertices() 
    {
        return new Vector3[]
        {
            new Vector3(0.5f, 0.05f, 0.5f),
            new Vector3(0.5f, 0.05f, -0.5f),
            new Vector3(-0.5f, 0.05f, 0.5f),
            new Vector3(-0.5f, 0.05f, -0.5f),
            
            new Vector3(0.5f, -0.05f, -0.5f),
            new Vector3(-0.5f, -0.05f, -0.5f),
            new Vector3(-0.5f, -0.05f, 0.5f),
            new Vector3(0.5f, -0.05f, 0.5f),
        };
    }

    int[] GenerateTriangles()
    {
        return new int[] { 0, 1, 3, 0, 3, 2, 3, 1, 4, 3, 4, 5, 3, 5, 6, 6, 2, 3, 5, 4, 7, 6, 5, 7, 0, 2, 7, 2, 6, 7, 0, 4, 1, 0, 7, 4};
    }
}
