using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class DynamicScrollSensitivity : MonoBehaviour
{
    private float baseSensitivity; // Default sensitivity provided by you
    private float fpsReference;  // Reference FPS for normal sensitivity
    private ScrollRect scrollRect;
    private float deviceScaleFactor; // Device-specific scale factor

    void Start()
    {
        // Get the ScrollRect component
        baseSensitivity = 0.17f;
        fpsReference = 60f;
        scrollRect = GetComponent<ScrollRect>();
        if (scrollRect == null)
        {
            Debug.LogError("ScrollRect component is missing!");
            return;
        }

        // Calculate the device scale factor
        CalculateDeviceScaleFactor();

        // Set initial sensitivity
        UpdateSensitivity();
    }

    void Update()
    {
        UpdateSensitivity();
    }

    void CalculateDeviceScaleFactor()
    {
        // Get the screen DPI and fallback if DPI is not available
        float dpi = Screen.dpi;
        if (dpi <= 0)
        {
            dpi = 96f; // Assume a default DPI value
        }

        // Calculate scale factor based on DPI and screen resolution
        float screenDiagonal = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);
        deviceScaleFactor = dpi / screenDiagonal;

        Debug.Log($"Device Scale Factor: {deviceScaleFactor}");
    }

    void UpdateSensitivity()
    {
        if (scrollRect == null) return;

        // Adjust sensitivity dynamically based on FPS and device scale
        float currentFps = 1f / Time.unscaledDeltaTime; // Calculate FPS
        float fpsFactor = currentFps / fpsReference; // Get FPS ratio compared to reference
        float normalizedSensitivity = baseSensitivity / deviceScaleFactor;

        // Combine sensitivity calculations
        scrollRect.scrollSensitivity = normalizedSensitivity * fpsFactor;

        // Optional: Clamp sensitivity to prevent extreme values
        scrollRect.scrollSensitivity = Mathf.Clamp(scrollRect.scrollSensitivity, 10f, 50f);
    }
}
