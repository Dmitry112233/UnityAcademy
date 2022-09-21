using Cinemachine;
using System.Collections.Generic;
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
                    var touch = new List<Touch>(Input.touches).Find(x => x.position.x > Screen.width / 2);
                    return touch.deltaPosition.x / TouchSensitivity_x;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            case "Mouse Y":
                if (Input.touchCount > 0)
                {
                    var touch = new List<Touch>(Input.touches).Find(x => x.position.x > Screen.width / 2);
                    return touch.deltaPosition.y / TouchSensitivity_y;
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
