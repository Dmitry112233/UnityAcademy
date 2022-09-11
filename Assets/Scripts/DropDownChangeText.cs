using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropDownChangeText : MonoBehaviour
{

    private TMP_Dropdown dropdown;
    private TextMeshProUGUI displayText;

    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        displayText = transform.parent.gameObject.transform.Find("DisplayText").gameObject.GetComponent<TextMeshProUGUI>();

        if (dropdown != null && displayText != null)
        {
            dropdown.onValueChanged.AddListener((value) =>
            {
                displayText.SetText($"{dropdown.options[dropdown.value].text}");
            });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
