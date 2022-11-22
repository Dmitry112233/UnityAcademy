using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Projectile
{
    public float radius;

    public float force;

    protected override IEnumerator WaitAndRelease()
    {
        yield return new WaitForSeconds(destroyDelay);
        AudioManager.Instance.Play3DAudio(transform, MyTags.AudioSourceNames.Grenade);
        Collider[] affectedColliders = Physics.OverlapSphere(transform.position, radius);
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        new List<Collider>(affectedColliders).ForEach(x => x.attachedRigidbody?.AddExplosionForce(force, transform.position, radius));
        Relese();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
