using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundManager : MonoBehaviour
{
    private static backgroundManager instance;
    private AudioSource audioSource;

    //fade out last music
    public AudioSource lastMusic;
    public string backgroundMusicName = "IntromusicManager";
    private void Start()
    {
        AudioSource[] allAudio = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudio)
        {
            if (audio.gameObject.scene.name == "DontDestroyOnLoad" &&
                audio.gameObject.name == backgroundMusicName)
            {
                lastMusic = audio;
                Debug.Log("Found background music: " + audio.gameObject.name);
                break;
            }
        }

        StartCoroutine(FadeOutMusic());
    }

    IEnumerator FadeOutMusic()
    {
        float timeElapsed = 0;
        float duration = 2f;
        float startVolume = lastMusic.volume;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            lastMusic.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        lastMusic.Stop();
    }

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
