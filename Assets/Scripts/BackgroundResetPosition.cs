using UnityEngine;

public class BackgroundResetPosition : MonoBehaviour
{
    public GameObject background;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            background.transform.position = background.GetComponent<BackgroundMove>().initPosition;
        }
    }
}
