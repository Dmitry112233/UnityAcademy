using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    public float destroyDelay = 3;

    protected IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
