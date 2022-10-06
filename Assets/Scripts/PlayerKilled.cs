using UnityEngine;

public class PlayerKilled : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>()?.PlayerDie();
        }
    }
}
