using UnityEngine;
using UnityEngine.SceneManagement;

public class IconManager : MonoBehaviour
{
    public GameObject animationObject; // GameObject with the animation
    private bool isTransitioning = false; // Prevents multiple transitions

    void OnMouseDown()
    {
        // Use the tag of the GameObject as the scene name
        string sceneName = gameObject.tag; 
        Debug.Log($"Mouse clicked on icon with tag: {sceneName}");

        if (!string.IsNullOrEmpty(sceneName))
        {
            SwitchScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is empty or not assigned!");
        }
    }

    private void SwitchScene(string sceneName)
    {
        if (isTransitioning) return; // Prevent multiple transitions
        isTransitioning = true;

        if (animationObject != null)
        {
            StartCoroutine(PlayAnimationAndSwitchScene(sceneName));
        }
        else
        {
            Debug.LogError("Animation object is not assigned!");
        }
    }

    private System.Collections.IEnumerator PlayAnimationAndSwitchScene(string sceneName)
    {
        if (animationObject != null)
        {
            // Activate the animation object to play the transition animation
            animationObject.SetActive(true);
            Debug.Log("Transition animation started.");

            // Wait for the animation to finish
            Animator animator = animationObject.GetComponent<Animator>();
            if (animator != null)
            {
                AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
                yield return new WaitForSeconds(animationState.length); // Wait for the full animation duration
            }
            else
            {
                // Fallback in case no Animator is found
                yield return new WaitForSeconds(1f); // Default wait time for the transition
            }
        }

        // After the animation is done, load the new scene
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Switching to scene: {sceneName}");
            SceneManager.LoadScene(sceneName);

            // Wait for the scene to load (although SceneManager is async, we can skip this as SceneManager handles it)
            yield return null;

            // After loading, deactivate the animation object to reset for future use
            if (animationObject != null)
            {
                animationObject.SetActive(false);
                Debug.Log("Animation object deactivated after scene load.");
            }
        }
        else
        {
            Debug.LogError("Scene name is not assigned!");
        }

        isTransitioning = false; // Allow future transitions
    }
}
