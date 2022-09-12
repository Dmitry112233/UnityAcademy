using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ships;

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

    private int activeShip;

    void Start()
    {
        ships.ForEach(x => x.SetActive(false));
        ships[0].SetActive(true);
        activeShip = 0;

        buttonRight.onClick.AddListener(() =>
        {

            ships[activeShip].SetActive(false);
            if (activeShip != ships.Count - 1)
            {
                activeShip++;
                ships[activeShip].SetActive(true);
            }
            else
            {
                activeShip = 0;
                ships[activeShip].SetActive(true);
            }
        });

        buttonLeft.onClick.AddListener(() =>
        {

            ships[activeShip].SetActive(false);
            if (activeShip != 0)
            {
                activeShip--;
                ships[activeShip].SetActive(true);
            }
            else
            {
                activeShip = ships.Count - 1;
                ships[activeShip].SetActive(true);
            }
        });

        buttonRed.onClick.AddListener(() =>
        {
            ships[activeShip].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        });

        buttonBlue.onClick.AddListener(() =>
        {
            ships[activeShip].GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        });

        buttonGreen.onClick.AddListener(() =>
        {
            ships[activeShip].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
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
}
