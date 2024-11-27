using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActivateGameObject : MonoBehaviour
{
    public GameObject alexMessage; // Assign Alex's message in the Inspector
    public GameObject alexChatBox; // Assign Alex's chat box in the Inspector
    public GameObject ericMessage; // Assign Eric's message in the Inspector
    public GameObject ericChatBox; // Assign Eric's chat box in the Inspector
    public GameObject bcMessage; // Assign BC's message in the Inspector
    public GameObject bcChatBox; // Assign BC's chat box in the Inspector

    public Button alexButton; // Assign Alex's button in the Inspector
    public Button ericButton; // Assign Eric's button in the Inspector
    public Button bcButton; // Assign BC's button in the Inspector

    private Vector2 topPosition; // Top position
    private Vector2 midPosition; // Middle position
    private Vector2 bottomPosition; // Bottom position

    private RectTransform alexButtonRect;
    private RectTransform ericButtonRect;
    private RectTransform bcButtonRect;

    public GameObject animationObject; // GameObject with animation
    public string sceneName;

    private bool isTransitioning = false;

    void Start()
    {
        sceneName = "01 Main Window";

        // Get RectTransform components
        alexButtonRect = alexButton.GetComponent<RectTransform>();
        ericButtonRect = ericButton.GetComponent<RectTransform>();
        bcButtonRect = bcButton.GetComponent<RectTransform>();

        // Record initial positions
        topPosition = alexButtonRect.anchoredPosition; // Assume Alex starts at the top
        midPosition = ericButtonRect.anchoredPosition; // Eric starts in the middle
        bottomPosition = bcButtonRect.anchoredPosition; // BC starts at the bottom

        // Ensure animation object is inactive initially
        if (animationObject != null)
        {
            animationObject.SetActive(false);
        }

        // Activate Alex's page by default
        ActivateAlexPage();
    }

    public void ActivateAlexPage()
    {
        DeactivateAllPages();

        if (alexMessage != null) alexMessage.SetActive(true);
        if (alexChatBox != null) alexChatBox.SetActive(true);

        // Assign positions explicitly
        SetButtonPositions(alexButtonRect, ericButtonRect, bcButtonRect);

        Debug.Log("Alex's page activated. Button positions updated.");
    }

    public void ActivateEricPage()
    {
        DeactivateAllPages();

        if (ericMessage != null) ericMessage.SetActive(true);
        if (ericChatBox != null) ericChatBox.SetActive(true);

        // Assign positions explicitly
        SetButtonPositions(ericButtonRect, alexButtonRect, bcButtonRect);

        Debug.Log("Eric's page activated. Button positions updated.");
    }

    public void ActivateBCPage()
    {
        DeactivateAllPages();

        if (bcMessage != null) bcMessage.SetActive(true);
        if (bcChatBox != null) bcChatBox.SetActive(true);

        // Assign positions explicitly
        SetButtonPositions(bcButtonRect, alexButtonRect, ericButtonRect);

        Debug.Log("BC's page activated. Button positions updated.");
    }

    private void SetButtonPositions(RectTransform top, RectTransform mid, RectTransform bottom)
    {
        // Move buttons explicitly to their new positions
        top.anchoredPosition = topPosition;
        mid.anchoredPosition = midPosition;
        bottom.anchoredPosition = bottomPosition;
    }

    private void DeactivateAllPages()
    {
        if (alexMessage != null) alexMessage.SetActive(false);
        if (alexChatBox != null) alexChatBox.SetActive(false);

        if (ericMessage != null) ericMessage.SetActive(false);
        if (ericChatBox != null) ericChatBox.SetActive(false);

        if (bcMessage != null) bcMessage.SetActive(false);
        if (bcChatBox != null) bcChatBox.SetActive(false);

        Debug.Log("All pages deactivated.");
    }

    public void SwitchScene()
    {
        if (isTransitioning) return; // Prevent multiple transitions
        isTransitioning = true;

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
            animationObject.SetActive(true); // Activate the animation object
            Debug.Log("Fade-out animation started.");

            // Wait for the animation to complete
            yield return new WaitForSeconds(1f); // Adjust this duration to match your animation
        }

        // Load the new scene
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Switching to scene: {sceneName}");
            SceneManager.LoadScene(sceneName);

            // Wait for the new scene to load
            yield return null;

            // Ensure the animation object is active in the new scene
            GameObject objInNewScene = GameObject.Find(animationObject.name);

            if (objInNewScene != null)
            {
                objInNewScene.SetActive(false);
                Debug.Log("Animation finished and object deactivated in new scene.");
            }
        }
        else
        {
            Debug.LogError("Scene name is not assigned or empty!");
        }
    }
}
