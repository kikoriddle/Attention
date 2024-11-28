using UnityEngine;
using UnityEngine.UI;

public class ScrollTracker : MonoBehaviour
{
    public ScrollRect scrollRect1; // Reference to the first ScrollRect
    public ScrollRect scrollRect2; // Reference to the second ScrollRect

    public GameObject message1; // GameObject to activate when the first scroll reaches the end
    public GameObject message2; // GameObject to activate when the second scroll reaches the end

    // Static booleans to track activation
    public static bool IsMessage1Activated = false;
    public static bool IsMessage2Activated = false;

    private static bool hasResetPlayerPrefs = false; // Ensures PlayerPrefs reset only once

    void Start()
    {
        // Reset PlayerPrefs only once per game session
        if (!hasResetPlayerPrefs)
        {
            PlayerPrefs.SetInt("Message1Activated", 0);
            PlayerPrefs.SetInt("Message2Activated", 0);
            PlayerPrefs.Save();

            Debug.Log("PlayerPrefs reset for the first time.");
            hasResetPlayerPrefs = true; // Mark as reset
        }

        // Load activation states from PlayerPrefs
        IsMessage1Activated = PlayerPrefs.GetInt("Message1Activated", 0) == 1;
        IsMessage2Activated = PlayerPrefs.GetInt("Message2Activated", 0) == 1;

        Debug.Log($"IsMessage1Activated: {IsMessage1Activated}");
        Debug.Log($"IsMessage2Activated: {IsMessage2Activated}");

        // Ensure messages are inactive if already activated
        if (IsMessage1Activated && message1 != null) message1.SetActive(false);
        if (IsMessage2Activated && message2 != null) message2.SetActive(false);
    }

    void Update()
    {
        // Check if scrollRect1 has reached the bottom and activate message1
        if (!IsMessage1Activated && CheckScrollAtEnd(scrollRect1))
        {
            ActivateMessage(message1, "Message1Activated", ref IsMessage1Activated);
        }

        // Check if scrollRect2 has reached the bottom and activate message2
        if (!IsMessage2Activated && CheckScrollAtEnd(scrollRect2))
        {
            ActivateMessage(message2, "Message2Activated", ref IsMessage2Activated);
        }
    }

    private bool CheckScrollAtEnd(ScrollRect scrollRect)
    {
        if (scrollRect == null) return false;

        // Return true if the scroll is at the bottom
        return scrollRect.verticalNormalizedPosition <= 0.01f; // Adjust threshold if needed
    }

    private void ActivateMessage(GameObject message, string prefsKey, ref bool activationFlag)
    {
        if (message != null)
        {
            message.SetActive(true); // Activate the message
            activationFlag = true; // Mark as activated
            PlayerPrefs.SetInt(prefsKey, 1); // Save the activation state
            PlayerPrefs.Save(); // Persist the changes
            Debug.Log($"{message.name} has been activated.");
        }
    }
}
