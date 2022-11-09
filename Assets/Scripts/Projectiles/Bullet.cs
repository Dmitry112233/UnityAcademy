using UnityEngine;

public class Bullet : Projectile
{
    public AudioSource bulletAudio;

    void Start()
    {
        StartCoroutine(WaitAndDestroy());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitEffect != null)
        {
            //AudioManager.Instance.PlayAudio(bulletAudio);
            var contactPosition = collision.GetContact(0).point;
            var direction = contactPosition - PlayerTransform.position;
            direction.Normalize();
            var effect = Instantiate(hitEffect, contactPosition, Quaternion.LookRotation(direction, Vector3.up));
            effect.transform.SetParent(collision.transform);
            Destroy(gameObject);
        }
    }
}
