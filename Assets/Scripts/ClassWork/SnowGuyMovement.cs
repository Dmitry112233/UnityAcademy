using UnityEngine;

public class SnowGuyMovement : MonoBehaviour
{
    public GameObject[] prefab;

    private GameObject instance;

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if(prefab == null) 
            {
                Debug.LogError("Prefab is Null!");
            }
            if(instance != null) 
            {
                Destroy(instance);
            }
            var rotation = Quaternion.identity;
            var position = new Vector3(Random.Range(-5, 5), Random.Range(0, 15), Random.Range(-5, 5));
            instance = Instantiate(prefab[Random.Range(0, prefab.Length)], position, rotation);
        }
    }
}
