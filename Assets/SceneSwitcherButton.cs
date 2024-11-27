using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcherButton : MonoBehaviour
{
    public GameObject animationObject; // GameObject with the animation
    public string sceneName = "01 Main Window"; // Default name of the scene to switch to
   

    private bool isTransitioning = false; // Prevents multiple transitions

    void Start()
    {
       
    }

    public void check()
    {
        print("pressed");
    }

    public void SwitchScene()
    {
        if (isTransitioning) return; // Prevent multiple transitions
        isTransitioning = true;
        print("pressed");

        if (animationObject != null)
        {
            StartCoroutine(PlayAnimationAndSwitchScene());
        }
        else
        {
            Debug.LogError("Animation object is not assigned!");
        }
    }

    private System.Collections.IEnumerator PlayAnimationAndSwitchScene()
    {
        if (animationObject != null)
        {
            // Activate the animation object
            animationObject.SetActive(true);

            // Wait for the animation to complete
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
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not assigned!");
        }
    }
}
