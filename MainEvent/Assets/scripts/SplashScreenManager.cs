using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    public float delay = 3f; 
    void Start()
    {
        Invoke("LoadNextScene", delay);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
