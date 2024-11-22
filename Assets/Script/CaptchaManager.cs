using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CaptcahManager : MonoBehaviour
{
    [SerializeField] private Button[] imageButtons; // Array of 9 image buttons
    [SerializeField] private Button verifyButton;
    private string nextSceneName;
    public Animator transitionAnimator; 
    //public IntroTransfer insPuzzleController;  -> set the variable
    //[SerializeField] private TMPro.TextMeshProUGUI instructionText;

    private bool[] selectedImages = new bool[9];

    void Start()
    {
        nextSceneName = "04 INS";

        // Setup each image button
        for (int i = 0; i < imageButtons.Length; i++)
        {
          //  Debug.Log("printing");

            int index = i; // Capture the index for lambda
            imageButtons[i].onClick.AddListener(() => ToggleImage(index));
        }

        verifyButton.onClick.AddListener(VerifySelection);
    }

    void ToggleImage(int index)
    {
        //debug collider
        Debug.Log("Hitting the button");
        selectedImages[index] = !selectedImages[index];

        // Visual feedback
        Transform checkmark = imageButtons[index].transform.Find("Checkmark");
        if (checkmark != null)
        {
            Debug.Log("Selected checkmark");
            checkmark.gameObject.SetActive(selectedImages[index]);
        }
    }

    void VerifySelection()
    {
        // Add your verification logic here
        Debug.Log("Hitting the verify");
        bool isCorrect = true;
        // Example: check if correct images are selected
        // Modify these indices based on your correct answers
        bool[] correctAnswers = { false, false, true, false, false, true, false, true, false };

        for (int i = 0; i < 9; i++)
        {
            if (selectedImages[i] != correctAnswers[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
{
    Debug.Log("Correct selection!");

    // Check if an animation should play before transitioning
    if (transitionAnimator != null)
    {
        StartCoroutine(PlayAnimationAndSwitchScene());
    }
    else if (!string.IsNullOrEmpty(nextSceneName))
    {
        // Directly load the next scene if no animation is needed
        SceneManager.LoadScene(nextSceneName);
        return; // Exit to prevent further code execution
    }
    else
    {
        Debug.LogError("Next scene name is not assigned!");
        return;
    }
}
else
{
    Debug.Log("Try again!");
    // Reset or retry logic
}
    }

// Coroutine to play animation and switch scene
private IEnumerator PlayAnimationAndSwitchScene()
{
    if (transitionAnimator != null)
    {
        // Enable the GameObject to play the animation
        GameObject animationObject = transitionAnimator.gameObject;
        animationObject.SetActive(true);

        // Play the animation
        transitionAnimator.Play("AnimationName"); // Replace "AnimationName" with your animation's name

        // Wait for the animation to complete
        float animationLength = transitionAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength); // Wait for the animation's duration
    }

    // Load the next scene (leave the animation active until it's done)
    if (!string.IsNullOrEmpty(nextSceneName))
    {
        SceneManager.LoadScene(nextSceneName);
    }
    else
    {
        Debug.LogError("Next scene name is not assigned!");
    }
}

    }
