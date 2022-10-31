using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;

    public float destroyDelay = 3;

    public GameObject hitEffect;

    public Transform playerTransform;

    protected IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitEffect != null) 
        {
            var contactPosition = collision.GetContact(0).point;
            Instantiate(hitEffect, contactPosition, playerTransform.rotation);
        }
    }
}
