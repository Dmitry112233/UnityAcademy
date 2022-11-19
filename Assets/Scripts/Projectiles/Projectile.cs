using Assets.Scripts.Data;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;

    public ProjectileType projectileType;

    public float destroyDelay = 3;

    public GameObject hitEffect;

    public Transform TransformToDisplay { get; set; }

    private Rigidbody rigidBody;
    private TrailRenderer trailRender;
    private Rigidbody RigidBody { get { return rigidBody = rigidBody ?? GetComponent<Rigidbody>(); } }
    private TrailRenderer TrailRenderer { get { return trailRender = trailRender ?? GetComponent<TrailRenderer>(); } }

    protected void DisplayEffect(Collision collision)
    {
        var contactPosition = collision.GetContact(0).point;
        var direction = contactPosition - TransformToDisplay.position;
        direction.Normalize();
        var effect = Instantiate(hitEffect, contactPosition, Quaternion.LookRotation(direction, Vector3.up));

        effect.transform.SetParent(collision.transform);
    }

    public void Shot(Vector3 direction)
    {
        var go = this.gameObject;
        go.SetActive(true);
        go.GetComponent<MonoBehaviour>().StartCoroutine(WaitAndRelease());
        go.GetComponent<Rigidbody>()?.AddForce(direction * go.GetComponent<Projectile>().speed, ForceMode.Impulse);

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
