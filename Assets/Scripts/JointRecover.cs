using UnityEngine;

public class JointRecover : MonoBehaviour
{
    public  PlayerController playerController;
    private HingeJoint2D hg;
    public HingeJoint2D HingeJoint { get { return hg = hg ?? GetComponent<HingeJoint2D>(); } }

    void Update()
    {
        if (!playerController.isAlive)
        {
            HingeJoint.useMotor = true;
        }
        else if (HingeJoint.useMotor)
        {
            HingeJoint.useMotor = false;
        }
    }
}
