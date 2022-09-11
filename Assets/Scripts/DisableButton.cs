using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    private Button button;
    private GameObject buttonOne;
    private GameObject buttonTwo;

    void Start()
    {
        button = GetComponent<Button>();
        buttonOne = transform.parent.gameObject.transform.Find("One")?.gameObject;
        buttonTwo = transform.parent.gameObject.transform.Find("Two")?.gameObject;

        if (button != null && buttonOne != null && buttonTwo != null)
        {
            button.onClick.AddListener(() =>
            {
                buttonOne.SetActive(false);
                buttonTwo.SetActive(false);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
