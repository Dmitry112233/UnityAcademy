using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsChangeText : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI displayText;

    void Start()
    {
        button = GetComponent<Button>();
        displayText = transform.parent.gameObject.transform.Find("DisplayText").gameObject.GetComponent<TextMeshProUGUI>();

        if (button != null && displayText != null)
        {
            button.onClick.AddListener(() =>
            {
                displayText.SetText($"{GetComponentInChildren<TextMeshProUGUI>().text} clicked");
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
