using Assets.Scripts.Data;
using UnityEngine;

public class ProjectileStand : MonoBehaviour
{
    public GameObject projectilePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == MyTags.Player) 
        {
            other.GetComponent<PlayerController>().projectilePrefab = projectilePrefab;
        }
    }
}
