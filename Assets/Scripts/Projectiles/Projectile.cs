using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;

    public float destroyDelay = 3;

    public GameObject hitEffect;

    public Transform PlayerTransform { get; set; }

    protected IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    protected void DisplayEffect(Collision collision) 
    {
        var contactPosition = collision.GetContact(0).point;
        var direction = contactPosition - PlayerTransform.position;
        direction.Normalize();
        var effect = Instantiate(hitEffect, contactPosition, Quaternion.LookRotation(direction, Vector3.up));
        effect.transform.SetParent(collision.transform);
    }
}
