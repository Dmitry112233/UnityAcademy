using UnityEngine;

public class JointRecover : MonoBehaviour
{
    public  PlayerController playerController;
    private HingeJoint2D hingeJoint2D;

    void Start()
    {
        hingeJoint2D = GetComponent<HingeJoint2D>();
    }

    void Update()
    {
        if (!playerController.isAlive)
        {
            Recover();
        }
        else 
        {
            DeleteMotor();
        }
    }

    private void Recover() 
    {
        hingeJoint2D.useMotor = true;
        JointMotor2D motor = hingeJoint2D.motor;
        motor.motorSpeed = -200;
        hingeJoint2D.motor = motor;
    }

    private void DeleteMotor() 
    {
        if (hingeJoint2D.useMotor) 
        {
            hingeJoint2D.useMotor = false;
        }
    }
}
