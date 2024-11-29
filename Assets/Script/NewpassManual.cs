using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewpassManual : MonoBehaviour
{
    public TMP_InputField newPasswordInput;
    public TMP_InputField confirmPasswordInput;
    public Button submitButton;
    public TextMeshProUGUI errorMessageText;
    public GameObject passwordResetPanel;
    public GameObject LoginManual;

    public LogInManager LM;

    [Header("Password Requirements")]
    private const int MIN_LENGTH = 8;
    private const int MIN_NUMBERS = 1;
    //private const int MIN_UPPERCASE = 1;
    private const int MIN_SPECIAL_CHARS = 1;
    private readonly string specialCharacters = "!@#$%^&*()_+-=[]{}|;:,.<>?";

    void Start()
    {
        errorMessageText.gameObject.SetActive(false);
        submitButton.interactable = false;

        newPasswordInput.onValueChanged.AddListener(_ => ValidateInputs());
        confirmPasswordInput.onValueChanged.AddListener(_ => ValidateInputs());
        submitButton.onClick.AddListener(HandlePasswordSubmit);


    }

    private void ValidateInputs()
    {
        string newPass = newPasswordInput.text;
        string confirmPass = confirmPasswordInput.text;

        bool isValid = true;
        string errorMessage = "";

        if (!string.IsNullOrEmpty(newPass))
        {
            var (passValid, passError) = ValidatePassword(newPass);
            if (!passValid)
            {
                isValid = false;
                errorMessage = passError;
            }
            else if (!string.IsNullOrEmpty(confirmPass) && newPass != confirmPass)
            {
                isValid = false;
                errorMessage = "Passwords do not match";
            }
        }

        errorMessageText.gameObject.SetActive(!isValid && !string.IsNullOrEmpty(errorMessage));
        errorMessageText.text = errorMessage;

        submitButton.interactable = isValid &&
                                  !string.IsNullOrEmpty(newPass) &&
                                  !string.IsNullOrEmpty(confirmPass) &&
                                  newPass == confirmPass;
    }

    private (bool isValid, string errorMessage) ValidatePassword(string password)
    {
        if (password.Length < MIN_LENGTH)
            return (false, $"Password must be at least {MIN_LENGTH} characters long");

        int numberCount = 0;
        foreach (char c in password)
        {
            if (char.IsDigit(c))
                numberCount++;
        }
        if (numberCount < MIN_NUMBERS)
            return (false, "Password must contain at least one number");

        // Count uppercase
        int uppercaseCount = 0;
        foreach (char c in password)
        {
            if (char.IsUpper(c))
                uppercaseCount++;
        }
        // Count special characters
        int specialCharCount = 0;
        foreach (char c in password)
        {
            if (specialCharacters.Contains(c))
                specialCharCount++;
        }
        if (specialCharCount < MIN_SPECIAL_CHARS)
            return (false, "Password must contain at least one special character");

        return (true, string.Empty);
    }


    private void HandlePasswordSubmit()
    {
        string newPassword = newPasswordInput.text;

        // Final validation
        var (isValid, errorMessage) = ValidatePassword(newPassword);
        if (!isValid)
        {
            ShowError(errorMessage);
            return;
        }

        if (newPassword != confirmPasswordInput.text)
        {
            ShowError("Passwords do not match");
            return;
        }


        // set new password
        LM.resetPass = true;
        LM.Password = newPassword;
        Debug.Log($"Password updated to: {LM.Password}");

        //clear input and swtich
        ClearInputs();
        passwordResetPanel.SetActive(false);
        LoginManual.SetActive(true);
    }

    //show error
    private void ShowError(string message)
    {
        errorMessageText.text = message;
        errorMessageText.gameObject.SetActive(true);
    }
    //clear input after fail 
    public void ClearInputs()
    {
        newPasswordInput.text = "";
        confirmPasswordInput.text = "";
        errorMessageText.gameObject.SetActive(false);
        submitButton.interactable = false;
    }

    //destroy

    private void OnDestroy()
    {
        // Clean up listeners
        if (newPasswordInput != null)
            newPasswordInput.onValueChanged.RemoveAllListeners();
        if (confirmPasswordInput != null)
            confirmPasswordInput.onValueChanged.RemoveAllListeners();
        if (submitButton != null)
            submitButton.onClick.RemoveAllListeners();
    }


}
