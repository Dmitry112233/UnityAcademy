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
            var slowZoneArea = 1 << NavMesh.GetAreaFromName("SlowZone");


            // Потомучто у нас 32  система и побитовое умножение даст единицу только когда единица будет под единецей того же разряда другого числа => 0000 0010 0000
            //                                                                                                                                         0000 0010 0000
            if ((hit.mask & slowZoneArea) != 0)
            {
                agent.speed = 2f;
            }
            else
            {
                agent.speed = 8f;
            }
        }
    }
}
