using UnityEngine;

public class cameraScriptForFinal : MonoBehaviour
{
    public Camera mainCamera; // Assign the camera in the Inspector
    public float zoomInSize = 5f;  // Orthographic size for zoomed-in state
    public float zoomOutSize = 10f; // Orthographic size for zoomed-out state
    public float zoomSpeed = 2f;  // Speed of the zoom transition

    private bool isZoomedOut = false; // Tracks the zoom state
    private float targetZoom; // Target zoom level

    public GameObject[] gameObjects; // Assign the series of GameObjects in the Inspector
    private int currentIndex = 0; // Tracks the currently active GameObject

    void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned!");
            return;
        }

        targetZoom = mainCamera.orthographicSize; // Initialize with the current orthographic size
        ActivateCurrentGameObject(); // Activate the first GameObject in the series
    }

    void Update()
    {
        // Handle zoom toggling
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleZoom();
        }

        // Smoothly transition to the target zoom
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);

        // Handle switching between GameObjects (only if zoomed in)
        if (!isZoomedOut && Input.GetKeyDown(KeyCode.RightArrow)) // Example: Right Arrow to switch to the next
        {
            SwitchToNextGameObject();
        }
    }

    void ToggleZoom()
    {
        isZoomedOut = !isZoomedOut; // Toggle zoom state
        targetZoom = isZoomedOut ? zoomOutSize : zoomInSize;
    }

    void ActivateCurrentGameObject()
    {
        // Check if there are any gameObjects assigned
        if (gameObjects == null || gameObjects.Length == 0)
        {
            Debug.LogWarning("No GameObjects assigned to the GameObjects array!");
            return;
        }

        // Deactivate all GameObjects first
        foreach (GameObject obj in gameObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        // Activate the current GameObject
        if (currentIndex >= 0 && currentIndex < gameObjects.Length && gameObjects[currentIndex] != null)
        {
            gameObjects[currentIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Current index is out of bounds or GameObject is null!");
        }
    }

    void SwitchToNextGameObject()
    {
        // Check if there are any GameObjects assigned
        if (gameObjects == null || gameObjects.Length == 0) return;

        // Increment the index and loop back if necessary
        currentIndex = (currentIndex + 1) % gameObjects.Length;

        // Activate the new current GameObject
        ActivateCurrentGameObject();
    }
}
