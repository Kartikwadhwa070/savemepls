using UnityEngine;
using UnityEngine.UI;

public class ButtonFade : MonoBehaviour
{
    public Button button;
    public float fadeSpeed = 1f; 
    private CanvasGroup canvasGroup;
    private bool isFadingOut = true;

    void Start()
    {
        canvasGroup = button.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = button.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1f;
    }
    void Update()
    {
        if (isFadingOut)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            if (canvasGroup.alpha <= 0f)
            {
                isFadingOut = false;
            }
        }
        else
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            if (canvasGroup.alpha >= 1f)
            {
                isFadingOut = true;
            }
        }
    }
}
