using UnityEngine;
using UnityEngine.SceneManagement; // For scene switching
using System.Collections;

public class CamScriptForFinal : MonoBehaviour
{
    public GameObject[] gameObjects; // Assign the series of GameObjects in the Inspector
    private int currentIndex = 0; // Tracks the currently active GameObject
    public AudioSource transitionSound;  
    public GameObject animationObject; // Assign the GameObject with the animation
    public Camera mainCamera; // Assign the camera in the Inspector
    private float targetZoom;
    public static bool changeTag;

    public float zoomInSize = 5f;  // Orthographic size for zoomed-in state
    public float zoomOutSize = 10f; // Orthographic size for zoomed-out state
    public float zoomSpeed = 2f;  // Speed of the zoom transition

    private bool isZoomedOut = false; // Tracks the zoom state

    // GameObjects to toggle based on zoom state
    public GameObject zoomedInObject1;
    public GameObject zoomedInObject2;

    // Two new GameObjects to activate at the second-to-last photo
    public GameObject alexFinal;
    public GameObject ericFinal;

    // Static booleans to track whether these objects have been activated
    public static bool alexFinalActivated = false;
    public static bool ericFinalActivated = false;

    private static bool hasResetPlayerPrefs = false; // Ensures PlayerPrefs reset only once
    private static bool finalObjectsActivated = false; // Tracks if final objects have been activated
    private bool isAnimationPlaying = false; // Tracks if animation is currently playing

    //sound
    public AudioClip popupSound;
    void Start()
    {
        changeTag = false;

        if (!hasResetPlayerPrefs)
        {
            PlayerPrefs.SetInt("AlexFinalActivated", 0);
            PlayerPrefs.SetInt("EricFinalActivated", 0);
            PlayerPrefs.Save();

            hasResetPlayerPrefs = true;
        }

        alexFinalActivated = PlayerPrefs.GetInt("AlexFinalActivated", 0) == 1;
        ericFinalActivated = PlayerPrefs.GetInt("EricFinalActivated", 0) == 1;

        if (mainCamera == null)
        {
            mainCamera = Camera.main; 
            if (mainCamera == null)
            {
                Debug.LogError("Main Camera is not assigned!");
                return;
            }
        }

        if (animationObject == null)
        {
            Debug.LogError("Animation GameObject is not assigned!");
            return;
        }

        targetZoom = mainCamera.orthographicSize;
        ActivateCurrentGameObject();
    }

    void Update()
    {
        if (isAnimationPlaying) return;

        // Handle zoom toggling
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isZoomedOut = !isZoomedOut; 
            targetZoom = isZoomedOut ? zoomOutSize : zoomInSize;

            // Toggle GameObjects based on zoom state
            if (zoomedInObject1 != null) zoomedInObject1.SetActive(!isZoomedOut);
            if (zoomedInObject2 != null) zoomedInObject2.SetActive(!isZoomedOut);
        }

        if (mainCamera != null)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
        }

        // Handle switching to the next GameObject
        if (!isZoomedOut && Input.GetKeyDown(KeyCode.D))
        {
            if (currentIndex == gameObjects.Length - 1)
            {
                StartCoroutine(SwitchSceneWithAnimation());
            }
            else
            {
                StartCoroutine(SmoothTransitionToNextGameObject());
            }
        }

        // Prevent moving left from the first object
        if (!isZoomedOut && Input.GetKeyDown(KeyCode.A))
        {
            if (currentIndex > 0)
            {
                StartCoroutine(SmoothTransitionToPreviousGameObject());
            }
            else
            {
                Debug.Log("Already at the first object. Cannot move left.");
            }
        }

        // Automatically switch scene when on the last object
        if (currentIndex == gameObjects.Length - 1)
        {
            StartCoroutine(SwitchSceneWithAnimation());
        }

        if (currentIndex == gameObjects.Length - 2 && !finalObjectsActivated)
        {
            if (!alexFinalActivated && alexFinal != null)
            {
                alexFinal.SetActive(true);
                AudioSource.PlayClipAtPoint(popupSound, Camera.main.transform.position, 0.5f);
                alexFinalActivated = true;
                PlayerPrefs.SetInt("AlexFinalActivated", 1);
                PlayerPrefs.Save();
            }

            if (!ericFinalActivated && ericFinal != null)
            {
                ericFinal.SetActive(true);
                AudioSource.PlayClipAtPoint(popupSound, Camera.main.transform.position, 0.5f);
                ericFinalActivated = true;
                PlayerPrefs.SetInt("EricFinalActivated", 1);
                PlayerPrefs.Save();
            }

            finalObjectsActivated = true;
        }
    }

    void ActivateCurrentGameObject()
    {
        if (gameObjects == null || gameObjects.Length == 0)
        {
            Debug.LogError("GameObjects array is empty!");
            return;
        }

        foreach (GameObject obj in gameObjects)
        {
            if (obj != null) obj.SetActive(false);
        }

        if (currentIndex >= 0 && currentIndex < gameObjects.Length && gameObjects[currentIndex] != null)
        {
            gameObjects[currentIndex].SetActive(true);
        }
    }

    IEnumerator SmoothTransitionToNextGameObject()
    {
        isAnimationPlaying = true;

        if (transitionSound != null) transitionSound.Play();

        if (animationObject != null)
        {
            animationObject.SetActive(true);
            Animator animator = animationObject.GetComponent<Animator>();
            if (animator != null)
            {
                AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
                if (clips.Length > 0)
                {
                    yield return new WaitForSeconds(clips[0].clip.length);
                }
            }
            animationObject.SetActive(false);
        }

        if (gameObjects[currentIndex] != null)
        {
            gameObjects[currentIndex].SetActive(false);
        }

        currentIndex = (currentIndex + 1) % gameObjects.Length;
        ActivateCurrentGameObject();
        isAnimationPlaying = false;
    }

    IEnumerator SmoothTransitionToPreviousGameObject()
    {
        isAnimationPlaying = true;

        if (transitionSound != null) transitionSound.Play();

        if (animationObject != null)
        {
            animationObject.SetActive(true);
            Animator animator = animationObject.GetComponent<Animator>();
            if (animator != null)
            {
                AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
                if (clips.Length > 0)
                {
                    yield return new WaitForSeconds(clips[0].clip.length);
                }
            }
            animationObject.SetActive(false);
        }

        if (gameObjects[currentIndex] != null)
        {
            gameObjects[currentIndex].SetActive(false);
        }

        currentIndex = (currentIndex - 1 + gameObjects.Length) % gameObjects.Length;
        ActivateCurrentGameObject();
        isAnimationPlaying = false;
    }

    IEnumerator SwitchSceneWithAnimation()
    {
        isAnimationPlaying = true;

        if (animationObject != null)
        {
            animationObject.SetActive(true);
            Animator animator = animationObject.GetComponent<Animator>();
            if (animator != null)
            {
                AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
                if (clips.Length > 0)
                {
                    yield return new WaitForSeconds(clips[0].clip.length);
                }
            }
            animationObject.SetActive(false);
        }

        changeTag = true; 
        SceneManager.LoadScene("Message 1");
    }
}
