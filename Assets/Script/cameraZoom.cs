using UnityEngine;

public class ToggleCameraZoom : MonoBehaviour
{
    public Camera mainCamera; // Assign the camera in the Inspector
    public float zoomInSize = 5f;  // Orthographic size or FOV for zoomed-in state
    public float zoomOutSize = 10f; // Orthographic size or FOV for zoomed-out state
    public float zoomSpeed = 2f;  // Speed of the zoom transition

    private bool isZoomedOut = false; // Tracks the zoom state
    private float targetZoom; // Target zoom level

    void Start()
    {
        // Initialize the camera zoom level
        targetZoom = mainCamera.orthographicSize; // For 2D (Orthographic)
    }

    void Update()
    {
        // Check for the 'E' key press to toggle zoom
        if (Input.GetKeyDown(KeyCode.E))
        {
            isZoomedOut = !isZoomedOut; // Toggle the zoom state
            targetZoom = isZoomedOut ? zoomOutSize : zoomInSize;
        }

        // Smoothly transition to the target zoom
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed); // For 2D
    }
}
