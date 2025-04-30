using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject[] canvases; // Array to hold the canvases

    public void ShowCanvas(int canvasIndex)
    {
        // Loop through all canvases and toggle visibility
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == canvasIndex);
        }
    }

    public void ShowMainCanvas()
    {
        // Ensure only the main canvas is active
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == 0); // Assuming MainCanvas is at index 0
        }
    }
}
