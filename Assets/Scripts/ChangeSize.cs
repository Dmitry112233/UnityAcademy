using UnityEngine;

public class ChangeSize : ObjectMove
{
    public float scaleLimit = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            var scale = Random.Range(0.5f, scaleLimit);
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
