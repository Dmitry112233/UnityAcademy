using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private Vector3 min = new Vector3(-30.0f, 0.0f, -30.0f);

    [SerializeField]
    private Vector3 max = new Vector3(30.0f, 30.0f, 30.0f);

    [SerializeField]
    private int teleportTimer = 2;

    void Start()
    {
        InvokeRepeating("DoTeleport", teleportTimer, teleportTimer);
    }

    void Update()
    {
    }

    private void DoTeleport()
    {
        transform.position = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));

    }
}
