using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Import UI namespace for buttons
using System.Collections;

public class FinalMessageScript : MonoBehaviour
{
    // Four GameObjects for Alex and Eric (two for each)
    public GameObject alexObjectY;
    public GameObject alexObjectB;
    public GameObject ericObjectY;
    public GameObject ericObjectB;

    // Buttons for the UI
    public Button alexButtonY;
    public Button alexButtonB;
    public Button ericButtonY;
    public Button ericButtonB;

    // Flags to track if the animations have been played
    private bool alexYPlayed = false;
    private bool alexBPlayed = false;
    private bool ericYPlayed = false;
    private bool ericBPlayed = false;

    // Transition GameObject for fade animation (e.g., a UI Panel or Image)
    public GameObject transitionObject;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure all game objects are initially inactive
        alexObjectY.SetActive(false);
        alexObjectB.SetActive(false);
        ericObjectY.SetActive(false);
        ericObjectB.SetActive(false);

        // Ensure the transition object is initially inactive
        transitionObject.SetActive(false);

        // Add listeners for button clicks
        alexButtonY.onClick.AddListener(ActivateAlexObjectY);
        alexButtonB.onClick.AddListener(ActivateAlexObjectB);
        ericButtonY.onClick.AddListener(ActivateEricObjectY);
        ericButtonB.onClick.AddListener(ActivateEricObjectB);
    }
    void Update()
    {
        // Check if the "R" key is pressed to trigger the transition animation
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(PlayTransitionAnimation());
        }
    }

    // Method to activate Alex's Y object and play its animation
    void ActivateAlexObjectY()
    {
        alexObjectY.SetActive(true);  // Activate the Alex Y object
        StartCoroutine(PlayAnimation(alexObjectY, "AlexY"));
    }

    // Method to activate Alex's B object and play its animation
    void ActivateAlexObjectB()
    {
        alexObjectB.SetActive(true);  // Activate the Alex B object
        StartCoroutine(PlayAnimation(alexObjectB, "AlexB"));
    }

    // Method to activate Eric's Y object and play its animation
    void ActivateEricObjectY()
    {
        ericObjectY.SetActive(true);  // Activate the Eric Y object
        StartCoroutine(PlayAnimation(ericObjectY, "EricY"));
    }

    // Method to activate Eric's B object and play its animation
    void ActivateEricObjectB()
    {
        ericObjectB.SetActive(true);  // Activate the Eric B object
        StartCoroutine(PlayAnimation(ericObjectB, "EricB"));
    }

    // Coroutine to play the animation of the object
    IEnumerator PlayAnimation(GameObject obj, string animationType)
    {
        // Get the Animator component attached to the object
        Animator animator = obj.GetComponent<Animator>();
        if (animator != null)
        {
            // Play the animation (assuming each object has a single animation)
            animator.SetTrigger("Play");  // Assuming "Play" is the trigger for the animation

            // Wait for the animation to finish
            AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
            if (clips.Length > 0)
            {
                yield return new WaitForSeconds(clips[0].clip.length);
            }
        }

        // Update the flags based on which object was activated
        if (animationType == "AlexY")
        {
            alexYPlayed = true;
        }
        else if (animationType == "AlexB")
        {
            alexBPlayed = true;
        }
        else if (animationType == "EricY")
        {
            ericYPlayed = true;
        }
        else if (animationType == "EricB")
        {
            ericBPlayed = true;
        }

        // Check if all animations have been played
        CheckAndSwitchScene();
    }

    // Method to check if all animations have been completed and then switch the scene
    void CheckAndSwitchScene()
    {
        // If all animations are played, proceed to the next scene
        if (alexYPlayed && alexBPlayed && ericYPlayed && ericBPlayed)
        {
            StartCoroutine(PlayTransitionAnimation());
        }
    }

    // Coroutine to play transition animation before switching scene
    IEnumerator PlayTransitionAnimation()
    {
        // Activate the transition object (fade-in/out animation, for example)
        transitionObject.SetActive(true);

        // Assuming transitionObject has an Animator component with a "FadeOut" trigger to fade out
        Animator transitionAnimator = transitionObject.GetComponent<Animator>();
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("FadeOut");

            // Wait for the fade-out animation to finish
            AnimatorClipInfo[] clips = transitionAnimator.GetCurrentAnimatorClipInfo(0);
            if (clips.Length > 0)
            {
                yield return new WaitForSeconds(clips[0].clip.length);
            }
        }

        // After the transition, load the next scene
        SceneManager.LoadScene("EndScene");  // Replace "NextSceneName" with your actual scene name
    }
}