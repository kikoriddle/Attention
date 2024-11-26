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

    public string sceneName;

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
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Switching to scene: {sceneName}");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not assigned or empty!");
        }
    }
}
