using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public static int totalClickCount = 0;

    private bool isClicked = false; // Ensures the object is clicked only once

    public Animator animator; // Reference to an external Animator component

    void Start()
    {
        // Ensure the GameObject has a 2D Collider
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError($"GameObject {gameObject.name} does not have a 2D Collider! Please add one.");
        }

        // Ensure the animator is assigned in the Inspector
        if (animator == null)
        {
            Debug.LogError("Animator is not assigned! Please attach an external Animator in the Inspector.");
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

            // Start coroutine to turn off the GameObject after the animation plays
            StartCoroutine(DeactivateAfterAnimation());
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

        // Turn off the GameObject
        gameObject.SetActive(false);
    }
}
