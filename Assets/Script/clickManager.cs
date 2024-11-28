using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class clickManager : MonoBehaviour
{
    public static clickManager Instance;
    private AudioSource clickSource;

    // Add a public GameObject that you want to activate for 3 seconds
    public GameObject objectToActivate;

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

        // If Left Shift is pressed, activate the object for 3 seconds
        
            ActivateObjectForTime();
        
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

    // Activate the object for 3 seconds and then turn it off
    private void ActivateObjectForTime()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);  // Activate the object
            Debug.Log($"{objectToActivate.name} activated!");

            // Start a coroutine to deactivate it after 3 seconds
            StartCoroutine(DeactivateAfterTime(3f));
        }
        else
        {
            Debug.LogError("objectToActivate is not assigned!");
        }
    }

    // Coroutine to deactivate the object after a set amount of time
    private IEnumerator DeactivateAfterTime(float time)
    {
        // Wait for the specified time (in this case, 3 seconds)
        yield return new WaitForSeconds(time);

        // Deactivate the object
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
            Debug.Log($"{objectToActivate.name} deactivated!");
        }
    }
}
