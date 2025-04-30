using UnityEngine;
using UnityEngine.UI;

public class ButtonColorToggle : MonoBehaviour
{
    [SerializeField] private Button targetButton; // Drag your Button here
    [SerializeField] private Toggle toggle;      // Drag your Toggle here

    private Color originalColor;

    private void Start()
    {
        if (targetButton == null || toggle == null)
        {
            Debug.LogError("Please assign both the Button and the Toggle in the Inspector.");
            return;
        }

        originalColor = targetButton.image.color;

        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        targetButton.image.color = isOn ? Color.green : originalColor;
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }
}
