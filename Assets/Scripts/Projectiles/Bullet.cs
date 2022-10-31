using UnityEngine;

public class Bullet : Projectile
{

    void Start()
    {
        StartCoroutine(WaitAndDestroy());
    }
}
