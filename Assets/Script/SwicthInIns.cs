using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;  // Add this line

public class SwitchInIns : MonoBehaviour
{
    public GameObject alexPage; // Assign Alex's page in the Inspector
    public GameObject ericPage; // Assign Eric's page in the Inspector
    public GameObject youPage; // Assign Your page in the Inspector
    public GameObject gPage; // Assign G's page in the Inspector
    public GameObject battery;
    public GameObject b1;
    public GameObject b2;
    public GameObject b3;

    public GameObject animationObject; // GameObject with animation
    public string sceneName; // Name of the scene to switch to

    private bool isTransitioning = false;

    void Start()
    {
        sceneName = "01 Main Window";  // Set the default scene

        // Ensure animation object is inactive initially
        if (animationObject != null)
        {
            animationObject.SetActive(false);
        }

        // Reset battery-related items on start
        ResetBatteryItems();
    }

    void Update()
    {
        if (BatteryInput.batteryActivate)
        {
            battery.SetActive(true);
            b1.SetActive(true);
            b2.SetActive(true);
            b3.SetActive(true);
        }
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

    public void ActivateGPage()
    {
        DeactivateAllPages(); // Turn off all pages first

        // Activate G's page
        if (gPage != null) gPage.SetActive(true);

        Debug.Log("G's page has been activated!");
    }

    private void DeactivateAllPages()
    {
        // Deactivate Alex's page
        if (alexPage != null) alexPage.SetActive(false);

        // Deactivate Eric's page
        if (ericPage != null) ericPage.SetActive(false);

        // Deactivate Your page
        if (youPage != null) youPage.SetActive(false);

        // Deactivate G's page
        if (gPage != null) gPage.SetActive(false);
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

    private IEnumerator PlayAnimationAndSwitchScene()
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

            // Ensure the animation object is deactivated in the new scene
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

    private void ResetBatteryItems()
    {
        battery.SetActive(false);
        b1.SetActive(false);
        b2.SetActive(false);
        b3.SetActive(false);
    }
}
