using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SecurityManager : MonoBehaviour
{
    public GameObject securityQuestionsPanel;
    public TMP_Dropdown birthdayDropdown;
    public TMP_Dropdown animalDropdown;
    public TMP_Dropdown colorDropdown;
    public Button verifyButton;
    //Button backButton;
    //do i need a go back button
    public TextMeshProUGUI errorText;
    public GameObject NewPasswordPanel;

    // References to other controllers
    // [SerializeField] private LoginManager loginController;
    // [SerializeField] private NewPasswordController newPasswordController;

    void Start()
    {
        errorText.gameObject.SetActive(false);

        //check button 
        verifyButton.onClick.AddListener(ValidateAnswers);
    }

    private void ValidateAnswers()
    {
        if (birthdayDropdown.value == 0 ||
          animalDropdown.value == 0 ||
          colorDropdown.value == 0)
        {
            ShowError("Please answer all security questions");
            return;
        }


        string selectedBirthday = birthdayDropdown.options[birthdayDropdown.value].text;
        string selectedAnimal = animalDropdown.options[animalDropdown.value].text;
        string selectedColor = colorDropdown.options[colorDropdown.value].text;

        bool isValid = (selectedBirthday == "7.10") &&
               selectedAnimal == "Cat" &&
               selectedColor == "Pink";

        if (isValid)
        {
            securityQuestionsPanel.SetActive(false);
            NewPasswordPanel.SetActive(true);
            //newPasswordController.ShowNewPasswordPanel();
            ResetDropdowns();
        }
        else
        {
            ShowError("One or more answers are incorrect");
        }
    }


    private void ShowError(string message)
    {
        errorText.gameObject.SetActive(true);
        errorText.text = message;
    }

    private void ResetDropdowns()
    {
        birthdayDropdown.value = 0;
        animalDropdown.value = 0;
        colorDropdown.value = 0;
        errorText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        verifyButton.onClick.RemoveAllListeners();
       // backButton.onClick.RemoveAllListeners();
    }
}