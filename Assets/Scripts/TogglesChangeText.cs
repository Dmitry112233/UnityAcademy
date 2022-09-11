using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TogglesChangeText : MonoBehaviour
{
    private Toggle toggle;
    private TextMeshProUGUI displayText;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        displayText = transform.parent.parent.gameObject.transform.Find("DisplayText")?.gameObject.GetComponent<TextMeshProUGUI>();

        if (toggle != null && displayText != null)
        {
            toggle.onValueChanged.AddListener((value) =>
            {
                if (toggle.isOn) 
                {
                    displayText.SetText($"{GetComponentInChildren<Text>().text}");

                }
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
