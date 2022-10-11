using Assets.Scripts;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(MyTags.Tags.Player))
        {
            collision.gameObject.GetComponent<PlayerController>()?.TakeDamage(100f);
        }
    }
}
