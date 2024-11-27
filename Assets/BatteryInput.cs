using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryInput : MonoBehaviour
{
    public Button button1; // Assign Button 1 in the Inspector
    public Button button2; // Assign Button 2 in the Inspector
    public Button button3; // Assign Button 3 in the Inspector

    private GameObject battery1; // First battery object from another scene
    private GameObject battery2; // Second battery object from another scene
    private GameObject battery3; // Third battery object from another scene

    private Animator animator1; // Animator for battery1
    private Animator animator2; // Animator for battery2
    private Animator animator3; // Animator for battery3

    public static bool batteryActivate = false;

    void Start()
    {
        batteryActivate = true; 
        // Find the battery objects by name in the current scene
        battery1 = GameObject.Find("battery1");
        battery2 = GameObject.Find("battery2");
        battery3 = GameObject.Find("battery3");

        // Ensure the batteries were found and assign their animators
        if (battery1 != null) animator1 = battery1.GetComponent<Animator>();
        if (battery2 != null) animator2 = battery2.GetComponent<Animator>();
        if (battery3 != null) animator3 = battery3.GetComponent<Animator>();

        // Ensure buttons are initially inactive
        if (button1 != null) button1.gameObject.SetActive(false);
        if (button2 != null) button2.gameObject.SetActive(false);
        if (button3 != null) button3.gameObject.SetActive(false);

        // Assign click listeners to buttons
        if (button1 != null) button1.onClick.AddListener(() => HandleButtonClick(battery1, animator1));
        if (button2 != null) button2.onClick.AddListener(() => HandleButtonClick(battery2, animator2));
        if (button3 != null) button3.onClick.AddListener(() => HandleButtonClick(battery3, animator3));
    }

    void Update()
    {
        // Check if the fullyCollected boolean from the Battery script is true
        if (Battery.fullyCollected)
        {
            ActivateButtons();
        }
    }

    private void ActivateButtons()
    {
        // Activate the buttons
        if (button1 != null) button1.gameObject.SetActive(true);
        if (button2 != null) button2.gameObject.SetActive(true);
        if (button3 != null) button3.gameObject.SetActive(true);

        Debug.Log("Buttons activated because all batteries are collected!");
    }

    private void HandleButtonClick(GameObject battery, Animator animator)
    {
        if (battery != null)
        {
            // Play the animation
            if (animator != null)
            {
                animator.SetTrigger("PlayDestroyAnimation");
                Debug.Log($"Playing destroy animation for {battery.name}");
            }

            // Start coroutine to destroy the battery
            StartCoroutine(DestroyBatteryAfterAnimation(battery, animator));
        }
        else
        {
            Debug.LogError("Battery object is null!");
        }
    }

    private IEnumerator DestroyBatteryAfterAnimation(GameObject battery, Animator animator)
    {
        if (animator != null)
        {
            // Wait for the animation to complete
            AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(animationState.length);
        }

        // Destroy the battery object
        Destroy(battery);
        Debug.Log($"{battery.name} destroyed.");
    }
}
