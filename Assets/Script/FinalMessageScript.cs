using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FinalMessageScript : MonoBehaviour
{
    // GameObjects for Alex and Eric (two for each)
    public GameObject alexObjectY;
    public GameObject alexObjectB;
    public GameObject ericObjectY;
    public GameObject ericObjectB;

    // GameObjects to show when buttons are clicked
    public GameObject alexGameObject;
    public GameObject ericGameObject;

    // UI Buttons
    public Button alexButtonY;
    public Button alexButtonB;
    public Button ericButtonY;
    public Button ericButtonB;

    // Animation play flags
    private bool alexYPlayed = false;
    private bool alexBPlayed = false;
    private bool ericYPlayed = false;
    private bool ericBPlayed = false;

    // Transition GameObject for fade animation
    public GameObject transitionObject;

    // Static booleans for combinations
    public static bool turnA = false;
    public static bool turnB = false;
    public static bool turnC = false;
    public static bool turnD = false;

    void Start()
    {
        // Initial state setup
        alexObjectY.SetActive(false);
        alexObjectB.SetActive(false);
        ericObjectY.SetActive(false);
        ericObjectB.SetActive(false);

        transitionObject.SetActive(false);
        alexGameObject.SetActive(false);
        ericGameObject.SetActive(false);

        // Assign button listeners
        alexButtonY.onClick.AddListener(ActivateAlexObjectY);
        alexButtonB.onClick.AddListener(ActivateAlexObjectB);
        ericButtonY.onClick.AddListener(ActivateEricObjectY);
        ericButtonB.onClick.AddListener(ActivateEricObjectB);
    }

    void Update()
    {
        // Trigger transition animation with "R" key
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(PlayTransitionAnimation());
        }
    }

    void ActivateAlexObjectY()
    {
        alexObjectY.SetActive(true);
        StartCoroutine(PlayAnimation(alexObjectY, "AlexY"));
        alexButtonY.interactable = false;
        alexButtonB.interactable = false;
        alexGameObject.SetActive(true);
    }

    void ActivateAlexObjectB()
    {
        alexObjectB.SetActive(true);
        StartCoroutine(PlayAnimation(alexObjectB, "AlexB"));
        alexButtonY.interactable = false;
        alexButtonB.interactable = false;
        alexGameObject.SetActive(true);
    }

    void ActivateEricObjectY()
    {
        ericObjectY.SetActive(true);
        HandleParentScrollView(ericObjectY);
        StartCoroutine(PlayAnimation(ericObjectY, "EricY"));
        ericButtonY.interactable = false;
        ericButtonB.interactable = false;
        ericGameObject.SetActive(true);
    }

    void ActivateEricObjectB()
    {
        ericObjectB.SetActive(true);
        HandleParentScrollView(ericObjectB);
        StartCoroutine(PlayAnimation(ericObjectB, "EricB"));
        ericButtonY.interactable = false;
        ericButtonB.interactable = false;
        ericGameObject.SetActive(true);
    }

    IEnumerator PlayAnimation(GameObject obj, string animationType)
    {
        Animator animator = obj.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("canPlay");
            AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
            if (clips.Length > 0)
            {
                yield return new WaitForSeconds(clips[0].clip.length);
            }
        }
        else
        {
            Debug.LogWarning("Animator component not found on the object.");
        }

        UpdateFlags(animationType);
        UpdateBooleanFlags();
        CheckAndSwitchScene();
    }

    void UpdateFlags(string animationType)
    {
        if (animationType == "AlexY") alexYPlayed = true;
        else if (animationType == "AlexB") alexBPlayed = true;
        else if (animationType == "EricY") ericYPlayed = true;
        else if (animationType == "EricB") ericBPlayed = true;
    }

    void UpdateBooleanFlags()
    {
        if (alexYPlayed && ericYPlayed) turnA = true;
        else if (alexYPlayed && ericBPlayed) turnB = true;
        else if (alexBPlayed && ericYPlayed) turnC = true;
        else if (alexBPlayed && ericBPlayed) turnD = true;
    }

    void CheckAndSwitchScene()
    {
        if ((alexYPlayed || alexBPlayed) && (ericYPlayed || ericBPlayed))
        {
            StartCoroutine(PlayTransitionAnimation());
        }
    }

    IEnumerator PlayTransitionAnimation()
    {
        transitionObject.SetActive(true);

        Animator transitionAnimator = transitionObject.GetComponent<Animator>();
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("FadeOut");
            AnimatorClipInfo[] clips = transitionAnimator.GetCurrentAnimatorClipInfo(0);
            if (clips.Length > 0)
            {
                yield return new WaitForSeconds(clips[0].clip.length);
            }
        }

        SceneManager.LoadScene("EndScene");
    }

    void HandleParentScrollView(GameObject obj)
    {
        Transform parentParentParent = obj.transform.parent?.parent?.parent;
        if (parentParentParent != null)
        {
            ScrollRect scrollView = parentParentParent.GetComponent<ScrollRect>();
            if (scrollView != null)
            {
                scrollView.enabled = false;
                Debug.Log($"ScrollRect on {parentParentParent.name} has been disabled.");
            }
            else
            {
                Debug.LogWarning($"No ScrollRect component found on parent's parent's parent object {parentParentParent.name}.");
            }
        }
        else
        {
            Debug.LogWarning("Animator's parent's parent's parent object is null.");
        }
    }
}
