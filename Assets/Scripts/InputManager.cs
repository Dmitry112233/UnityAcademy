using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector3 MovementInput { get; private set; }
    public bool SprintInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool DieInput { get; private set; }
    public bool JumpInput { get; private set; }
   
    void Update()
    {
        MovementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        SprintInput = Input.GetKey(KeyCode.LeftShift);
        AttackInput = Input.GetMouseButtonDown(0);
        DieInput = Input.GetKeyDown(KeyCode.E);
        JumpInput = Input.GetButtonDown("Jump");
    }
}
