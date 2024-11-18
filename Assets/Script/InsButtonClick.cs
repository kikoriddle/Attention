using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ActivateGameObject : MonoBehaviour
{
    public GameObject alexMessage; // Assign Alex's message in the Inspector
    public GameObject alexChatBox; // Assign Alex's chat box in the Inspector
    public GameObject ericMessage; // Assign Eric's message in the Inspector
    public GameObject ericChatBox; // Assign Eric's chat box in the Inspector

    public Button alexButton; // Assign Alex's button in the Inspector
    public Button ericButton; // Assign Eric's button in the Inspector

    public RectTransform alexButtonPosition; // Reference to Alex's button position
    public RectTransform ericButtonPosition; // Reference to Eric's button position
    public string sceneName;

    void Start()
    {
        sceneName = "01 Main Window";
        ResetButtonPositions();
    }

    public void ActivateAlexPage()
    {
        DeactivateAllPages(); // Turn off all pages first

        // Activate Alex's message and chat box
        if (alexMessage != null) alexMessage.SetActive(true);
        if (alexChatBox != null) alexChatBox.SetActive(true);

        // Switch button positions
        SwitchButtonPositions(alexButtonPosition, ericButtonPosition);

        Debug.Log("Alex's page has been activated!");
    }

    public void ActivateEricPage()
    {
        DeactivateAllPages(); // Turn off all pages first

        // Activate Eric's message and chat box
        if (ericMessage != null) ericMessage.SetActive(true);
        if (ericChatBox != null) ericChatBox.SetActive(true);

        // Switch button positions
        SwitchButtonPositions(ericButtonPosition, alexButtonPosition);

        Debug.Log("Eric's page has been activated!");
    }

    private void DeactivateAllPages()
    {
        // Deactivate Alex's message and chat box
        if (alexMessage != null) alexMessage.SetActive(false);
        if (alexChatBox != null) alexChatBox.SetActive(false);

        // Deactivate Eric's message and chat box
        if (ericMessage != null) ericMessage.SetActive(false);
        if (ericChatBox != null) ericChatBox.SetActive(false);
    }

    private void ResetButtonPositions()
    {
        // Reset button positions to default
        if (alexButtonPosition != null)
            alexButton.GetComponent<RectTransform>().anchoredPosition = alexButtonPosition.anchoredPosition;

        if (ericButtonPosition != null)
            ericButton.GetComponent<RectTransform>().anchoredPosition = ericButtonPosition.anchoredPosition;
    }

    private void SwitchButtonPositions(RectTransform newPosition, RectTransform oldPosition)
    {
        // Swap positions between the buttons
        if (alexButton != null && ericButton != null)
        {
            Vector2 tempPosition = newPosition.anchoredPosition;
            newPosition.anchoredPosition = oldPosition.anchoredPosition;
            oldPosition.anchoredPosition = tempPosition;
        }
    }
     public void SwitchScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Switching to scene: {sceneName}");
            SceneManager.LoadScene(sceneName); // Load the specified scene
        }
        else
        {
            Debug.LogError("Scene name is not assigned or empty!");
        }
    }
}
