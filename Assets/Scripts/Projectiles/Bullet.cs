using Assets.Scripts.Data;
using UnityEngine;

public class Bullet : Projectile
{

    void Start()
    {
        StartCoroutine(WaitAndDestroy());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitEffect != null)
        {
            AudioManager.Instance.Play3DAudio(transform, MyTags.AudioSourceNames.Bullet);
            DisplayEffect(collision);
            Destroy(gameObject);
        }
    }
}
