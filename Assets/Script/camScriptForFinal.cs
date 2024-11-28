using UnityEngine;
using UnityEngine.SceneManagement; // For scene switching
using System.Collections;

public class CamScriptForFinal : MonoBehaviour
{
    public GameObject[] gameObjects; // Assign the series of GameObjects in the Inspector
    private int currentIndex = 0; // Tracks the currently active GameObject
    public GameObject animationObject; // Assign the GameObject with the animation
    public Camera mainCamera; // Assign the camera in the Inspector
    private float targetZoom;

    public float zoomInSize = 5f;  // Orthographic size for zoomed-in state
    public float zoomOutSize = 10f; // Orthographic size for zoomed-out state
    public float zoomSpeed = 2f;  // Speed of the zoom transition

    private bool isZoomedOut = false; // Tracks the zoom state

    // Two new GameObjects to activate at the second-to-last photo
    public GameObject alexFinal;
    public GameObject ericFinal;

    // Static booleans to track whether these objects have been activated
    public static bool alexFinalActivated = false;
    public static bool ericFinalActivated = false;

    private static bool hasResetPlayerPrefs = false; // Ensures PlayerPrefs reset only once

    // Static boolean to track if the special objects have been turned on
    private static bool finalObjectsActivated = false;

    // New flag to track whether animation is currently playing
    private bool isAnimationPlaying = false;

    void Start()
    {
        // Reset PlayerPrefs only once per game session
        if (!hasResetPlayerPrefs)
        {
            PlayerPrefs.SetInt("AlexFinalActivated", 0);
            PlayerPrefs.SetInt("EricFinalActivated", 0);
            PlayerPrefs.Save();

            Debug.Log("PlayerPrefs reset for the first time.");
            hasResetPlayerPrefs = true; // Mark as reset
        }

        // Load activation states for special objects
        alexFinalActivated = PlayerPrefs.GetInt("AlexFinalActivated", 0) == 1;
        ericFinalActivated = PlayerPrefs.GetInt("EricFinalActivated", 0) == 1;

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

        // Ensure the animation object is assigned
        if (animationObject == null)
        {
            Debug.LogError("Animation GameObject is not assigned!");
            return;
        }

        // Initialize the orthographic size of the camera
        targetZoom = mainCamera.orthographicSize;

        // Activate the first GameObject in the series
        ActivateCurrentGameObject();
    }

    void Update()
    {
        // Prevent input if an animation is playing
        if (isAnimationPlaying) return;

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
            if (currentIndex == gameObjects.Length - 1) // If at the last item in the array
            {
                StartCoroutine(SwitchSceneWithAnimation()); // Trigger scene switch with animation
            }
            else
            {
                StartCoroutine(SmoothTransitionToNextGameObject());
            }
        }

        // Handle switching to the previous GameObject (only if zoomed in)
        if (!isZoomedOut && Input.GetKeyDown(KeyCode.A)) // Use 'A' for previous
        {
            StartCoroutine(SmoothTransitionToPreviousGameObject());
        }

        // Check if player has reached the second-to-last photo and activate special objects
        if (currentIndex == gameObjects.Length - 2 && !finalObjectsActivated) // If at the second-to-last object in the array
        {
            // Activate special objects if they haven't been activated yet
            if (!alexFinalActivated && alexFinal != null)
            {
                alexFinal.SetActive(true);
                alexFinalActivated = true;
                PlayerPrefs.SetInt("AlexFinalActivated", 1);
                PlayerPrefs.Save();
                Debug.Log("Alex Final Activated!");
            }

            if (!ericFinalActivated && ericFinal != null)
            {
                ericFinal.SetActive(true);
                ericFinalActivated = true;
                PlayerPrefs.SetInt("EricFinalActivated", 1);
                PlayerPrefs.Save();
                Debug.Log("Eric Final Activated!");
            }

            // Set the static boolean to true once the special objects are activated
            finalObjectsActivated = true;
            Debug.Log("Final Objects Activated: " + finalObjectsActivated);
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

    // Coroutine to smoothly transition to the next GameObject
    IEnumerator SmoothTransitionToNextGameObject()
    {
        if (gameObjects == null || gameObjects.Length == 0) yield break; // No GameObjects to switch

        // Set flag to prevent input during animation
        isAnimationPlaying = true;

        // Preload the next GameObject while playing the animation
        GameObject nextGameObject = gameObjects[(currentIndex + 1) % gameObjects.Length];

        // Play the animation
        if (animationObject != null)
        {
            animationObject.SetActive(true); // Activate the animation GameObject
            Animator animator = animationObject.GetComponent<Animator>();
            if (animator != null)
            {
                AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
                if (clips.Length > 0)
                {
                    yield return new WaitForSeconds(clips[0].clip.length); // Wait for animation length
                }
            }
            animationObject.SetActive(false); // Deactivate the animation GameObject
        }

        // Deactivate the currently active GameObject
        if (gameObjects[currentIndex] != null)
        {
            gameObjects[currentIndex].SetActive(false);
        }

        // Increment the index and loop back if necessary
        currentIndex = (currentIndex + 1) % gameObjects.Length;

        // Activate the new current GameObject
        ActivateCurrentGameObject();

        // Allow input again
        isAnimationPlaying = false;
    }

    // Coroutine to smoothly transition to the previous GameObject
    IEnumerator SmoothTransitionToPreviousGameObject()
    {
        if (gameObjects == null || gameObjects.Length == 0) yield break; // No GameObjects to switch

        // Set flag to prevent input during animation
        isAnimationPlaying = true;

        // Preload the previous GameObject while playing the animation
        GameObject previousGameObject = gameObjects[(currentIndex - 1 + gameObjects.Length) % gameObjects.Length];

        // Play the animation
        if (animationObject != null)
        {
            animationObject.SetActive(true); // Activate the animation GameObject
            Animator animator = animationObject.GetComponent<Animator>();
            if (animator != null)
            {
                AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
                if (clips.Length > 0)
                {
                    yield return new WaitForSeconds(clips[0].clip.length); // Wait for animation length
                }
            }
            animationObject.SetActive(false); // Deactivate the animation GameObject
        }

        // Deactivate the currently active GameObject
        if (gameObjects[currentIndex] != null)
        {
            gameObjects[currentIndex].SetActive(false);
        }

        // Decrement the index (do not allow it to go below 0)
        currentIndex = (currentIndex - 1 + gameObjects.Length) % gameObjects.Length;

        // Activate the new current GameObject
        ActivateCurrentGameObject();

        // Allow input again
        isAnimationPlaying = false;
    }

    IEnumerator SwitchSceneWithAnimation()
    {
        // Set flag to prevent input during animation
        isAnimationPlaying = true;

        // Play the animation before switching scenes
        if (animationObject != null)
        {
            animationObject.SetActive(true); // Activate the animation GameObject
            Animator animator = animationObject.GetComponent<Animator>();
            if (animator != null)
            {
                AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
                if (clips.Length > 0)
                {
                    yield return new WaitForSeconds(clips[0].clip.length); // Wait for animation length
                }
            }
            animationObject.SetActive(false); // Deactivate the animation GameObject
        }

        // Switch scene (replace "MainPage" with your actual scene name)
        SceneManager.LoadScene("01 Main Window");
    }

    // Static method to check if special objects have been activated
    public static bool AreSpecialObjectsActivated()
    {
        return finalObjectsActivated;
    }
}
