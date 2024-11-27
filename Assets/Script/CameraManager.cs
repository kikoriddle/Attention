using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance; // Singleton instance
    public float consistentSize = 5f; // Default camera size

    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void Start()
    {
        ApplyCameraSize();
    }

    public void ApplyCameraSize()
    {
        Camera mainCamera = Camera.main; // Get the main camera
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = consistentSize; // Apply consistent size
        }
        else
        {
            Debug.LogWarning("No Main Camera found in the scene.");
        }
    }
}
