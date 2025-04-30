using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    public void ReturnToClueScene()
    {
        SceneManager.LoadScene("NewClueScene");
    }
}
