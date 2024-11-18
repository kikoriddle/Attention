using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwicthInIns : MonoBehaviour
{
   
    public GameObject alexPage; // Assign Alex's page in the Inspector
    public GameObject ericPage; // Assign Eric's page in the Inspector
    public GameObject youPage; // Assign Your page in the Inspector

    public string sceneName; // Name of the scene to switch to

    void Start()
    {
        sceneName = "01 Main Window";
    }

    public void ActivateAlexPage()
    {
        DeactivateAllPages(); // Turn off all pages first

        // Activate Alex's page
        if (alexPage != null) alexPage.SetActive(true);

        Debug.Log("Alex's page has been activated!");
    }

    public void ActivateEricPage()
    {
        DeactivateAllPages(); // Turn off all pages first

        // Activate Eric's page
        if (ericPage != null) ericPage.SetActive(true);

        Debug.Log("Eric's page has been activated!");
    }

    public void ActivateYouPage()
    {
        DeactivateAllPages(); // Turn off all pages first

        // Activate Your page
        if (youPage != null) youPage.SetActive(true);

        Debug.Log("Your page has been activated!");
    }

    private void DeactivateAllPages()
    {
        // Deactivate Alex's page
        if (alexPage != null) alexPage.SetActive(false);

        // Deactivate Eric's page
        if (ericPage != null) ericPage.SetActive(false);

        // Deactivate Your page
        if (youPage != null) youPage.SetActive(false);
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

    