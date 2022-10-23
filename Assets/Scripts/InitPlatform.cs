using UnityEngine;
using UnityEngine.SceneManagement;

public class InitPlatform : MonoBehaviour
{
    public Transform initZTransform;
    public Transform initXTransform;

    public GameObject basePlatform;
    public Material material;
    public GameObject emptyPlatformPrefab;
    
    private GameObject currentPlatform;
    private GameObject initPlatform;
    private float sizeZ;
    private float sizeX;

    private bool isZAxis;

    private void Start()
    {
        sizeZ = 1f;
        sizeX = 1f;

        initPlatform = GenerateInitPlatform();
        currentPlatform = initPlatform;

        isZAxis = false;

        initZTransform.position = initZTransform.position + transform.up * 0.1f;
        initXTransform.position = initXTransform.position + transform.up * 0.1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isZAxis = !isZAxis;
            if (currentPlatform != null)
            {
                CheckPositionGeneratePlatformsOrRestart(currentPlatform.transform.position, basePlatform.transform.position);
                Destroy(currentPlatform);
            }

            if (!isZAxis) 
            {
                currentPlatform = Instantiate(initPlatform, initZTransform.position, Quaternion.identity);
            }
            else 
            {
                currentPlatform = Instantiate(initPlatform, initXTransform.position, Quaternion.identity);
            }

            initZTransform.position = initZTransform.position + transform.up * 0.1f;
            initXTransform.position = initXTransform.position + transform.up * 0.1f;
        }
        if (currentPlatform != null)
        {
            if (!isZAxis)
            {
                currentPlatform.transform.position = currentPlatform.transform.position + currentPlatform.transform.forward * -1f * Time.deltaTime;
            }
            else
            {
                currentPlatform.transform.position = currentPlatform.transform.position + currentPlatform.transform.right * -1f * Time.deltaTime;
            }
        }
    }

    public void CheckPositionGeneratePlatformsOrRestart(Vector3 currentPlatformPosition, Vector3 basePosition)
    {
        if (isZAxis) 
        {
            var gap = currentPlatformPosition.z - basePosition.z;

            if (Mathf.Abs(gap) <= sizeZ)
            {
                var lastGeneratedPlatform = GenerateMainPlatformZ(currentPlatformPosition, gap);
                GenerateAdditionalPlatformZ(currentPlatformPosition, gap);

                initPlatform = lastGeneratedPlatform;
                basePlatform = lastGeneratedPlatform;
                initXTransform.position = new Vector3(initXTransform.position.x, initXTransform.position.y, lastGeneratedPlatform.transform.position.z);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            var gap = currentPlatformPosition.x - basePosition.x;

            if (Mathf.Abs(gap) <= sizeX)
            {
                var lastGeneratedPlatform = GenerateMainPlatformX(currentPlatformPosition, gap);
                GenerateAdditionalPlatformX(currentPlatformPosition, gap);

                initPlatform = lastGeneratedPlatform;
                basePlatform = lastGeneratedPlatform;
                initZTransform.position = new Vector3(lastGeneratedPlatform.transform.position.x, initZTransform.position.y, initZTransform.position.z);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private GameObject GenerateMainPlatformZ(Vector3 currentPlatformPosition, float gap)
    {
        GameObject mp = Instantiate(emptyPlatformPrefab, currentPlatformPosition, Quaternion.identity);
        mp.transform.position = new Vector3(currentPlatformPosition.x, currentPlatformPosition.y, currentPlatformPosition.z - gap / 2);

        var mpMeshFilter = mp.GetComponent<MeshFilter>();
        mpMeshFilter.mesh = new Mesh();

        if (gap > 0)
        {
            mpMeshFilter.mesh.vertices = GenerateVerticesPositiveGapZ(gap);
            sizeZ -= gap;
        }
        else
        {
            mpMeshFilter.mesh.vertices = GenerateVerticesNegativeGapZ(gap);
            sizeZ += gap;
        }

        mpMeshFilter.mesh.triangles = GenerateTriangles();
        mpMeshFilter.mesh.RecalculateNormals();
        mp.GetComponent<MeshCollider>().sharedMesh = mpMeshFilter.mesh;
        mp.GetComponent<Rigidbody>().isKinematic = true;

        return mp;
    }

    private GameObject GenerateMainPlatformX(Vector3 currentPlatformPosition, float gap)
    {
        GameObject mp = Instantiate(emptyPlatformPrefab, currentPlatformPosition, Quaternion.identity);
        mp.transform.position = new Vector3(currentPlatformPosition.x - gap / 2, currentPlatformPosition.y, currentPlatformPosition.z);

        var mpMeshFilter = mp.GetComponent<MeshFilter>();
        mpMeshFilter.mesh = new Mesh();

        if (gap > 0)
        {
            mpMeshFilter.mesh.vertices = GenerateVerticesPositiveGapX(gap);
            sizeX -= gap;
        }
        else
        {
            mpMeshFilter.mesh.vertices = GenerateVerticesNegativeGapX(gap);
            sizeX += gap;
        }

        mpMeshFilter.mesh.triangles = GenerateTriangles();
        mpMeshFilter.mesh.RecalculateNormals();
        mp.GetComponent<MeshCollider>().sharedMesh = mpMeshFilter.mesh;
        mp.GetComponent<Rigidbody>().isKinematic = true;

        return mp;
    }

    private GameObject GenerateAdditionalPlatformZ(Vector3 currentPlatformPosition, float gap)
    {
        GameObject mp = Instantiate(emptyPlatformPrefab, currentPlatformPosition, Quaternion.identity);

        var mpMeshFilter = mp.GetComponent<MeshFilter>();
        mpMeshFilter.mesh = new Mesh();

        if (gap > 0)
        {
            mp.transform.position = new Vector3(currentPlatformPosition.x, currentPlatformPosition.y, currentPlatformPosition.z + (sizeZ/2) + (gap/2));
            mpMeshFilter.mesh.vertices = GenerateVerticesPartialZ(gap);
        }
        else
        {
            mp.transform.position = new Vector3(currentPlatformPosition.x, currentPlatformPosition.y, currentPlatformPosition.z - (sizeZ/2) + (gap/2));
            mpMeshFilter.mesh.vertices = GenerateVerticesPartialZ(gap);
        }

        mpMeshFilter.mesh.triangles = GenerateTriangles();
        mpMeshFilter.mesh.RecalculateNormals();
        mp.GetComponent<MeshCollider>().sharedMesh = mpMeshFilter.mesh;
        mp.GetComponent<Rigidbody>().useGravity = true;

        return mp;
    }

    private GameObject GenerateAdditionalPlatformX(Vector3 currentPlatformPosition, float gap)
    {
        GameObject mp = Instantiate(emptyPlatformPrefab, currentPlatformPosition, Quaternion.identity);

        var mpMeshFilter = mp.GetComponent<MeshFilter>();
        mpMeshFilter.mesh = new Mesh();

        if (gap > 0)
        {
            mp.transform.position = new Vector3(currentPlatformPosition.x + (sizeX / 2) + (gap / 2), currentPlatformPosition.y, currentPlatformPosition.z);
            mpMeshFilter.mesh.vertices = GenerateVerticesPartialX(gap);
        }
        else
        {
            mp.transform.position = new Vector3(currentPlatformPosition.x - (sizeX / 2) + (gap / 2), currentPlatformPosition.y, currentPlatformPosition.z);
            mpMeshFilter.mesh.vertices = GenerateVerticesPartialX(gap);
        }

        mpMeshFilter.mesh.triangles = GenerateTriangles();
        mpMeshFilter.mesh.RecalculateNormals();
        mp.GetComponent<MeshCollider>().sharedMesh = mpMeshFilter.mesh;
        mp.GetComponent<Rigidbody>().useGravity = true;

        return mp;
    }

    private GameObject GenerateInitPlatform()
    {
        GameObject mp = Instantiate(emptyPlatformPrefab, initZTransform.position, Quaternion.identity);
        var mpMeshFilter = mp.GetComponent<MeshFilter>();
        mpMeshFilter.mesh = new Mesh();
        mpMeshFilter.mesh.vertices = GenerateVerticesInit();
        mpMeshFilter.mesh.triangles = GenerateTriangles();
        mpMeshFilter.mesh.RecalculateNormals();
        mp.GetComponent<MeshCollider>().sharedMesh = mpMeshFilter.mesh;

        return mp;
    }

    private Vector3[] GenerateVerticesPositiveGapZ(float gap)
    {
        return new Vector3[]
        {
            new Vector3(sizeX / 2f, 0.05f, (sizeZ - gap)/2f),
            new Vector3(sizeX / 2f, 0.05f, -1 * (sizeZ - gap)/2f),
            new Vector3(-1f * sizeX / 2f, 0.05f, (sizeZ - gap)/2f),
            new Vector3(-1f * sizeX / 2f, 0.05f, -1 * (sizeZ - gap)/2f),

            new Vector3(sizeX / 2f, -0.05f, -1f * (sizeZ - gap)/2f),
            new Vector3(-1f * sizeX / 2f, -0.05f, -1f * (sizeZ - gap)/2f),
            new Vector3(-1f * sizeX / 2f, -0.05f, (sizeZ - gap)/2f),
            new Vector3(sizeX / 2f, -0.05f, (sizeZ - gap)/2f),
        };
    }

    private Vector3[] GenerateVerticesPositiveGapX(float gap)
    {
        return new Vector3[]
        {
            new Vector3((sizeX - gap)/2f, 0.05f, sizeZ / 2f),
            new Vector3((sizeX - gap)/2f, 0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * (sizeX - gap)/2f, 0.05f, sizeZ / 2f),
            new Vector3(-1f *(sizeX - gap)/2f, 0.05f, -1f * sizeZ / 2f),

            new Vector3((sizeX - gap)/2f, -0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * (sizeX - gap)/2f, -0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * (sizeX - gap)/2f, -0.05f, sizeZ / 2f),
            new Vector3((sizeX - gap)/2f, -0.05f, sizeZ / 2f),
        };
    }

    private Vector3[] GenerateVerticesNegativeGapZ(float gap)
    {
        return new Vector3[]
        {
            new Vector3(sizeX / 2f, 0.05f, (sizeZ + gap)/2f),
            new Vector3(sizeX / 2f, 0.05f, -1f * (sizeZ + gap)/2f),
            new Vector3(-1f * sizeX / 2f, 0.05f, (sizeZ + gap)/2f),
            new Vector3(-1f * sizeX / 2f, 0.05f, -1f * (sizeZ + gap)/2f),

            new Vector3(sizeX / 2f, -0.05f, -1f * (sizeZ + gap)/2f),
            new Vector3(-1f * sizeX / 2f, -0.05f, -1f * (sizeZ + gap)/2f),
            new Vector3(-1f * sizeX / 2f, -0.05f, (sizeZ + gap)/2f),
            new Vector3(sizeX / 2f, -0.05f, (sizeZ + gap)/2f),
        };
    }

    private Vector3[] GenerateVerticesNegativeGapX(float gap)
    {
        return new Vector3[]
        {
             new Vector3((sizeX + gap)/2f, 0.05f, sizeZ / 2f),
            new Vector3((sizeX + gap)/2f, 0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * (sizeX + gap)/2f, 0.05f, sizeZ / 2f),
            new Vector3(-1f * (sizeX + gap)/2f, 0.05f, -1f * sizeZ / 2f),

            new Vector3((sizeX + gap)/2f, -0.05f, -1f * sizeZ / 2f),
            new Vector3(-1 * (sizeX + gap)/2f, -0.05f, -1f * sizeZ / 2f),
            new Vector3(-1 * (sizeX + gap)/2f, -0.05f, sizeZ / 2f),
            new Vector3((sizeX + gap)/2f, -0.05f, sizeZ / 2f),
        };
    }

    private Vector3[] GenerateVerticesPartialZ(float gap)
    {
        return new Vector3[]
        {
            new Vector3(sizeX / 2f, 0.05f, gap/2f),
            new Vector3(sizeX / 2f, 0.05f, -1f * gap/2f),
            new Vector3(-1f * sizeX / 2f, 0.05f, gap/2f),
            new Vector3(-1f * sizeX / 2f, 0.05f, -1f * gap/2f),

            new Vector3(sizeX / 2f, -0.05f, -1f * gap/2f),
            new Vector3(-1f * sizeX / 2f, -0.05f, -1f * gap/2f),
            new Vector3(-1f * sizeX / 2f, -0.05f, gap/2f),
            new Vector3(sizeX / 2f, -0.05f, gap/2f),
        };
    }

    private Vector3[] GenerateVerticesPartialX(float gap)
    {
        return new Vector3[]
        {
            new Vector3(gap/2f, 0.05f, sizeZ / 2f),
            new Vector3(gap/2f, 0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * gap/2f, 0.05f, sizeZ / 2f),
            new Vector3(-1f * gap/2f, 0.05f, -1f * sizeZ / 2f),

            new Vector3(gap/2f, -0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * gap/2f, -0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * gap/2f, -0.05f, sizeZ / 2f),
            new Vector3(gap/2f, -0.05f, sizeZ / 2f),
        };
    }

    Vector3[] GenerateVerticesInit()
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
}