using Cinemachine;
using UnityEngine;

public class CameraInput : MonoBehaviour
{
    public float TouchSensitivity_x = 10f;
    public float TouchSensitivity_y = 10f;

    // Use this for initialization
    void Start()
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
    }

    float HandleAxisInputDelegate(string axisName)
    {
        switch (axisName)
        {

            case "Mouse X":

                if (Input.touchCount > 0)
                {
                    if(Input.touches[0].position.x > Screen.width / 2f) 
                    {
                        return Input.touches[0].deltaPosition.x / TouchSensitivity_x;
                    }
                    break;
                }
                if (Input.touchCount > 1)
                {
                    if (Input.touches[1].position.x > Screen.width / 2f)
                    {
                        return Input.touches[1].deltaPosition.x / TouchSensitivity_x;
                    }
                    break;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            case "Mouse Y":
                if (Input.touchCount > 0)
                {
                    if (Input.touches[0].position.y > Screen.width / 2f)
                    {
                        return Input.touches[0].deltaPosition.y / TouchSensitivity_y;
                    }
                    break;
                }
                if (Input.touchCount > 1)
                {
                    if (Input.touches[1].position.y > Screen.width / 2f)
                    {
                        return Input.touches[1].deltaPosition.y / TouchSensitivity_y;
                    }
                    break;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            default:
                Debug.LogError("Input <" + axisName + "> not recognyzed.", this);
                break;
        }

        return 0f;
    }
}
