using Assets.Scripts;
using UnityEngine;

public class BackgroundResetPosition : MonoBehaviour
{
    public Transform background;
    public BackgroundMove backgroundMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MyTags.Tags.Player)) 
        {
            background.position = backgroundMove.InitPosition;
        }
    }
}
