using UnityEngine;
using UnityEngine.UI;

public class ToggleColorChange : MonoBehaviour
{
    public Toggle toggle;           // Reference to the Toggle
    public Image background;        // Reference to the background Image component
    public Color onColor = Color.green;  // Color when Toggle is ON
    public Color offColor = Color.white; // Color when Toggle is OFF

    private void Start()
    {
        if (toggle == null || background == null)
        {
            Debug.LogError("Toggle or Background is not assigned!");
            return;
        }

        // Set the initial background color based on the toggle's state
        UpdateBackgroundColor(toggle.isOn);

        // Add a listener to handle state changes
        toggle.onValueChanged.AddListener(UpdateBackgroundColor);
    }

    private void UpdateBackgroundColor(bool isOn)
    {
        background.color = isOn ? onColor : offColor;
    }

    private void OnDestroy()
    {
        // Remove listener when the script is destroyed to avoid memory leaks
        toggle.onValueChanged.RemoveListener(UpdateBackgroundColor);
    }
}
