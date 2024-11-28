using UnityEngine;
using UnityEngine.UI;

public class contentManage : MonoBehaviour
{
    // Alex GameObjects
    public GameObject alex1;
    public GameObject alex2;
    public GameObject alex3;
    public GameObject alex4;

    // Eric GameObjects
    public GameObject eric1;
    public GameObject eric2;
    public GameObject eric3;
    public GameObject eric4;

    // ScrollRects for Alex and Eric
    public ScrollRect alexScrollRect; // ScrollRect for Alex
    public ScrollRect ericScrollRect; // ScrollRect for Eric

    // Alex Scrollable Contents
    public RectTransform alexContent1;
    public RectTransform alexContent2;
    public RectTransform alexContent3;
    public RectTransform alexContent4;

    // Eric Scrollable Contents
    public RectTransform ericContent1;
    public RectTransform ericContent2;
    public RectTransform ericContent3;
    public RectTransform ericContent4;

    void Start()
    {
        // Manage Alex objects
        ManageAlexObjects();

        // Manage Eric objects
        ManageEricObjects();
    }

    void ManageAlexObjects()
    {
        if (LogInManager.alex1turnOn)
        {
            SetActiveAlexObjects(alex2, alex1, alex3, alex4);
            SetScrollContent(alexScrollRect, alexContent2);
        }

        if (ScrollTracker.IsMessage1Activated)
        {
            SetActiveAlexObjects(alex3, alex1, alex2, alex4);
            SetScrollContent(alexScrollRect, alexContent3);
        }

        if (CamScriptForFinal.alexFinalActivated)
        {
            SetActiveAlexObjects(alex4, alex1, alex2, alex3);
            SetScrollContent(alexScrollRect, alexContent4);
        }
        
    }

    void ManageEricObjects()
    {
        if (CaptcahManager.ericm1)
        {
            SetActiveEricObjects(eric2, eric1, eric3, eric4);
            SetScrollContent(ericScrollRect, ericContent2);
        }

        if (ScrollTracker.IsMessage2Activated)
        {
            SetActiveEricObjects(eric3, eric1, eric2, eric4);
            SetScrollContent(ericScrollRect, ericContent3);
        }
        if (CamScriptForFinal.ericFinalActivated)
        {
            SetActiveAlexObjects(eric4, eric1, eric2, eric3);
            SetScrollContent(ericScrollRect, ericContent4);
        }
    }

    void SetActiveAlexObjects(GameObject active, params GameObject[] inactiveObjects)
    {
        if (active != null) active.SetActive(true);
        Debug.Log("Alex object set active");
        foreach (GameObject obj in inactiveObjects)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

    void SetActiveEricObjects(GameObject active, params GameObject[] inactiveObjects)
    {
        if (active != null) active.SetActive(true);
        Debug.Log("Eric object set active");
        foreach (GameObject obj in inactiveObjects)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

    void SetScrollContent(ScrollRect scrollRect, RectTransform newContent)
    {
        if (scrollRect != null && newContent != null)
        {
            // Assign the new content to the ScrollRect
            scrollRect.content = newContent;

            // Reset the scroll position to the top
            scrollRect.verticalNormalizedPosition = 1f;

            Debug.Log($"ScrollRect content switched to: {newContent.name}");
        }
    }
}
