using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalClickSound : MonoBehaviour
{
    public static GlobalClickSound Instance;
    private AudioSource audioSource;

    void Awake()
    {
        // Make sure only one exists across all scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Get or add AudioSource
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            // Subscribe to scene loading event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Play sound on any mouse click in any scene
        if (Input.GetMouseButtonDown(0))
        {
            PlayClickSound();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ensure sound still works in new scene
        Debug.Log($"Scene {scene.name} loaded - Click sound ready");
    }

    public void PlayClickSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    void OnDestroy()
    {
        // Clean up scene loading subscription
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}