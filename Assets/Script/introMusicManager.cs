using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introMusicManager : MonoBehaviour
{
    private static introMusicManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        // Check if another instance exists
        if (instance == null)
        {
            // First time - make this the instance
            instance = this;
            DontDestroyOnLoad(gameObject); // This keeps it alive between scenes
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            // Instance already exists - destroy duplicate
            Destroy(gameObject);
        }
    }

    // Optional: Add volume control
    //for later i think
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

}
