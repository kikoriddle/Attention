using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogInManager : MonoBehaviour
{
    // Objects
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button forgotPasswordButton;
    public TextMeshProUGUI errorText;
    public GameObject loginPanel;
    public Animator transitionAnimator;
    public string Password;
    private const string INITIAL_PASSWORD = "fair41wEd";
    public bool resetPass = false;

    // Call security panel
    public GameObject securityPanel;
    public GameObject newPassPanel;

    private string nextSceneName;

    // Static booleans for tracking
    public static bool HasLoggedInBefore { get; private set; }
    public static bool alexGameObjectDestroyedBefore = false; // Tracks if the Alex game object was destroyed
    public static bool alex1turnOn;

    public GameObject alexGameObject; // The game object to activate and destroy

    void Start()
    {
        nextSceneName = "03 Captcha";

        // Initialize the login scene
        errorText.gameObject.SetActive(false);
        securityPanel.SetActive(false);
        newPassPanel.SetActive(false);
        alex1turnOn = true;

        // Check if alexGameObject has been destroyed before
        alexGameObjectDestroyedBefore = PlayerPrefs.GetInt("AlexGameObjectDestroyedBefore", 0) == 1;

        if (!alexGameObjectDestroyedBefore && alexGameObject != null)
        {
            alexGameObject.SetActive(true);
            StartCoroutine(DestroyAlexGameObject());
        }

        loginButton.onClick.AddListener(HandleLogin);
        forgotPasswordButton.onClick.AddListener(() =>
        {
            loginPanel.SetActive(false);
            securityPanel.SetActive(true);
        });

        // Add input validation
        usernameInput.onValueChanged.AddListener(ValidateInput);
        passwordInput.onValueChanged.AddListener(ValidateInput);

        if (resetPass)
        {
            Debug.Log(resetPass);
        }
        else
        {
            Debug.Log("Get new pass");
            Password = INITIAL_PASSWORD;
        }
    }

    private void ValidateInput(string _)
    {
        // Enable login button only if both fields have text
        bool isValid = !string.IsNullOrEmpty(usernameInput.text) &&
                      !string.IsNullOrEmpty(passwordInput.text);
        loginButton.interactable = isValid;
    }

    void HandleLogin()
    {
        errorText.gameObject.SetActive(false);
        string user_name = usernameInput.text;
        string user_pass = passwordInput.text;

        // Add Login check
        bool loginSuccessful = AuthenticateUser(user_name, user_pass);

        if (loginSuccessful)
        {
            Debug.Log("Login successful!");
            HasLoggedInBefore = true; // Set static boolean to true
            PlayerPrefs.SetInt("HasLoggedInBefore", 1); // Save login state
            PlayerPrefs.Save();

            if (transitionAnimator != null)
            {
                StartCoroutine(PlayAnimationAndSwitchScene());
            }
            else if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError("Next scene name is not assigned!");
            }
        }
        else
        {
            Debug.Log("Cannot login!");
            errorText.gameObject.SetActive(true);
        }
    }

    private bool AuthenticateUser(string user_name, string user_pass)
    {
        // Check if username and password match
        return user_name == "itsnotyuki" && user_pass == Password;
    }

    private void OnDestroy()
    {
        // Clean up listeners
        loginButton.onClick.RemoveAllListeners();
        usernameInput.onValueChanged.RemoveAllListeners();
        passwordInput.onValueChanged.RemoveAllListeners();
    }

    private IEnumerator PlayAnimationAndSwitchScene()
    {
        if (transitionAnimator != null)
        {
            // Enable the GameObject to play the animation
            GameObject animationObject = transitionAnimator.gameObject;
            animationObject.SetActive(true);

            // Play the animation
            transitionAnimator.Play("AnimationName"); // Replace "AnimationName" with your animation's name

            // Wait for the animation to complete
            yield return new WaitForSeconds(1f); // Adjust this duration to match the animation length

            // Turn off the animation GameObject
            animationObject.SetActive(false);
        }

        // Load the next scene
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not assigned!");
        }
    }

    private IEnumerator DestroyAlexGameObject()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        if (alexGameObject != null)
        {
            Destroy(alexGameObject); // Destroy alexGameObject
            alexGameObjectDestroyedBefore = true; // Mark as destroyed
            PlayerPrefs.SetInt("AlexGameObjectDestroyedBefore", 1); // Save this state to PlayerPrefs
            PlayerPrefs.Save();
            Debug.Log("alexGameObject has been destroyed.");
        }
    }
}
