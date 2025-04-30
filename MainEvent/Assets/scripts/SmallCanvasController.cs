using UnityEngine;

public class SmallCanvasController : MonoBehaviour
{
    public GameObject smallCanvas;

    void Start()
    {
        // Check if this is the first time opening the app
        if (PlayerPrefs.GetInt("FirstTimeOpen", 1) == 1)
        {
            // First time: Show the canvas and set the flag
            smallCanvas.SetActive(true);
            PlayerPrefs.SetInt("FirstTimeOpen", 0);
        }
        else
        {
            // Not the first time: Hide the canvas
            smallCanvas.SetActive(false);
        }
    }

    public void HideCanvas()
    {
        smallCanvas.SetActive(false);
    }
}
