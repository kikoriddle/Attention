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
            if (audio.gameObject.name == backgroundMusicName)
            {
                lastMusic = audio;
                Debug.Log("Found background music: " + audio.gameObject.name);
                break;
            }
        }

        if (lastMusic != null)
        {
            StartCoroutine(FadeOutMusic());
        }
        else
        {
            Debug.LogWarning("No matching background music found with name: " + backgroundMusicName);
        }
    }

    IEnumerator FadeOutMusic()
    {
        if (lastMusic == null)
        {
            Debug.LogError("lastMusic is null. Cannot fade out music.");
            yield break;
        }

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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
