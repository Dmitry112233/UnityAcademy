using Assets.Scripts;
using UnityEngine;

public class ColliderActivator : MonoBehaviour
{
    public GameObject colider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(MyTags.Tags.Player))
        {
            colider.SetActive(true);
        }
    }
}
