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

    private bool[] selectedImages = new bool[9];
    public AudioClip popupSound;

    // New variables for `ericm1` functionality
    public GameObject ericGameObject; // GameObject to activate
    public static bool ericm1 = false; // Static boolean to track activation state

    void Start()
    {
        nextSceneName = "04 INS";

        // Check and activate ericGameObject if not already activated
        if (!ericm1 && ericGameObject != null)
        {
            ericGameObject.SetActive(true);
            AudioSource.PlayClipAtPoint(popupSound, Camera.main.transform.position, 0.5f);
            StartCoroutine(DeactivateEricGameObjectAfterTime());
        }

        // Setup each image button
        for (int i = 0; i < imageButtons.Length; i++)
        {
            int index = i; // Capture the index for lambda
            imageButtons[i].onClick.AddListener(() => ToggleImage(index));
        }

        verifyButton.onClick.AddListener(VerifySelection);
    }

    void ToggleImage(int index)
    {
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
        Debug.Log("Hitting the verify");
        bool isCorrect = true;

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

            if (transitionAnimator != null)
            {
                StartCoroutine(PlayAnimationAndSwitchScene());
            }
            else if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
                return; 
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
        }
    }

    // Coroutine to play animation and switch scene
    private IEnumerator PlayAnimationAndSwitchScene()
    {
        if (transitionAnimator != null)
        {
            GameObject animationObject = transitionAnimator.gameObject;
            animationObject.SetActive(true);

            transitionAnimator.Play("AnimationName");

            float animationLength = transitionAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength); 
        }

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not assigned!");
        }
    }

    // Coroutine to deactivate the Eric GameObject
    private IEnumerator DeactivateEricGameObjectAfterTime()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds

        if (ericGameObject != null)
        {
            ericGameObject.SetActive(false); // Deactivate the GameObject
            ericm1 = true; // Mark as activated to prevent future activations
            Debug.Log("ericGameObject has been deactivated and ericm1 set to true.");
        }
    }
}
