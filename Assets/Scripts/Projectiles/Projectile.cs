using Assets.Scripts.Data;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;

    public float destroyDelay = 3;

    public GameObject hitEffect;

    public Transform PlayerTransform { get; set; }

    private Rigidbody rigidBody;
    private TrailRenderer trailRender;
    private Rigidbody RigidBody { get { return rigidBody = rigidBody ?? GetComponent<Rigidbody>(); } }
    private TrailRenderer TrailRenderer { get { return trailRender = trailRender ?? GetComponent<TrailRenderer>(); } }

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
        var projectile = PoolObjectManager.Instance.GetPooledObject(this);

        projectile.SetActive(true);
        projectile.transform.position = projectileInitPosition.position;
        projectile.transform.rotation = Quaternion.identity;

        projectile.transform.SetParent(projectileInitPosition);

        projectile.GetComponent<Projectile>().PlayerTransform = transform;

        projectile.GetComponent<MonoBehaviour>().StartCoroutine(projectile.GetComponent<Projectile>().WaitAndRelease());

        if (isThrowWithAngle)
        {
            var direction = Quaternion.AngleAxis(-45.0f, projectileInitPosition.right) * projectileInitPosition.forward;
            direction.Normalize();
            projectile.GetComponent<Rigidbody>()?.AddForce(direction * projectile.GetComponent<Projectile>().speed, ForceMode.Impulse);
        }
        else 
        {
            projectile.GetComponent<Rigidbody>()?.AddForce(projectileInitPosition.forward * projectile.GetComponent<Projectile>().speed, ForceMode.Impulse);
        }

        AudioManager.Instance.PlayAudio(MyTags.AudioSourceNames.Shot);
    }

    protected virtual void Release() 
    {
        RigidBody.velocity = Vector3.zero;
        RigidBody.angularVelocity = Vector3.zero;
        TrailRenderer.Clear();
        gameObject.SetActive(false);
        PoolObjectManager.Instance.ReturnToPool(this);
    }

    protected virtual IEnumerator WaitAndRelease()
    {
        yield return new WaitForSeconds(destroyDelay);
        Release();
    }
}
