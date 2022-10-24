using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitPlatform : MonoBehaviour
{
    public MeshGeneratorUtil meshGeneratorUtil;

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

        initPlatform = meshGeneratorUtil.GenerateInitPlatform(initZTransform.position);
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
                CheckPositionGeneratePlatformsUpdateInitPosition(currentPlatform.transform.position, basePlatform.transform.position);
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

    private void CheckPositionGeneratePlatformsUpdateInitPosition(Vector3 currentPlatformPosition, Vector3 basePosition)
    {
        if (isZAxis) 
        {
            var gap = currentPlatformPosition.z - basePosition.z;

            if (Mathf.Abs(gap) <= sizeZ)
            {
                var generatedPlatform = RecalculateAndGeneratePlatformsZAxis(gap, currentPlatformPosition);
                UpdateBaseAndInitPlatforms(generatedPlatform);
                initXTransform.position = new Vector3(initXTransform.position.x, initXTransform.position.y, generatedPlatform.transform.position.z);
            }
            else
            {
                ReloadScene();
            }
        }
        else
        {
            var gap = currentPlatformPosition.x - basePosition.x;

            if (Mathf.Abs(gap) <= sizeX)
            {
                var generatedPlatform = RecalculateAndGeneratePlatformsXAxis(gap, currentPlatformPosition);
                UpdateBaseAndInitPlatforms(generatedPlatform);
                initZTransform.position = new Vector3(generatedPlatform.transform.position.x, initZTransform.position.y, initZTransform.position.z);
            }
            else
            {
                ReloadScene();
            }
        }
    }

    private GameObject RecalculateAndGeneratePlatformsZAxis(float gap, Vector3 currentPlatformPosition) 
    {
        var position = new Vector3(currentPlatformPosition.x, currentPlatformPosition.y, currentPlatformPosition.z - gap / 2);

        GameObject generatedPlatform = null;
        Vector3 additionalPlatformPosition;

        if (gap > 0)
        {
            generatedPlatform = meshGeneratorUtil.GeneratePlatform(position, sizeX, sizeZ, gap, meshGeneratorUtil.GenerateVerticesPositiveGapZ, true);
            sizeZ -= gap;
            additionalPlatformPosition = new Vector3(currentPlatformPosition.x, currentPlatformPosition.y, currentPlatformPosition.z + (sizeZ / 2) + (gap / 2));
        }
        else
        {
            generatedPlatform = meshGeneratorUtil.GeneratePlatform(position, sizeX, sizeZ, gap, meshGeneratorUtil.GenerateVerticesNegativeGapZ, true);
            sizeZ += gap;
            additionalPlatformPosition = new Vector3(currentPlatformPosition.x, currentPlatformPosition.y, currentPlatformPosition.z - (sizeZ / 2) + (gap / 2));
        }

        var additionalPlatform = meshGeneratorUtil.GeneratePlatform(additionalPlatformPosition, sizeX, sizeZ, gap, meshGeneratorUtil.GenerateVerticesPartialZ, false);
        StartCoroutine(DestroyAdditionalPlatform(additionalPlatform));

        return generatedPlatform;
    }

    private GameObject RecalculateAndGeneratePlatformsXAxis(float gap, Vector3 currentPlatformPosition)
    {
        var position = new Vector3(currentPlatformPosition.x - gap / 2, currentPlatformPosition.y, currentPlatformPosition.z);
        GameObject generatedPlatform = null;
        Vector3 additionalPlatformPosition;

        if (gap > 0)
        {
            generatedPlatform = meshGeneratorUtil.GeneratePlatform(position, sizeX, sizeZ, gap, meshGeneratorUtil.GenerateVerticesPositiveGapX, true);
            sizeX -= gap;
            additionalPlatformPosition = new Vector3(currentPlatformPosition.x + (sizeX / 2) + (gap / 2), currentPlatformPosition.y, currentPlatformPosition.z);
        }
        else
        {
            generatedPlatform = meshGeneratorUtil.GeneratePlatform(position, sizeX, sizeZ, gap, meshGeneratorUtil.GenerateVerticesNegativeGapX, true);
            sizeX += gap;
            additionalPlatformPosition = new Vector3(currentPlatformPosition.x - (sizeX / 2) + (gap / 2), currentPlatformPosition.y, currentPlatformPosition.z);
        }

        var additionalPlatform = meshGeneratorUtil.GeneratePlatform(additionalPlatformPosition, sizeZ, sizeX, gap, meshGeneratorUtil.GenerateVerticesPartialX, false);
        StartCoroutine(DestroyAdditionalPlatform(additionalPlatform));

        return generatedPlatform;
    }

    private void ReloadScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateBaseAndInitPlatforms(GameObject platform) 
    {
        initPlatform = platform;
        basePlatform = platform;
    }

    private IEnumerator DestroyAdditionalPlatform(GameObject gameObject)
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }
}