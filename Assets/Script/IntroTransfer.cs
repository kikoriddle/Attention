using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class IntroTransfer : MonoBehaviour
{
    public GameObject object1; 
    public GameObject object2; 
    public GameObject object3; 
    public GameObject object4;
    //public bool ifsolveAllGamePuzzle = false;
    //if user first enter ins-> then its false but then after they solve the captcha
    //begins true-> if true then they dont have to do the LogIn
    private int currentIndex = 0;
    private string nextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        nextSceneName = "01 Main Window";
    }

  

    void Update()
    {
        // Check for mouse click or spacebar press
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            ToggleObjects();
        }
    }

   void ToggleObjects()
    {
        // Turn off the current object
        if (currentIndex == 0) object1.SetActive(false);
        else if (currentIndex == 1) object2.SetActive(false);
        else if (currentIndex == 2) object3.SetActive(false);
        else if (currentIndex == 3)
        {
            object4.SetActive(false);

            // If it's the fourth object, transfer to the next scene
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
                return; // Exit to prevent further code execution
            }
            else
            {
                Debug.LogError("Next scene name is not assigned!");
                return;
            }
        }

        // Move to the next object
        currentIndex = (currentIndex + 1) % 4;

        // Turn on the next object
        if (currentIndex == 0) object1.SetActive(true);
        else if (currentIndex == 1) object2.SetActive(true);
        else if (currentIndex == 2) object3.SetActive(true);
        else if (currentIndex == 3) object4.SetActive(true);
    }
}

