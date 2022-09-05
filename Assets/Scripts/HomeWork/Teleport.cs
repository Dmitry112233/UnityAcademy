using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private float timeToTeleport = 2.0f;

    private float timer;

    private float xMin = -30.0f;
    private float xMax = 30.0f;
    private float zMin = -30.0f;
    private float zMax = 30.0f;
    private float yMin = 0.0f;
    private float yMax = 30.0f;

    void Start()
    {
        timer = timeToTeleport;
    }

    void Update()
    {
        DoTeleport();
    }

    private void DoTeleport() 
    {
        timer += Time.deltaTime;

        if(timer >= timeToTeleport) 
        {
            transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), Random.Range(zMin, zMax));
            timer = 0.0f;
        }
    }
}
