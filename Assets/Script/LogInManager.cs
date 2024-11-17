using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogInManager : MonoBehaviour
{
    //object
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button forgotPasswordButton;
    public TextMeshProUGUI errorText;
    public GameObject loginPanel;

    public string Password;
    private const string INITIAL_PASSWORD = "fair41wEd";
    public bool resetPass = false;

    //call security panel
    public GameObject securityPanel;
    public GameObject newPassPanel;
    // Start is called before the first frame update
    void Start()
    {
        errorText.gameObject.SetActive(false);
        securityPanel.SetActive(false);
        newPassPanel.SetActive(false);

        loginButton.onClick.AddListener(HandleLogin);
        forgotPasswordButton.onClick.AddListener(() => {
            loginPanel.SetActive(false);
            securityPanel.SetActive(true);
           // securityController.ShowSecurityQuestions();
        });

        //add input validation
        usernameInput.onValueChanged.AddListener(ValidateInput);
        passwordInput.onValueChanged.AddListener(ValidateInput);
        if (resetPass)
        { 
            //player has reset the password
            Debug.Log(resetPass);
        }
        else 
        {
            //noReset
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

        //add Login check
        bool loginSuccessful = AuthenticateUser(user_name, user_pass);


        if (loginSuccessful)
        {
            Debug.Log("Login successful!");
            // maybe set things not active?
            //loginPanel.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("cannot login!");
            errorText.gameObject.SetActive(true);
        }

    }
    
    private bool AuthenticateUser(string user_name,string user_pass)
    {
        // if dont match then goes to reset
        return user_name == "itsnotyuki" && user_pass == Password;  
    }

    private void OnDestroy()
    {
        // Clean up listeners
        loginButton.onClick.RemoveAllListeners();
        usernameInput.onValueChanged.RemoveAllListeners();
        passwordInput.onValueChanged.RemoveAllListeners();
    }
}
