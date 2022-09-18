using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Projectile
{
    [SerializeField]
    private float radius;

    [SerializeField]
    private float force;

    void Start()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(destroyDelay);
        Collider[] affectedColliders = Physics.OverlapSphere(transform.position, radius);
        new List<Collider>(affectedColliders).ForEach(x => x.attachedRigidbody?.AddExplosionForce(force, transform.position, radius));
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
