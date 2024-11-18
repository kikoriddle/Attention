using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickManager : MonoBehaviour
{
    public static clickManager Instance;
    private AudioSource clickSource;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            clickSource = GetComponent<AudioSource>();

            // Find and add click sound to all buttons
            AddClickSoundToButtons();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        // Play sound on any mouse click
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            PlayClickSound();
        }
    }

    void AddClickSoundToButtons()
    {
        // Find all buttons in the scene
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button button in allButtons)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    public void PlayClickSound()
    {
        if (clickSource != null)
        {
            clickSource.PlayOneShot(clickSource.clip);
        }
    }

    // Call this when loading new scenes to add sound to new buttons
    public void SetupNewSceneButtons()
    {
        AddClickSoundToButtons();
    }

}
