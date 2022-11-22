using Assets.Scripts.Data;
using UnityEngine;

public class Bullet : Projectile
{
    private void OnCollisionEnter(Collision collision)
    {
        if (hitEffect != null)
        {
            AudioManager.Instance.Play3DAudio(transform, MyTags.AudioSourceNames.Bullet);
            DisplayEffect(collision);
            Relese();
        }
    }
}
