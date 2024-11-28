using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BatteryInput : MonoBehaviour
{
    public Button button1; // Assign Button 1 in the Inspector
    public Button button2; // Assign Button 2 in the Inspector
    public Button button3; // Assign Button 3 in the Inspector
    public Button nextSceneButton; // Button for transitioning to the next scene

    private GameObject battery1; // First battery object
    private GameObject battery2; // Second battery object
    private GameObject battery3; // Third battery object

    public Animator animator1; // Animator for battery1
    public Animator animator2; // Animator for battery2
    public Animator animator3; // Animator for battery3

    public GameObject rewardObject; // The game object to activate when all animations are played
    public GameObject animationObject; // GameObject with fade-out animation for the button

    private bool isTransitioning = false; // Prevent multiple transitions
    private bool isRewardObjectActivated = false; // Tracks if the reward object is activated

    public static bool batteryActivate = false;
    public static bool allAnimationsPlayed = false; // Tracks if all three animations have been played

    private int animationsPlayedCount = 0; // Tracks the number of animations played

    public string nextSceneName = "Photo Albumn"; // Scene to switch to for the next scene

    void Start()
    {
        batteryActivate = true;

        // Ensure buttons are initially inactive
        if (button1 != null) button1.gameObject.SetActive(false);
        if (button2 != null) button2.gameObject.SetActive(false);
        if (button3 != null) button3.gameObject.SetActive(false);

        // Ensure reward object and animation object are initially inactive
        if (rewardObject != null) rewardObject.SetActive(false);
        if (animationObject != null) animationObject.SetActive(false);

        // Assign click listeners to buttons
        if (button1 != null) button1.onClick.AddListener(PlayBattery1Animation);
        if (button2 != null) button2.onClick.AddListener(PlayBattery2Animation);
        if (button3 != null) button3.onClick.AddListener(PlayBattery3Animation);
        if (nextSceneButton != null) nextSceneButton.onClick.AddListener(() => SwitchScene(nextSceneName));
    }

    void Update()
    {
        battery1 = GameObject.Find("battery1");
        battery2 = GameObject.Find("battery2");
        battery3 = GameObject.Find("battery3");

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
    }

    private void ActivateButtons()
    {
        if (button1 != null) button1.gameObject.SetActive(true);
        if (button2 != null) button2.gameObject.SetActive(true);
        if (button3 != null) button3.gameObject.SetActive(true);

        Debug.Log("Buttons activated because all batteries are collected!");
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

    public void PlayBattery1Animation()
    {
        PlayAndDestroyBattery(battery1, animator1);
    }

    public void PlayBattery2Animation()
    {
        PlayAndDestroyBattery(battery2, animator2);
    }

    public void PlayBattery3Animation()
    {
        PlayAndDestroyBattery(battery3, animator3);
    }

    private void PlayAndDestroyBattery(GameObject battery, Animator animator)
    {
        if (battery != null && animator != null)
        {
            animator.SetBool("canPlay", true); // Trigger the animation
            Debug.Log($"Playing animation for {battery.name}");
            StartCoroutine(DestroyBatteryAfterAnimation(battery, animator));
        }
        else
        {
            Debug.LogError("Battery or Animator is null!");
        }
    }

    private IEnumerator DestroyBatteryAfterAnimation(GameObject battery, Animator animator)
    {
        if (animator != null)
        {
            AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(animationState.length); // Wait for the animation to complete
        }

        if (battery != null)
        {
            Destroy(battery); // Destroy the battery object
            Debug.Log($"{battery.name} destroyed.");
        }

        animationsPlayedCount++;
        if (animationsPlayedCount >= 3)
        {
            allAnimationsPlayed = true;
            Debug.Log("All animations have been played!");
        }
    }

    public void SwitchScene(string sceneName)
    {
        if (isTransitioning) return; // Prevent multiple transitions
        isTransitioning = true;

        if (animationObject != null)
        {
            StartCoroutine(PlayAnimationAndSwitchScene(sceneName));
        }
        else
        {
            Debug.Log($"Switching to scene: {sceneName}");
            SceneManager.LoadScene(sceneName);
        }
    }

    private IEnumerator PlayAnimationAndSwitchScene(string sceneName)
    {
        if (animationObject != null)
        {
            animationObject.SetActive(true); // Activate the animation object
            Debug.Log("Fade-out animation started.");

            yield return new WaitForSeconds(1f); // Adjust this duration to match your animation
        }

        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Switching to scene: {sceneName}");
            SceneManager.LoadScene(sceneName);
        }

        if (animationObject != null)
        {
            animationObject.SetActive(false); // Deactivate the animation object
            Debug.Log("Animation finished and object deactivated after scene load.");
        }
    }
}
