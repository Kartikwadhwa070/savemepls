using UnityEngine;
using UnityEngine.UI;

public class ClueSceneManager : MonoBehaviour
{
    [System.Serializable]
    public class ClueToggleMapping
    {
        public string clueName;
        public Toggle clueToggle;
    }

    [SerializeField] private ClueToggleMapping[] clueToggleMappings;
    private const string GameSessionKey = "GameSession";
    private const string GameInitializedKey = "GameInitialized";

    private void Awake()
    {
#if UNITY_EDITOR
        // Only reset if the game hasn't been initialized this session
        if (!IsGameInitialized())
        {
            ResetGameState();
            SetGameInitialized();
            Debug.Log("ClueSceneManager: First time initialization in Editor");
        }
        else
        {
            Debug.Log("ClueSceneManager: Game already initialized, preserving state");
        }
#else
        // For builds, check if it's a fresh launch
        if (!IsGameInitialized())
        {
            ResetGameState();
            SetGameInitialized();
        }
#endif

        if (clueToggleMappings == null || clueToggleMappings.Length == 0)
        {
            Debug.LogError("ClueSceneManager: No clue toggle mappings assigned in the Inspector!");
            return;
        }

        ResetCheckboxes();
    }

    private bool IsGameInitialized()
    {
        return PlayerPrefs.GetInt(GameInitializedKey, 0) == 1;
    }

    private void SetGameInitialized()
    {
        PlayerPrefs.SetInt(GameInitializedKey, 1);
        PlayerPrefs.Save();
        Debug.Log("ClueSceneManager: Game marked as initialized");
    }

    private void ResetGameState()
    {
        Debug.Log("ClueSceneManager: Resetting game state - clearing all PlayerPrefs");
        // Store the current session ID before clearing
        PlayerPrefs.DeleteAll();
        string currentSession = System.Guid.NewGuid().ToString();
        PlayerPrefs.SetString(GameSessionKey, currentSession);
        PlayerPrefs.Save();
        Debug.Log($"ClueSceneManager: New session started with ID: {currentSession}");
    }

    private void OnEnable()
    {
        Debug.Log("ClueSceneManager: OnEnable called");
        if (clueToggleMappings == null || clueToggleMappings.Length == 0)
        {
            Debug.LogError("ClueSceneManager: No clue toggle mappings assigned in the Inspector!");
            return;
        }

        foreach (var mapping in clueToggleMappings)
        {
            if (string.IsNullOrEmpty(mapping.clueName))
            {
                Debug.LogError("ClueSceneManager: Clue name is empty or null in a mapping!");
                continue;
            }
            if (mapping.clueToggle == null)
            {
                Debug.LogError($"ClueSceneManager: Toggle for clue '{mapping.clueName}' is not assigned!");
                continue;
            }
            UpdateCheckboxState(mapping.clueName, mapping.clueToggle);
        }
    }

    private void ResetCheckboxes()
    {
        foreach (var mapping in clueToggleMappings)
        {
            if (mapping.clueToggle == null)
            {
                Debug.LogError($"ClueSceneManager: Toggle for clue '{mapping.clueName}' is not assigned!");
                continue;
            }
            mapping.clueToggle.isOn = false;
            mapping.clueToggle.interactable = false;
            Debug.Log($"ClueSceneManager: Reset checkbox for clue '{mapping.clueName}' to unticked.");
        }
    }

    private void UpdateCheckboxState(string clueName, Toggle clueToggle)
    {
        int scannedState = PlayerPrefs.GetInt($"{clueName}_Scanned", 0);
        Debug.Log($"ClueSceneManager: Retrieved value for '{clueName}_Scanned': {scannedState}");

        clueToggle.isOn = scannedState == 1;
        clueToggle.interactable = false;

        Debug.Log($"ClueSceneManager: '{clueName}' checkbox set to: {clueToggle.isOn}");
    }

    void OnApplicationQuit()
    {
#if UNITY_EDITOR
        // Clear the initialization flag when quitting in Unity Editor
        PlayerPrefs.DeleteKey(GameInitializedKey);
        PlayerPrefs.Save();
        Debug.Log("ClueSceneManager: Cleared initialization state on application quit (Editor only)");
#endif
    }
}