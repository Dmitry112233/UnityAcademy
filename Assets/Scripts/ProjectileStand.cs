using Assets.Scripts.Data;
using UnityEngine;

public class ProjectileStand : MonoBehaviour
{
    public ProjectileType projectileType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == MyTags.Player) 
        {
            other.GetComponent<PlayerController>().projectileType = projectileType;
        }
    }
}
