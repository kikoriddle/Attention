using UnityEngine;
using UnityEngine.UI;

public class CaptcahManager : MonoBehaviour
{
    [SerializeField] private Button[] imageButtons; // Array of 9 image buttons
    [SerializeField] private Button verifyButton;
    [SerializeField] private TMPro.TextMeshProUGUI instructionText;

    private bool[] selectedImages = new bool[9];

    void Start()
    {
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
        bool isCorrect = true;
        // Example: check if correct images are selected
        // Modify these indices based on your correct answers
        bool[] correctAnswers = { true, false, true, false, true, false, true, false, true };

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
            // Add success logic
        }
        else
        {
            Debug.Log("Try again!");
            // Reset or retry logic
        }
    }
}