using Assets.Scripts.Data;
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

    public void Shot(Transform projectileInitPosition, bool isThrowWithAngle)
    {
        var bullet = Instantiate(gameObject, projectileInitPosition.position, Quaternion.identity);
        bullet.transform.SetParent(projectileInitPosition);
        bullet.GetComponent<Projectile>().PlayerTransform = transform;

        if (isThrowWithAngle)
        {
            var direction = Quaternion.AngleAxis(-45.0f, projectileInitPosition.right) * projectileInitPosition.forward;
            direction.Normalize();
            bullet.GetComponent<Rigidbody>()?.AddForce(direction * bullet.GetComponent<Projectile>().speed, ForceMode.Impulse);
        }
        else 
        {
            bullet.GetComponent<Rigidbody>()?.AddForce(projectileInitPosition.forward * bullet.GetComponent<Projectile>().speed, ForceMode.Impulse);
        }
        AudioManager.Instance.PlayAudio(MyTags.AudioSourceNames.Shot);
    }
}
