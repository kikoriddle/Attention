using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BatteryInput : MonoBehaviour
{
    public Button button1; // Assign Button 1 in the Inspector
    public Button button2; // Assign Button 2 in the Inspector
    public Button button3; // Assign Button 3 in the Inspector
    public Button nextSceneButton; // Button for transitioning to the next scene

    public Animator animator1; // Animator for object1
    public Animator animator2; // Animator for object2
    public Animator animator3; // Animator for object3

    public GameObject objectA; // Original GameObject 1
    public GameObject objectB; // Original GameObject 2
    public GameObject objectC; // Original GameObject 3

    public GameObject newObjectA; // New GameObject 1 to turn on
    public GameObject newObjectB; // New GameObject 2 to turn on
    public GameObject newObjectC; // New GameObject 3 to turn on

    public GameObject rewardObject; // The game object to activate when all animations are played
    public GameObject animationObject; // GameObject with fade-out animation for the button

    private bool isTransitioning = false; // Prevent multiple transitions
    private bool isRewardObjectActivated = false; // Tracks if the reward object is activated

    public static bool batteryActivate = false;
    public static bool allAnimationsPlayed = false; // Tracks if all three animations have been played

    private int animationsPlayedCount = 0; // Tracks the number of animations played

    public string sceneName = "Photo Album"; // Scene to switch to for the next scene
    public static bool changePh;

    void Start()
    {
        changePh = false;
        batteryActivate = true;

        // Ensure buttons are initially inactive
        if (button1 != null) button1.gameObject.SetActive(false);
        if (button2 != null) button2.gameObject.SetActive(false);
        if (button3 != null) button3.gameObject.SetActive(false);

        // Ensure reward object and animation object are initially inactive
        if (rewardObject != null) rewardObject.SetActive(false);
        if (animationObject != null) animationObject.SetActive(false);

        // Ensure new objects are initially inactive
        if (newObjectA != null) newObjectA.SetActive(false);
        if (newObjectB != null) newObjectB.SetActive(false);
        if (newObjectC != null) newObjectC.SetActive(false);

        // Re-assign button listeners
        if (button1 != null) button1.onClick.AddListener(() => PlayBatteryAnimation(animator1));
        if (button2 != null) button2.onClick.AddListener(() => PlayBatteryAnimation(animator2));
        if (button3 != null) button3.onClick.AddListener(() => PlayBatteryAnimation(animator3));
        if (nextSceneButton != null) nextSceneButton.onClick.AddListener(SwitchScene);
    }

    void Update()
    {
        // Check if the fullyCollected boolean from the Battery script is true
        if (Battery.fullyCollected)
        {
            ActivateButtons();
        }

        // Activate the reward object if all animations are played
        if (allAnimationsPlayed && !isRewardObjectActivated)
        {
            ActivateRewardObject();
        }

        // Update sprites as long as the reward object is active
        if (rewardObject != null && rewardObject.activeSelf)
        {
            ToggleObjects(); // Handle turning on/off of new and original objects
        }
    }

    private void ActivateButtons()
    {
        if (button1 != null) button1.gameObject.SetActive(true);
        if (button2 != null) button2.gameObject.SetActive(true);
        if (button3 != null) button3.gameObject.SetActive(true);

        Debug.Log("Buttons activated because all animations are collected!");
    }

    private void ActivateRewardObject()
    {
        if (rewardObject != null)
        {
            rewardObject.SetActive(true);
            isRewardObjectActivated = true; // Ensure it only activates once
            Debug.Log("Reward object activated because all animations are played!");
        }
    }

    private void ToggleObjects()
    {
        // Turn off original objects
        if (objectA != null) objectA.SetActive(false);
        if (objectB != null) objectB.SetActive(false);
        if (objectC != null) objectC.SetActive(false);

        // Turn on new objects
        if (newObjectA != null) newObjectA.SetActive(true);
        if (newObjectB != null) newObjectB.SetActive(true);
        if (newObjectC != null) newObjectC.SetActive(true);

        Debug.Log("New objects are now active, and original objects are turned off.");
    }

    public void PlayBatteryAnimation(Animator animator)
    {
        if (animator != null)
        {
            animator.SetBool("canPlay", true); // Trigger the animation
            Debug.Log($"Playing animation for {animator.gameObject.name}");
            StartCoroutine(DestroyBatteryAfterAnimation(animator));
        }
        else
        {
            Debug.LogError("Animator is null!");
        }
    }

    private IEnumerator DestroyBatteryAfterAnimation(Animator animator)
    {
        if (animator != null)
        {
            AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(animationState.length); // Wait for the animation to complete
        }

        animationsPlayedCount++;
        if (animationsPlayedCount >= 3)
        {
            allAnimationsPlayed = true;
            Debug.Log("All animations have been played!");
        }
    }

    // Switch scene with animation
    public void SwitchScene()
    {
        if (isTransitioning) return; // Prevent multiple transitions
        isTransitioning = true;

        // Deactivate all game objects that could cause flashing
        DeactivateCurrentSceneObjects();

        // Activate the fade-out animation for the transition
        if (animationObject != null) animationObject.SetActive(true);

        // Load the next scene asynchronously
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        changePh = true;
        // Start loading the scene in the background
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Don't let the scene activate until the fade-out is complete
        asyncLoad.allowSceneActivation = false;

        // Wait until fade-out is complete (you can adjust the duration of the fade-out here)
        yield return new WaitForSeconds(1f); // Wait for the fade duration

        // Allow the scene to activate now
        asyncLoad.allowSceneActivation = true;

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Deactivate the fade-out animation after the scene has loaded
        if (animationObject != null)
        {
            animationObject.SetActive(false);
        }

        Debug.Log($"Scene {sceneName} loaded and transition complete.");
    }

    private void DeactivateCurrentSceneObjects()
    {
        // Disable all objects in the current scene that may cause a flash
        if (button1 != null) button1.gameObject.SetActive(false);
        if (button2 != null) button2.gameObject.SetActive(false);
        if (button3 != null) button3.gameObject.SetActive(false);
        if (rewardObject != null) rewardObject.SetActive(false);
    }
}
