using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTransfer : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;
    public GameObject object5;
    public GameObject object6;

    public GameObject object7;

    public GameObject transitionObject; // The GameObject used for the transition effect

    private int currentIndex = 0;
    private string nextSceneName = "01 Main Window"; // Name of the next scene

    void Start()
    {
        // Ensure the transition object is initially inactive
        if (transitionObject != null)
        {
            transitionObject.SetActive(false);
        }
    }

    void Update()
    {
        // Check for mouse click, spacebar press, or left shift press
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleObjects();
        }
    }

    void ToggleObjects()
    {
        // Turn off the current object based on the current index
        switch (currentIndex)
        {
            case 0: object1.SetActive(false); break;
            case 1: object2.SetActive(false); break;
            case 2: object3.SetActive(false); break;
            case 3: object4.SetActive(false); break;
            case 4: object5.SetActive(false); break;
            case 5: object6.SetActive(false); break;
            case 6: object7.SetActive(false); break;
        }

        // Move to the next object
        currentIndex = (currentIndex + 1) % 7; // Wrap around after 6 objects

        // Turn on the next object based on the new index
        switch (currentIndex)
        {
            case 0: object1.SetActive(true); break;
            case 1: object2.SetActive(true); break;
            case 2: object3.SetActive(true); break;
            case 3: object4.SetActive(true); break;
            case 4: object5.SetActive(true); break;
            case 5: object6.SetActive(true); break;
            case 6: object7.SetActive(true); break;
        }

        // If it's the last object, start the fade-out animation and then load the scene
        if (currentIndex == 6) // The last object is at index 5
        {
            // Start the fade-out effect using the transition object
            if (transitionObject != null)
            {
                StartCoroutine(PlayAnimationAndSwitchScene());
            }
        }
    }

    // Coroutine for fading out and then switching to the next scene
    IEnumerator PlayAnimationAndSwitchScene()
    {
        // Activate the transition object (turn it on)
        transitionObject.SetActive(true);
        Debug.Log("Transition object turned on.");

        // Wait for the duration of the transition (1 second, adjust as needed)
        Animator animator = transitionObject.GetComponent<Animator>();
        if (animator != null)
        {
            // Wait until the transition animation is finished
            AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(animationState.length);
        }
        else
        {
            // Fallback: wait for a default duration (1 second)
            yield return new WaitForSeconds(1f);
        }

        // Load the new scene asynchronously
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log($"Switching to scene: {nextSceneName}");

            // Load the scene asynchronously but do not immediately activate it
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
            asyncLoad.allowSceneActivation = false; // Prevent the scene from being activated immediately

            // Wait until the scene is ready to be activated
            while (!asyncLoad.isDone)
            {
                // Check if the scene is loaded
                if (asyncLoad.progress >= 0.9f)
                {
                    // When the scene is ready, allow it to be activated
                    asyncLoad.allowSceneActivation = true;
                    Debug.Log("Scene is ready, activating now.");
                }

                yield return null;
            }
        }
        else
        {
            Debug.LogError("Scene name is not assigned!");
        }

        // Deactivate the transition object after the scene load
        transitionObject.SetActive(false);
        Debug.Log("Transition object turned off.");
    }
}
