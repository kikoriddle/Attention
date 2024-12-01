using System.Collections;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public static int totalClickCount = 0;
    public static bool fullyCollected = false; // Tracks if all batteries are collected

    private bool isClicked = false; // Ensures the object is clicked only once
    public Animator animator;
    public static Animator GlobalAnimator;  // Reference to an external Animator component
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        // Ensure the GameObject has a 2D Collider
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError($"GameObject {gameObject.name} does not have a 2D Collider! Please add one.");
        }

        // Ensure the animator is assigned in the Inspector
        if (GlobalAnimator == null)
        {
          GlobalAnimator = animator;
          Debug.Log("Animator stored globally.");
        }
        else
        {
           animator = GlobalAnimator; // Reassign the Animator if it already exists
        }
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure the SpriteRenderer component is attached
        if (spriteRenderer == null)
        {
            Debug.LogError($"GameObject {gameObject.name} does not have a SpriteRenderer component! Please add one.");
        }
    }

    void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true; // Mark as clicked
            totalClickCount++; // Increment the shared click count

            Debug.Log($"GameObject {gameObject.name} clicked! Total clicks: {totalClickCount}");

            // Trigger the animation by setting the playB parameter to true
            if (animator != null)
            {
                animator.SetBool("playB", true); // Set the external Animator's playB parameter
                Debug.Log("Triggered animation on external Animator.");
            }

            // Start coroutine to turn off the sprite renderer after the animation plays
            StartCoroutine(DeactivateAfterAnimation());

            // Check if all batteries are collected
            if (totalClickCount >= 3) // Adjust this number based on the total number of batteries
            {
                fullyCollected = true;
                Debug.Log("All batteries have been collected!");
            }
        }
    }

    private IEnumerator DeactivateAfterAnimation()
    {
        // Wait for the animation to finish playing
        if (animator != null)
        {
            AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(animationState.length);
        }

        // Disable the SpriteRenderer component (make the sprite invisible)
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;  // Hide the sprite by disabling the renderer
            Debug.Log($"SpriteRenderer for {gameObject.name} disabled.");
        }
    }
}
