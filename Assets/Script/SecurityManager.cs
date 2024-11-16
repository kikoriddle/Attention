using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SecurityManager : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject securityQuestionsPanel;
    public TMP_Dropdown birthdayDropdown;
    public TMP_Dropdown animalDropdown;
    public TMP_Dropdown colorDropdown;
    public Button verifyButton;
    public TextMeshProUGUI errorText;
    public GameObject NewPasswordPanel;

    // Cache correct answers and indexes for faster comparison
    private readonly string correctBirthday = "7.10";
    private readonly string correctAnimal = "Cat";
    private readonly string correctColor = "Pink";

    // Cache dropdown values
    private int birthdayIndex;
    private int animalIndex;
    private int colorIndex;

    private float clickTime;

 

    private void Awake()
    {
        // Initialize in Awake for faster startup
        SetupUI();
    }

    private void SetupUI()
    {
        errorText.gameObject.SetActive(false);

        // Set up button without lambda for better performance
        verifyButton.onClick.AddListener(OnVerifyButtonClick);

        // Initialize dropdowns immediately
        SetupDropdowns();
    }

    private void SetupDropdowns()
    {
        // Clear and setup lists once
        var birthdays = new List<string>(6) { "Select Birthday", "7.09", "7.10", "8.09", "8.10", "8.11" };
        var animals = new List<string>(6) { "Select Animal", "Fish", "Dog", "Bird", "Cat", "Rabbit" };
        var colors = new List<string>(6) { "Select Color", "Black", "Blue", "Pink", "Green", "Purple" };

        birthdayDropdown.ClearOptions();
        animalDropdown.ClearOptions();
        colorDropdown.ClearOptions();

        birthdayDropdown.AddOptions(birthdays);
        animalDropdown.AddOptions(animals);
        colorDropdown.AddOptions(colors);

        // Pre-cache correct indexes for faster comparison
        birthdayIndex = birthdays.IndexOf(correctBirthday);
        animalIndex = animals.IndexOf(correctAnimal);
        colorIndex = colors.IndexOf(correctColor);

        ResetDropdowns();
    }

    private void OnVerifyButtonClick()
    {
        Debug.Log("Button clicked");
        ValidateAnswers();

        clickTime = Time.realtimeSinceStartup;
        // ... rest of the code ...
        float processTime = Time.realtimeSinceStartup - clickTime;
        Debug.Log($"Process time: {processTime * 1000}ms");


    }

    private void ValidateAnswers() 
    {
        // Immediate validation without method call
        if (birthdayDropdown.value == 0 ||
            animalDropdown.value == 0 ||
            colorDropdown.value == 0)
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Please answer all security questions";
            return;
        }

        // Direct comparison with cached indexes
        if (birthdayDropdown.value == birthdayIndex &&
            animalDropdown.value == animalIndex &&
            colorDropdown.value == colorIndex)
        {
            // Correct answers
            NewPasswordPanel.SetActive(true);
            securityQuestionsPanel.SetActive(false);
            ResetDropdowns();
        }
        else
        {
            // Wrong answers
            errorText.gameObject.SetActive(true);
            errorText.text = "One or more answers are incorrect";
        }
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
    }
}