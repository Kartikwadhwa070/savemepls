using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;

public class ARSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] imageTargets;
    private bool targetScanned = false;

    [SerializeField] private string clueSceneName = "ClueScene";

    [System.Serializable]
    public class TargetClueMapping
    {
        public string targetName;
        public string clueName;
    }

    [SerializeField] private TargetClueMapping[] targetClueMappings;

    void Start()
    {
        Debug.Log("ARSceneManager: Starting AR Scene");

        string targetToScan = PlayerPrefs.GetString("TargetToScan", "");
        Debug.Log($"ARSceneManager: Target to scan retrieved from PlayerPrefs: {targetToScan}");

        foreach (var target in imageTargets)
        {
            if (target.name == targetToScan)
            {
                target.SetActive(true);
                Debug.Log($"ARSceneManager: Activated target {target.name}");

                var observer = target.GetComponent<ObserverBehaviour>();
                if (observer != null)
                {
                    observer.OnTargetStatusChanged += OnTargetStatusChanged;
                }
            }
            else
            {
                target.SetActive(false);
                Debug.Log($"ARSceneManager: Deactivated target {target.name}");
            }
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        Debug.Log($"ARSceneManager: Target status changed - Status: {targetStatus.Status}");

        if (targetStatus.Status == Status.TRACKED && !targetScanned)
        {
            targetScanned = true;
            Debug.Log($"ARSceneManager: Target {behaviour.TargetName} successfully tracked");

            // Find the corresponding clue name for this target
            string clueName = FindClueName(behaviour.TargetName);
            if (clueName != null)
            {
                SaveClueState(clueName);
            }
            else
            {
                Debug.LogError($"ARSceneManager: No clue name found for target {behaviour.TargetName}");
            }

            // Return to the clue scene after a short delay
            Debug.Log($"ARSceneManager: Will try to load scene: {clueSceneName}");
            Invoke("ReturnToClueScene", 5f);
        }
    }

    private string FindClueName(string targetName)
    {
        foreach (var mapping in targetClueMappings)
        {
            if (mapping.targetName == targetName)
            {
                Debug.Log($"ARSceneManager: Found clue name '{mapping.clueName}' for target '{targetName}'");
                return mapping.clueName;
            }
        }
        Debug.LogError($"ARSceneManager: No clue name found for target '{targetName}'");
        return null;
    }

    private void SaveClueState(string clueName)
    {
        // Save clue state in PlayerPrefs
        Debug.Log($"ARSceneManager: Setting PlayerPrefs for {clueName}_Scanned to 1");

        PlayerPrefs.SetInt(clueName + "_Scanned", 1);
        PlayerPrefs.Save();
        Debug.Log($"ARSceneManager: PlayerPrefs successfully saved for {clueName}_Scanned");

        // Verify that the value is correctly saved
        int savedState = PlayerPrefs.GetInt(clueName + "_Scanned", 0);
        Debug.Log($"ARSceneManager: Retrieved PlayerPrefs for {clueName}_Scanned after saving: {savedState}");
    }

    private void ReturnToClueScene()
    {
        Debug.Log($"ARSceneManager: Attempting to return to scene: {clueSceneName}");
        SceneManager.LoadScene(clueSceneName);
    }
}
