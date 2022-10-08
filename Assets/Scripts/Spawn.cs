using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] prefab;

    private void Start()
    {
        Physics.IgnoreLayerCollision(3, 3, true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(prefab[Random.Range(0, prefab.Length)], transform.position, Quaternion.identity);
        }
    }
}
