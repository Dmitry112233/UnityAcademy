using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Projectile
{
    public float radius;

    public float force;

    public AudioSource explosionAudio;

    void Start()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(destroyDelay);
        Collider[] affectedColliders = Physics.OverlapSphere(transform.position, radius);
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        new List<Collider>(affectedColliders).ForEach(x => x.attachedRigidbody?.AddExplosionForce(force, transform.position, radius));
        //AudioManager.Instance.PlayAudio(explosionAudio);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
