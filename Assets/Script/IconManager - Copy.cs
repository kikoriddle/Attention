using UnityEngine;
using UnityEngine.SceneManagement;

public class IconManager : MonoBehaviour
{
    // Hover settings
    private SpriteRenderer spriteRenderer;
    public Color normalColor = Color.white;
    public Color hoverColor = new Color(0.8f, 0.8f, 0.8f, 1f);

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = normalColor;
    }

    // Mouse hover effects
    void OnMouseEnter()
    {
        spriteRenderer.color = hoverColor;
    }

    void OnMouseExit()
    {
        spriteRenderer.color = normalColor;
    }

    void OnMouseDown()
    {
        string sceneName = gameObject.tag; // Use the tag name directly as the scene name
        Debug.Log($"Mouse clicked on icon with tag: {sceneName}");

        if (!string.IsNullOrEmpty(sceneName))
        {
            LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is empty or not assigned!");
        }
    }

    private void LoadScene(string sceneName)
    {
        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
