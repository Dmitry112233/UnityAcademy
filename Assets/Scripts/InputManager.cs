using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public bool IsJump { get; private set; }

    void Update()
    {

        Horizontal = Input.GetAxis("Horizontal");
        IsJump = Input.GetKeyDown(KeyCode.W);

    }
}
