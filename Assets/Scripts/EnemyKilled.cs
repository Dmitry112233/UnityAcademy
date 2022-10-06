using UnityEngine;

public class EnemyKilled : MonoBehaviour
{
    public GameObject enemy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Destroy(enemy);
        }
    }
}
