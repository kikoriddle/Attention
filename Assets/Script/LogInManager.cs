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
    public static bool alex1turnOn;

    private string nextSceneName;

    //music
    public AudioClip popupSound;

    // Static booleans for tracking
    public static bool HasLoggedInBefore { get; private set; }

    public GameObject alexGameObject; // The game object to activate and destroy
    private const string AlexDestroyedKey = "AlexGameObjectDestroyed"; // Key for PlayerPrefs

    void Start()
    {
        nextSceneName = "03 Captcha";

        // Reset PlayerPrefs in Unity Editor for testing
#if UNITY_EDITOR
        PlayerPrefs.DeleteKey(AlexDestroyedKey);
#endif

        // Initialize the login scene
        errorText.gameObject.SetActive(false);
        securityPanel.SetActive(false);
        newPassPanel.SetActive(false);

        // Check if alexGameObject has been destroyed before
        bool alexDestroyed = PlayerPrefs.GetInt(AlexDestroyedKey, 0) == 1;

        if (alexDestroyed)
        {
            if (alexGameObject != null)
            {
                Destroy(alexGameObject); // Permanently destroy the object if already destroyed
                Debug.Log("alexGameObject was already destroyed in a previous session.");
            }
        }
        else
        {
            if (alexGameObject != null)
            {
                alexGameObject.SetActive(true); // Activate alexGameObject only the first time
                AudioSource.PlayClipAtPoint(popupSound, Camera.main.transform.position, 0.5f);
                alex1turnOn = true; 
                StartCoroutine(DestroyAlexGameObject());
            }
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

            Debug.Log("Animation is playing");
            // Play the animation
            transitionAnimator.Play("AnimationName"); // Replace "AnimationName" with your animation's name

            // Wait for the animation to complete
            float animationLength = transitionAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength);

            // Turn off the animation GameObject
            //animationObject.SetActive(false);
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
            PlayerPrefs.SetInt(AlexDestroyedKey, 1); // Save this state to PlayerPrefs
            PlayerPrefs.Save();
            Debug.Log("alexGameObject has been destroyed permanently.");
        }
    }
}
