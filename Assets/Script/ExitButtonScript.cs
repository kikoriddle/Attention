using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonScript : MonoBehaviour
{
    public string sceneName; // Scene name to switch to
    public GameObject animationObject; // GameObject with fade-out animation
    private bool isTransitioning = false; // Prevent multiple transitions

    void Start()
    {
        // Assign the scene name here, keeping your original structure
        sceneName = "01 Main Window";

        if (animationObject != null)
        {
            animationObject.SetActive(false); // Ensure the animation object is inactive initially
        }
    }

    public void SwitchScene()
    {
        if (isTransitioning) return; // Prevent multiple transitions
        isTransitioning = true;

        if (animationObject != null)
        {
            StartCoroutine(PlayAnimationAndSwitchScene());
        }
        else
        {
            Debug.Log($"Switching to scene: {sceneName}");
            SceneManager.LoadScene(sceneName);
        }
    }

    private IEnumerator PlayAnimationAndSwitchScene()
    {
        if (animationObject != null)
        {
            animationObject.SetActive(true); // Activate the animation object
            Debug.Log("Fade-out animation started.");

            // Wait for the animation to complete
            yield return new WaitForSeconds(1f); // Adjust this duration to match your animation
        }

        // Load the new scene while keeping the animation object active
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Switching to scene: {sceneName}");
            SceneManager.LoadScene(sceneName);

            // Wait for the scene to fully load
            yield return null;

            // Ensure the animation object is turned off after the new scene is loaded
            if (animationObject != null)
            {
                animationObject.SetActive(false);
                Debug.Log("Animation finished and object deactivated after scene load.");
            }
        }
        else
        {
            Debug.LogError("Scene name is not assigned or empty!");
        }
    }
}
