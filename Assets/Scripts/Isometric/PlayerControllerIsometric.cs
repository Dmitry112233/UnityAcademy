using UnityEngine;

public class PlayerControllerIsometric : MonoBehaviour
{
    public AnimationController animationController;
    public float speed = 10;

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        transform.position = transform.position + new Vector3 (horizontal, vertical, 0).normalized * speed * Time.deltaTime;
        animationController.SetDirection(new Vector2(horizontal, vertical));
    }
}
