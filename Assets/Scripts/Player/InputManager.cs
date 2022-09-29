using UnityEngine;

namespace Assets.Scripts
{
    public class InputManager : MonoBehaviour
    {
        public Vector3 MovementInput { get; private set; }
        public float MouseHorizontalInput { get; private set; }
        public float MouseVerticalInput { get; private set; }

        void Update()
        {
            MovementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            MouseHorizontalInput = Input.GetAxis("Mouse X");
            MouseVerticalInput = Input.GetAxis("Mouse Y");
        }
    }
}
