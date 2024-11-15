using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGameObject : MonoBehaviour
{

    public GameObject alexPage; // Assign the GameObject to be activated in the Inspector
    public GameObject youPage;
    public GameObject ericPage;

    public void ActivateAlexPage()
    {
        DeactivateAllPages(); // Turn off all pages first
        if (alexPage != null)
        {
            alexPage.SetActive(true); // Activate Alex's page
            Debug.Log($"{alexPage.name} has been activated!");
        }
        else
        {
            Debug.LogError("No object assigned to activate for Alex's page!");
        }
    }

    public void ActivateYourPage()
    {
        DeactivateAllPages(); // Turn off all pages first
        if (youPage != null)
        {
            youPage.SetActive(true); // Activate Your page
            Debug.Log($"{youPage.name} has been activated!");
        }
        else
        {
            Debug.LogError("No object assigned to activate for Your page!");
        }
    }

    public void ActivateEricPage()
    {
        DeactivateAllPages(); // Turn off all pages first
        if (ericPage != null)
        {
            ericPage.SetActive(true); // Activate Eric's page
            Debug.Log($"{ericPage.name} has been activated!");
        }
        else
        {
            Debug.LogError("No object assigned to activate for Eric's page!");
        }
    }

    private void DeactivateAllPages()
    {
        if (alexPage != null) alexPage.SetActive(false);
        if (youPage != null) youPage.SetActive(false);
        if (ericPage != null) ericPage.SetActive(false);
    }
}

