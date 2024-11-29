using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class DynamicScrollSensitivity : MonoBehaviour
{
    public float baseSensitivity = 0.67f; // Default sensitivity
    public float fpsReference = 60f; // Reference FPS for normal sensitivity
    private ScrollRect scrollRect;

    void Start()
    {
        // Get the ScrollRect component
        scrollRect = GetComponent<ScrollRect>();
        if (scrollRect == null)
        {
            Debug.LogError("ScrollRect component is missing!");
            return;
        }

        // Set initial sensitivity
        UpdateSensitivity();
    }

    void Update()
    {
        UpdateSensitivity();
    }

    void UpdateSensitivity()
    {
        // Adjust sensitivity dynamically based on current frame rate
        float currentFps = 1f / Time.unscaledDeltaTime; // Calculate FPS
        float fpsFactor = currentFps / fpsReference; // Get FPS ratio compared to the reference
        scrollRect.scrollSensitivity = baseSensitivity * fpsFactor;

        // Optional: Clamp the sensitivity to prevent extreme values
        scrollRect.scrollSensitivity = Mathf.Clamp(scrollRect.scrollSensitivity, 5f, 50f);
    }
}
