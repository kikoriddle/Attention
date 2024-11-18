using UnityEngine;

public class cameraZoom : MonoBehaviour
{
    public Camera mainCamera; // Assign the camera in the Inspector
    public float zoomInSize = 5f;  // Your zoomed-in size
    public float zoomOutSize = 7.94f; // Your zoomed-out size
    public float zoomSpeed = 2f;  // Speed of the zoom transition

    private bool isZoomedOut = false; // Tracks the zoom state
    private float targetZoom; // Target zoom level

    void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned!");
            return;
        }

        targetZoom = mainCamera.orthographicSize; // Initialize with the current orthographic size
    }

    void Update()
    {
        // Handle zoom toggling
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleZoom();
        }

        // Smoothly transition to the target zoom
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
        }
    }

    void ToggleZoom()
    {
        isZoomedOut = !isZoomedOut; // Toggle zoom state
        targetZoom = isZoomedOut ? zoomOutSize : zoomInSize;
    }
}
