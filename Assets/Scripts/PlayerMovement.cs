using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private Camera cam;
    private NavMeshAgent agent;
    private NavMeshHit hit;

    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) 
            {
                agent.SetDestination(hit.point);
            }
        }

        if(!agent.SamplePathPosition(NavMesh.AllAreas, 1, out hit)) 
        {
            var index = IndexFromMask(hit.mask);
            if(index == 3)
            {
                agent.speed = 2f;
            }
            else
            {
                agent.speed = 8f;
            }
        }
    }

    private int IndexFromMask(int mask)
    {
        for (int i = 0; i < 32; ++i)
        {
            if ((1 << i) == mask)
                return i;
        }
        return -1;
    }
}
