using UnityEngine;

public class ChangeTexture : ObjectMove
{
    public Mesh[] meshes;

    private MeshFilter meshFilter;
    private MeshFilter MeshFilter { get { return meshFilter = meshFilter ?? GetComponent<MeshFilter>(); } }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            MeshFilter.mesh = meshes[Random.Range(0, meshes.Length)];
        }
    }

}
