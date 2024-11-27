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
            // Activate the animation object and play the animation
            animationObject.SetActive(true);
            Debug.Log("Fade-out animation started.");

            Animator animator = animationObject.GetComponent<Animator>();
            if (animator != null)
            {
                AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
                yield return new WaitForSeconds(animationState.length);
            }
            else
            {
                yield return new WaitForSeconds(1f); // Fallback duration
            }
        }

        // Load the new scene
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Switching to scene: {sceneName}");
            SceneManager.LoadScene(sceneName);

            // Wait for the new scene to load
            yield return null;

            // Ensure the animation object is deactivated after the new scene loads
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
