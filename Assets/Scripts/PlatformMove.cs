using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public MeshRecalculate meshRecalculate;
    public GameObject platformPrefab;
    public Transform basePlatform;
    private GameObject currentPlatform;

    private void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (currentPlatform != null)
            {
                meshRecalculate.GenerateMainPlatform(currentPlatform.transform.position, basePlatform.position);
                Destroy(currentPlatform);
            }

            currentPlatform = Instantiate(platformPrefab, transform.position, Quaternion.identity);
            transform.position = transform.position + transform.up * 0.1f;
        }
        if (currentPlatform != null) 
        {
            currentPlatform.transform.position = currentPlatform.transform.position + currentPlatform.transform.forward * -1f * Time.deltaTime;
        }
    }
}
