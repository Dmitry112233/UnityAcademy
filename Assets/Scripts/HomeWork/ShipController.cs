using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ships;
    private int activeShip;

    [SerializeField]
    private Button buttonRight;

    [SerializeField]
    private Button buttonLeft;

    [SerializeField]
    private Button buttonRed;
    [SerializeField]
    private Button buttonBlue;
    [SerializeField]
    private Button buttonGreen;

    [SerializeField]
    private Button buttonViewUp;
    [SerializeField]
    private Button buttonViewDown;
    [SerializeField]
    private Button buttonViewFace;
    [SerializeField]
    private Button buttonViewLeft;

    [SerializeField]
    private GameObject shipCamera;

    [SerializeField]
    private Vector3 cameraPositionUp;
    [SerializeField]
    private Vector3 cameraRotationUp;

    [SerializeField]
    private Vector3 cameraPositionDown;
    [SerializeField]
    private Vector3 cameraRotationDown;

    [SerializeField]
    private Vector3 cameraPositionFace;
    [SerializeField]
    private Vector3 cameraRotationFace;

    [SerializeField]
    private Vector3 cameraPositionLeft;
    [SerializeField]
    private Vector3 cameraRotationLeft;

    void Start()
    {
        ships.ForEach(x => x.SetActive(false));
        ships[0].SetActive(true);
        activeShip = 0;

        buttonRight.onClick.AddListener(() =>
        {
            ships[activeShip].SetActive(false);
            activeShip = activeShip != (ships.Count - 1) ? activeShip+1 : 0;
            ships[activeShip].SetActive(true);
        });
          
        buttonLeft.onClick.AddListener(() =>
        {
            ships[activeShip].SetActive(false);
            activeShip = activeShip != 0 ? activeShip - 1 : ships.Count - 1;
            ships[activeShip].SetActive(true);
        });

        buttonRed.onClick.AddListener(() =>
        {
            SetShipColor(Color.red);
        });

        buttonBlue.onClick.AddListener(() =>
        {
            SetShipColor(Color.blue);
        });

        buttonGreen.onClick.AddListener(() =>
        {
            SetShipColor(Color.green);
        });

        buttonViewLeft.onClick.AddListener(() => 
        {
            SetShipCameraPositionColor(cameraPositionLeft, cameraRotationLeft);
        });

        buttonViewFace.onClick.AddListener(() =>
        {
            SetShipCameraPositionColor(cameraPositionFace, cameraRotationFace);
        });

        buttonViewUp.onClick.AddListener(() =>
        {
            SetShipCameraPositionColor(cameraPositionUp, cameraRotationUp);
        });

        buttonViewDown.onClick.AddListener(() =>
        {
            SetShipCameraPositionColor(cameraPositionDown, cameraRotationDown);
        });
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                ships[activeShip].transform.Rotate(0, touch.deltaPosition.x / 3, 0, Space.Self);
            }
        }
    }

    private void SetShipColor(Color color) 
    {
        ships[activeShip].GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    private void SetShipCameraPositionColor(Vector3 position, Vector3 rotation)
    {
        shipCamera.transform.position = position;
        shipCamera.transform.rotation = Quaternion.Euler(rotation);
    }
}
