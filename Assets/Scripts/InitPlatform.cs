using UnityEngine;
using UnityEngine.SceneManagement;

public class InitPlatform : MonoBehaviour
{
    public GameObject initPlatform;
    public GameObject basePlatform;
    public Material material;
    public GameObject generatedPlatform;
    private GameObject currentPlatform;

    private float size;

    private void Start()
    {
        size = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentPlatform != null)
            {
                CheckPositionGeneratePlatformsOrRestart(currentPlatform.transform.position, basePlatform.transform.position, size);
                Destroy(currentPlatform);
            }

            currentPlatform = Instantiate(initPlatform, transform.position, Quaternion.identity);
            transform.position = transform.position + transform.up * 0.1f;
        }
        if (currentPlatform != null)
        {
            currentPlatform.transform.position = currentPlatform.transform.position + currentPlatform.transform.forward * -1f * Time.deltaTime;
        }
    }

    public void CheckPositionGeneratePlatformsOrRestart(Vector3 currentPlatformPosition, Vector3 basePosition, float size)
    {
        var gap = currentPlatformPosition.z - basePosition.z;

        if (Mathf.Abs(gap) <= size)
        {
            var lastGeneratedPlatform = GenerateMainPlatform(currentPlatformPosition, gap);
            GenerateAdditionalPlatform(currentPlatformPosition, gap);

            initPlatform = lastGeneratedPlatform;
            basePlatform = lastGeneratedPlatform;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private GameObject GenerateMainPlatform(Vector3 currentPlatformPosition, float gap)
    {
        GameObject mp = Instantiate(generatedPlatform, currentPlatformPosition, Quaternion.identity);
        mp.transform.position = new Vector3(currentPlatformPosition.x, currentPlatformPosition.y, currentPlatformPosition.z - gap / 2);
        
        //Color is black
        //mp.transform.rotation = mp.transform.rotation * Quaternion.AngleAxis(180f, mp.transform.right);

        var mpMeshFilter = mp.GetComponent<MeshFilter>();
        mpMeshFilter.mesh = new Mesh();

        if (gap > 0)
        {
            mpMeshFilter.mesh.vertices = GenerateVerticesPositiveGap(gap);
            size -= gap;
        }
        else
        {
            mpMeshFilter.mesh.vertices = GenerateVerticesNegativeGap(gap);
            size += gap;
        }

        mpMeshFilter.mesh.triangles = GenerateTriangles();
        mpMeshFilter.mesh.RecalculateNormals();
        mp.GetComponent<MeshCollider>().sharedMesh = mpMeshFilter.mesh;
        mp.GetComponent<Rigidbody>().isKinematic = true;

        return mp;
    }

    private GameObject GenerateAdditionalPlatform(Vector3 currentPlatformPosition, float gap)
    {
        GameObject mp = Instantiate(generatedPlatform, currentPlatformPosition, Quaternion.identity);

        var mpMeshFilter = mp.GetComponent<MeshFilter>();
        mpMeshFilter.mesh = new Mesh();

        if (gap > 0)
        {
            mp.transform.position = new Vector3(currentPlatformPosition.x, currentPlatformPosition.y, currentPlatformPosition.z + (size/2) + (gap/2));
            mpMeshFilter.mesh.vertices = GenerateVerticesPartial(gap);
        }
        else
        {
            mp.transform.position = new Vector3(currentPlatformPosition.x, currentPlatformPosition.y, currentPlatformPosition.z - (size/2) + (gap/2));
            mpMeshFilter.mesh.vertices = GenerateVerticesPartial(gap);
        }

        mpMeshFilter.mesh.triangles = GenerateTriangles();
        mpMeshFilter.mesh.RecalculateNormals();
        mp.GetComponent<MeshCollider>().sharedMesh = mpMeshFilter.mesh;
        mp.GetComponent<Rigidbody>().useGravity = true;

        return mp;
    }

    private Vector3[] GenerateVerticesPositiveGap(float gap)
    {
        return new Vector3[]
        {
            new Vector3(0.5f, 0.05f, (size - gap)/2),
            new Vector3(0.5f, 0.05f, -1 * (size - gap)/2),
            new Vector3(-0.5f, 0.05f, (size - gap)/2),
            new Vector3(-0.5f, 0.05f, -1 * (size - gap)/2),

            new Vector3(0.5f, -0.05f, -1 * (size - gap)/2),
            new Vector3(-0.5f, -0.05f, -1 * (size - gap)/2),
            new Vector3(-0.5f, -0.05f, (size - gap)/2),
            new Vector3(0.5f, -0.05f, (size - gap)/2),
        };
    }

    private Vector3[] GenerateVerticesNegativeGap(float gap)
    {
        return new Vector3[]
        {
            new Vector3(0.5f, 0.05f, (size + gap)/2),
            new Vector3(0.5f, 0.05f, -1 * (size + gap)/2),
            new Vector3(-0.5f, 0.05f, (size + gap)/2),
            new Vector3(-0.5f, 0.05f, -1 * (size + gap)/2),

            new Vector3(0.5f, -0.05f, -1 * (size + gap)/2),
            new Vector3(-0.5f, -0.05f, -1 * (size + gap)/2),
            new Vector3(-0.5f, -0.05f, (size + gap)/2),
            new Vector3(0.5f, -0.05f, (size + gap)/2),
        };
    }

    private Vector3[] GenerateVerticesPartial(float gap)
    {
        return new Vector3[]
        {
            new Vector3(0.5f, 0.05f, gap/2),
            new Vector3(0.5f, 0.05f, -1 * gap/2),
            new Vector3(-0.5f, 0.05f, gap/2),
            new Vector3(-0.5f, 0.05f, -1 * gap/2),

            new Vector3(0.5f, -0.05f, -1 * gap/2),
            new Vector3(-0.5f, -0.05f, -1 * gap/2),
            new Vector3(-0.5f, -0.05f, gap/2),
            new Vector3(0.5f, -0.05f, gap/2),
        };
    }

    int[] GenerateTriangles()
    {
        return new int[] { 0, 1, 3, 0, 3, 2, 3, 1, 4, 3, 4, 5, 3, 5, 6, 6, 2, 3, 5, 4, 7, 6, 5, 7, 0, 2, 7, 2, 6, 7, 0, 4, 1, 0, 7, 4 };
    }
}