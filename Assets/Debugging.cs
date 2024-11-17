using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debugging : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    // Start is called before the first frame update
    void Start()
    {
        // Check if input field is properly connected
        if (usernameInput == null)
        {
            Debug.LogError("Input Field not assigned!");
            return;
        }

        // Add listeners to track input
        usernameInput.onValueChanged.AddListener((value) => {
            Debug.Log("Text changed to: " + value);
        });

        usernameInput.onSelect.AddListener((value) => {
            Debug.Log("Input Field selected");
        });
    }

}
