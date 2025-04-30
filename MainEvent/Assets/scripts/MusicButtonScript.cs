using UnityEngine;
using UnityEngine.UI;

public class MusicButtonScript : MonoBehaviour  
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Button playPauseButton;
    private bool isPlaying = false;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
            return;
        }

        if (playPauseButton == null)
        {
            Debug.LogError("Play/Pause button is not assigned!");
            return;
        }

        playPauseButton.onClick.AddListener(ToggleAudio);

        PlayAudio();
    }

    void PlayAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
        }
    }

    void ToggleAudio()
    {
        if (audioSource != null)
        {
            if (isPlaying)
            {
                audioSource.Pause();
                isPlaying = false;
            }
            else
            {
                audioSource.Play();
                isPlaying = true;
            }
        }
    }
}