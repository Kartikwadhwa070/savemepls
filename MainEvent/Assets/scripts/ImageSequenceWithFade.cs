using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSequenceWithFade : MonoBehaviour
{
    [SerializeField] private Image[] images;  
    [SerializeField] private float fadeDuration = 1f; 
    [SerializeField] private float delayBetweenImages = 0.5f; 
    private void Start()
    {
        StartCoroutine(ShowAndFadeImages());
    }

    private IEnumerator ShowAndFadeImages()
    {
        foreach (Image img in images)
        {
            img.gameObject.SetActive(true);  
            yield return StartCoroutine(FadeIn(img));  
            yield return new WaitForSeconds(delayBetweenImages);
        }
    }

    private IEnumerator FadeIn(Image img)
    {
        float elapsedTime = 0f;
        Color startColor = img.color;
        startColor.a = 0f;  
        img.color = startColor; 

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            img.color = new Color(startColor.r, startColor.g, startColor.b, alpha);  
            yield return null;  
        }

        img.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
    }
}
