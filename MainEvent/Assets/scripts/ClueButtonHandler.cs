using UnityEngine;
using UnityEngine.SceneManagement;

public class ClueButtonHandler : MonoBehaviour
{
    public void LoadARScene(string targetName)
    {
        PlayerPrefs.SetString("TargetToScan", targetName);
        SceneManager.LoadScene("ARScene");
    }
}
