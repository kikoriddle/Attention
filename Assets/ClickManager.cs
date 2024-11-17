// Start is called before the first frame update
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to AudioSource component
    public AudioClip clickSound;     // Your click sound effect

    void Start()
    {
        // If no AudioSource is assigned, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the click sound
        audioSource.clip = clickSound;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Check for mouse click (left button)
        if (Input.GetMouseButtonDown(0))
        {
            // Play the sound
            audioSource.Play();
        }
    }
}