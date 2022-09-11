using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour 
{
    [SerializeField]
    private GameObject canvasToOpen;

    private GameObject mainCanvas;
    private Button button;
    
    void Start()
    {
        button = GetComponent<Button>();
        mainCanvas = transform.parent.gameObject;

        if (button != null && mainCanvas != null) 
        {
            button.onClick.AddListener(() => 
            {
                mainCanvas.SetActive(false);
                canvasToOpen.SetActive(true);
                if (canvasToOpen.name.Equals("ButtonsCanvas")) 
                {
                    canvasToOpen.transform.Find("One")?.gameObject.SetActive(true);
                    canvasToOpen.transform.Find("Two")?.gameObject.SetActive(true);
                }
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
