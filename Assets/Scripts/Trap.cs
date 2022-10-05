using UnityEngine;

public class Trap : MonoBehaviour
{
    public PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            playerController.PlayerDie();
        }
    }
}
