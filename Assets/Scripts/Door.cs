using Unity.AI.Navigation;
using UnityEngine;

public class Door : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    private NavMeshModifier navMeshModifier;

    private void Start()
    {
        navMeshModifier = GetComponent<NavMeshModifier>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = transform.position + new Vector3(0f, 5.0f, 0.0f);
            navMeshModifier.ignoreFromBuild = true;
            navMeshSurface.BuildNavMesh();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = transform.position + new Vector3(0f, -5.0f, 0.0f);
            navMeshModifier.ignoreFromBuild = false;
            navMeshSurface.BuildNavMesh();
        }
    }
}
