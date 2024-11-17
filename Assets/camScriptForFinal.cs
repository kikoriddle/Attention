using UnityEngine;
using System.Collections; // Required for Coroutines

public class camScriptForFinal : MonoBehaviour
{
    public GameObject[] gameObjects; // Assign the series of GameObjects in the Inspector
    private int currentIndex = 0; // Tracks the currently active GameObject
    public GameObject a;
    public GameObject b;

    public GameObject animationObject; // Assign the GameObject with the animation
    public Camera mainCamera; // Assign the camera in the Inspector
    private float targetZoom;

    public float zoomInSize = 5f;  // Orthographic size for zoomed-in state
    public float zoomOutSize = 10f; // Orthographic size for zoomed-out state
    public float zoomSpeed = 2f;  // Speed of the zoom transition

    private bool isZoomedOut = false; // Tracks the zoom state

    void Start()
    {
        // Ensure mainCamera is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Assign the main camera automatically
            if (mainCamera == null)
            {
                Debug.LogError("Main Camera is not assigned, and no MainCamera tag is found!");
                return;
            }
        }

        // Ensure a and b are assigned
        if (a == null || b == null)
        {
            Debug.LogError("GameObjects 'a' and 'b' must be assigned in the Inspector!");
            return;
        }

        // Ensure the animation object is assigned
        if (animationObject == null)
        {
            Debug.LogError("Animation GameObject is not assigned!");
            return;
        }

        // Initialize the orthographic size of the camera
        targetZoom = mainCamera.orthographicSize;

        // Initialize the GameObjects array with 'a' and 'b'
        gameObjects = new GameObject[] { a, b };

        // Activate the first GameObject in the series
        ActivateCurrentGameObject();
    }

    void Update()
    {
        // Handle zoom toggling
        if (Input.GetKeyDown(KeyCode.E))
        {
            isZoomedOut = !isZoomedOut; // Toggle zoom state
            targetZoom = isZoomedOut ? zoomOutSize : zoomInSize;
        }

        // Smoothly transition to the target zoom
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
        }

        // Handle switching to the next GameObject (only if zoomed in)
        if (!isZoomedOut && Input.GetKeyDown(KeyCode.D)) // Use 'D' for next
        {
            StartCoroutine(SwitchToNextGameObject());
        }

        // Handle switching to the previous GameObject (only if zoomed in)
        if (!isZoomedOut && Input.GetKeyDown(KeyCode.A)) // Use 'A' for previous
        {
            StartCoroutine(SwitchToPreviousGameObject());
        }
    }

    void ActivateCurrentGameObject()
    {
        if (gameObjects == null || gameObjects.Length == 0)
        {
            Debug.LogError("GameObjects array is empty or not initialized!");
            return;
        }

        // Deactivate all GameObjects first
        foreach (GameObject obj in gameObjects)
        {
            if (obj != null) obj.SetActive(false);
        }

        // Activate the current GameObject
        if (currentIndex >= 0 && currentIndex < gameObjects.Length && gameObjects[currentIndex] != null)
        {
            gameObjects[currentIndex].SetActive(true);
        }
    }

    IEnumerator SwitchToNextGameObject()
    {
        if (gameObjects == null || gameObjects.Length == 0) yield break; // No GameObjects to switch

        // Deactivate the currently active GameObject
        if (gameObjects[currentIndex] != null)
        {
            gameObjects[currentIndex].SetActive(false);
        }

        // Play the animation
        if (animationObject != null)
        {
            animationObject.SetActive(true); // Activate the animation GameObject
            yield return new WaitForSeconds(animationObject.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length); // Wait for the animation to finish
            animationObject.SetActive(false); // Deactivate the animation GameObject
        }

        // Increment the index and loop back if necessary
        currentIndex = (currentIndex + 1) % gameObjects.Length;

        // Activate the new current GameObject
        ActivateCurrentGameObject();
    }

    IEnumerator SwitchToPreviousGameObject()
    {
        if (gameObjects == null || gameObjects.Length == 0) yield break; // No GameObjects to switch

        // Check if we're at the first GameObject
        if (currentIndex == 0)
        {
            Debug.Log("End"); // Print "End" if there's no previous GameObject
            yield break;
        }

        // Deactivate the currently active GameObject
        if (gameObjects[currentIndex] != null)
        {
            gameObjects[currentIndex].SetActive(false);
        }

        // Play the animation
        if (animationObject != null)
        {
            animationObject.SetActive(true); // Activate the animation GameObject
            yield return new WaitForSeconds(animationObject.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length); // Wait for the animation to finish
            animationObject.SetActive(false); // Deactivate the animation GameObject
        }

        // Decrement the index (do not allow it to go below 0)
        currentIndex--;

        // Activate the new current GameObject
        ActivateCurrentGameObject();
    }
}
