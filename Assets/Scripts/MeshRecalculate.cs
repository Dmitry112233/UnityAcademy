using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class MeshRecalculate : MonoBehaviour
{
    public Material material;

    private Mesh mesh;
    private Rigidbody rb;
    private MeshRenderer meshRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
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
        return new int[] { 0, 1, 3, 0, 3, 2, 3, 1, 4, 3, 4, 5, 3, 5, 6, 6, 2, 3, 5, 4, 7, 6, 5, 7, 0, 2, 7, 2, 6, 7, 0, 4, 1, 0, 7, 4 };
    }

    Vector3[] GenerateVerticesMain(Vector3 platformPosition, Vector3 destinationPosition)
    {
        var gap = platformPosition.z - destinationPosition.z;

        return new Vector3[]
        {
            new Vector3(0.5f, 0.05f, 0.5f - gap),
            new Vector3(0.5f, 0.05f, -0.5f),
            new Vector3(-0.5f, 0.05f, 0.5f - gap),
            new Vector3(-0.5f, 0.05f, -0.5f),

            new Vector3(0.5f, -0.05f, -0.5f),
            new Vector3(-0.5f, -0.05f, -0.5f),
            new Vector3(-0.5f, -0.05f, 0.5f - gap),
            new Vector3(0.5f, -0.05f, 0.5f - gap),
        };
    }

    public void GenerateMainPlatform(Vector3 platformPosition, Vector3 destinationPosition) 
    {
        //We need generate new objects platform with generated mesh here instead of use this one
        rb.useGravity = false;
        transform.position = platformPosition;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = GenerateVerticesMain(platformPosition, destinationPosition);
        mesh.triangles = GenerateTriangles();

        var meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;

        rb.useGravity = true;
        meshRenderer.material = material;
    }
}
