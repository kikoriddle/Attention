using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManage : MonoBehaviour
{
    //music changing
    public AudioSource introMusic;
    public AudioSource endingMusic1;
    public AudioSource endingMusic2;
    private bool musicHasChanged = false;


    // References to the three GameObjects
    public GameObject objectA;
    public GameObject objectB;
    public GameObject objectC;
    public GameObject objectD;

    // Boolean variables to control which object is active
    public bool turnA = false;
    public bool turnB = false;
    public bool turnC = false;
    public bool turnD = false;
    public string backgroundMusicName = "backgroundManager"; 
    // Start is called before the first frame update
    void Start()
    {
        // Initialize all objects as inactive
        objectA.SetActive(false);
        objectB.SetActive(false);
        objectC.SetActive(false);
        objectD.SetActive(false);

        //find it at do not destory
         AudioSource[] allAudio = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudio)
        {
            if (audio.gameObject.scene.name == "DontDestroyOnLoad" && 
                audio.gameObject.name == backgroundMusicName)
            {
                introMusic = audio;
                Debug.Log("Found background music: " + audio.gameObject.name);
                break;
            }
        }
    }

    //music transition
 IEnumerator FadeOutMusic()
    {
        float timeElapsed = 0;
        float duration = 2f;
        float startVolume = introMusic.volume;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            introMusic.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        introMusic.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicHasChanged && introMusic != null)
        {
                StartCoroutine(FadeOutMusic());
                musicHasChanged = true;
        }
        // Check each boolean and set the corresponding GameObject active or inactive
        if (FinalMessageScript.turnA)
        {
            objectA.SetActive(true);
        }
        else
        {
            objectA.SetActive(false);
        }

        if (FinalMessageScript.turnD)
        {
            objectB.SetActive(true);
        }
        else
        {
            objectB.SetActive(false);
        }

        if (FinalMessageScript.turnC)
        {
            objectC.SetActive(true);
        }
        else
        {
            objectC.SetActive(false);
        }
         if (FinalMessageScript.turnB)
        {
            objectD.SetActive(true);
        }
        else
        {
            objectD.SetActive(false);
        }
    }
}
