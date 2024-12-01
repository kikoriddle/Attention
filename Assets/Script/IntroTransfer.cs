using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTransfer : MonoBehaviour
{
    private string nextSceneName = "000 Begin"; // Name of the next scene

    void Start()
    {
        // You can optionally call switchScene() here if the scene should change automatically
        // switchScene();
    }

    void Update()
    {
        // Optionally add input or trigger-based scene switching logic here
    }

    void switchScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            // Correct method to load the scene
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is null or empty. Please set a valid scene name.");
        }
    }
}
