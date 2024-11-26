using UnityEngine;
using UnityEngine.UI;

public class ScrollTrackerMultiple : MonoBehaviour
{
    public ScrollRect scrollRect1; // Reference to the first ScrollRect
    public ScrollRect scrollRect2; // Reference to the second ScrollRect
    public ScrollRect scrollRect3; // Reference to the third ScrollRect

    public GameObject message2; // The GameObject to activate when two scrolls reach the end

    private bool isScroll1AtEnd = false; // Tracks if ScrollRect1 is at the bottom
    private bool isScroll2AtEnd = false; // Tracks if ScrollRect2 is at the bottom
    private bool isScroll3AtEnd = false; // Tracks if ScrollRect3 is at the bottom

    void Start()
    {
        if (message2 != null)
        {
            message2.SetActive(false); // Ensure the message is hidden initially
        }
    }

    void Update()
    {
        // Check the end state for each scroll view
        isScroll1AtEnd = CheckScrollAtEnd(scrollRect1);
        isScroll2AtEnd = CheckScrollAtEnd(scrollRect2);
        isScroll3AtEnd = CheckScrollAtEnd(scrollRect3);

        // If at least two scrolls are at the end, activate the message
        if (message2 != null && (IsAtLeastTwoTrue()))
        {
            message2.SetActive(true);
        }
        else if (message2 != null)
        {
            message2.SetActive(false);
        }
    }

    private bool CheckScrollAtEnd(ScrollRect scrollRect)
    {
        if (scrollRect == null) return false;

        // Return true if the scroll is at the bottom
        return scrollRect.verticalNormalizedPosition <= 0.01f; // Adjust threshold if needed
    }

    private bool IsAtLeastTwoTrue()
    {
        int trueCount = 0;

        if (isScroll1AtEnd) trueCount++;
        if (isScroll2AtEnd) trueCount++;
        if (isScroll3AtEnd) trueCount++;

        return trueCount >= 2;
    }
}
