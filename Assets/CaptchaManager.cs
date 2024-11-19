using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CaptcahManager : MonoBehaviour
{
    [SerializeField] private Button[] imageButtons; // Array of 9 image buttons
    [SerializeField] private Button verifyButton;
    private string nextSceneName;
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
            //isSolveAllInsPuzzle = true
            // Add success logic
            // go to the ins main page
            if (!string.IsNullOrEmpty(nextSceneName))
            {
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
}