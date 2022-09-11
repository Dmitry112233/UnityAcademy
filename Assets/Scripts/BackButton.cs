using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasToOpen;

    private GameObject currentCanvas;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        currentCanvas = transform.parent.gameObject;

        if (button != null && currentCanvas != null)
        {
            button.onClick.AddListener(() =>
            {
                currentCanvas.SetActive(false);
                canvasToOpen.SetActive(true);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
