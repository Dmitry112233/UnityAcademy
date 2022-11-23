using Assets.Scripts.Data;
using Assets.Scripts.Managers.PoolObject;
using Assets.Scripts.Projectiles;
using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    public float speed = 10;

    public ProjectileType projectileType;

    public float destroyDelay = 3;

    public GameObject hitEffect;

    public Transform TransformToDisplay { get; set; }

    private Rigidbody rigidBody;
    private TrailRenderer trailRender;

    public event Action<IPoolable> onReleseEvent;

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

    protected virtual IEnumerator WaitAndRelease()
    {
        yield return new WaitForSeconds(destroyDelay);
        onReleseEvent?.Invoke(this);
    }

    public void Init(PoolInitData data)
    {
        var dataConverted = (PoolInitProjectileData)data;

        transform.position = dataConverted.IniPosition.position;
        transform.rotation = Quaternion.identity;
        transform.SetParent(dataConverted.IniPosition);
        GetComponent<Projectile>().TransformToDisplay = dataConverted.IniPosition;
    }

    public void Reset(Transform parentTransform)
    {
        RigidBody.velocity = Vector3.zero;
        RigidBody.angularVelocity = Vector3.zero;
        TrailRenderer.Clear();
        gameObject.SetActive(false);
        transform.SetParent(parentTransform);
    }

    public virtual void Relese()
    {
        onReleseEvent?.Invoke(this);
    }

    public void AfterCreate(Transform parrentTransform)
    {
        transform.SetParent(parrentTransform);
        gameObject.SetActive(false);
    }
}
