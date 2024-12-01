using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuStart : MonoBehaviour
{
    public GameObject startButton;
    public GameObject transitionObject; // The GameObject used for the transition effect

    private string nextSceneName = "01 Main Window"; // Name of the next scene

    void Start()
    {
        // Ensure the transition object is initially inactive
        if (transitionObject != null)
        {
            transitionObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        ToggleObjects();
    }

    void ToggleObjects()
    {
        if (startButton != null)
        {
            Debug.Log("Button Clicked");
        }
        StartCoroutine(PlayAnimationAndSwitchScene());
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
